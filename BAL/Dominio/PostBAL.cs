using BAL.DTO;
using BaseDeDatos;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;

namespace BAL.Dominio
{
    public class PostBAL : ConexionSQLServer
    {
        public async void ConsumirApiguardarbd()
        {
            var url = "https://jsonplaceholder.typicode.com/posts";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var Comment = JsonConvert.DeserializeObject<List<PostDTO>>(content);

                    var table = new DataTable();
                    table.Columns.Add("UserId", typeof(int));
                    table.Columns.Add("Id", typeof(int));
                    table.Columns.Add("Title", typeof(string));
                    table.Columns.Add("Body", typeof(string));

                    foreach (var itemDetail in Comment)
                    {
                        table.Rows.Add(new object[]
                            {
                                itemDetail.UserId,
                                itemDetail.Id,
                                itemDetail.Title,
                                itemDetail.Body
                            });
                    }
                    using (var connection = getConnection())
                    {
                        connection.Open();
                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                            {
                                try
                                {
                                    bulkCopy.DestinationTableName = "Posts";
                                    bulkCopy.WriteToServer(table);
                                    transaction.Commit();
                                }
                                catch (Exception)
                                {
                                    transaction.Rollback();
                                    connection.Close();
                                    throw;
                                }
                            }
                        }
                    }

                }

            }

        }
        public dynamic ListarPost()
        {


            DataTable data = Listar("sp_ListPost");

            string jsongenero = JsonConvert.SerializeObject(data);

            var resultado = new
            {
                success = true,
                message = "Exito",
                result = new
                {
                    familiares = JsonConvert.DeserializeObject(jsongenero)
                }
            };

            return resultado;
        }
        public dynamic CrearPost(PostDTO post)
        {
            List<Parametro> lista = new List<Parametro>
            {
                new Parametro("@userid", post.UserId.ToString()),
                new Parametro("@postid", post.Id.ToString()),
                new Parametro("@title", post.Title.ToString()),
                new Parametro("@body", post.Body.ToString()),

            };
            bool resultadoP = Ejecutar("sp_InsertPost", lista);
            var resultado = new
            {
                success = resultadoP,
                message = resultadoP ? "Exito" : "Error al guardar",
                result = ""
            };

            return resultado;
        }
        public dynamic ActualizarPost(PostDTO post)
        {

            List<Parametro> lista = new List<Parametro>
            {
                new Parametro("@userid", post.UserId.ToString()),
                new Parametro("@postid", post.Id.ToString()),
                new Parametro("@title", post.Title.ToString()),
                new Parametro("@body", post.Body.ToString()),

            };

            bool resultadop = Ejecutar("sp_UpdatePost", lista);

            var resultado = new
            {
                success = resultadop,
                message = resultadop ? "Exito" : "Error al guardar",
                result = ""
            };
            return resultado;
        }
        public dynamic EliminarPost(int postid)
        {
            List<Parametro> lista = new List<Parametro>
            {
                new Parametro("@postid", postid.ToString())
            };

            bool resultadop = Ejecutar("sp_DeletePost", lista);

            var resultado = new
            {
                success = resultadop,
                message = resultadop ? "Exito" : "Error al Eliminar",
                result = ""
            };
            return resultado;
        }
        public dynamic ObtenerPost(int idpost)
        {
            List<Parametro> lista = new List<Parametro>
            {
                new Parametro("@idfamiliar", idpost.ToString())
            };

            DataTable data = Listar("sp_ObtenerPost", lista);

            string jsongenero = JsonConvert.SerializeObject(data);
 
            var resultado = new
            {
                success = true,
                message = "Exito",
                result = new
                {
                    familiares = JsonConvert.DeserializeObject(jsongenero)
                }
            };

            return resultado;
        }



    }
}
