using BAL.DTO;
using BaseDeDatos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;

namespace BAL.Dominio
{
    public class CommentBAL : ConexionSQLServer
    {

        public async void ConsumirApiguardarbd()
        {
            var url = "https://jsonplaceholder.typicode.com/comments";

            using(var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var Comment = JsonConvert.DeserializeObject<List<CommentDTO>>(content);

                    var table = new DataTable();
                    table.Columns.Add("id", typeof(int));
                    table.Columns.Add("postId", typeof(int));
                    table.Columns.Add("name", typeof(string));
                    table.Columns.Add("email", typeof(string));
                    table.Columns.Add("body", typeof(string));

                    foreach (var itemDetail in Comment)
                    {
                        table.Rows.Add(new object[]
                            {
                                itemDetail.Id,
                                itemDetail.PostId,
                                itemDetail.Name,
                                itemDetail.Email,
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
                                    bulkCopy.DestinationTableName = "Comments";
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
        public dynamic ListarComment()
        {


            DataTable data = Listar("sp_ListComment");

            string jsongenero = JsonConvert.SerializeObject(data);

            var resultado = new
            {
                success = true,
                message = "Exito",
                result = new
                {
                    ListarComment = JsonConvert.DeserializeObject(jsongenero)
                }
            };

            return resultado;
        }
        public dynamic ListarCommentxPost(int PostId)
        {
            List<Parametro> lista = new List<Parametro>
            {
                new Parametro("@postid", PostId.ToString()),
            };

            DataTable data = Listar("sp_ListCommentxPost", lista);

            string jsongenero = JsonConvert.SerializeObject(data);

            var resultado = new
            {
                success = true,
                message = "Exito",
                result = new
                {
                    ListarCommentxPost = JsonConvert.DeserializeObject<List<CommentxPostDTO>>(jsongenero)
                }
            };

            return resultado;
        }
        public dynamic CrearComment(CommentDTO comment)
        {
            List<Parametro> lista = new List<Parametro>
            {
                new Parametro("@comentid", comment.Id.ToString()),
                new Parametro("@postid", comment.PostId.ToString()),
                new Parametro("@name", comment.Name.ToString()),
                new Parametro("@email", comment.Email.ToString()),
                new Parametro("@body", comment.Body.ToString()),

            };
            bool resultadoP = Ejecutar("sp_InsertComments", lista);
            var resultado = new
            {
                success = resultadoP,
                message = resultadoP ? "Exito" : "Error al guardar",
                result = ""
            };

            return resultado;
        }
        public dynamic ActualizarComment(CommentDTO comment)
        {

            List<Parametro> lista = new List<Parametro>
            {
                new Parametro("@comentid", comment.Id.ToString()),
                new Parametro("@postid", comment.PostId.ToString()),
                new Parametro("@name", comment.Name.ToString()),
                new Parametro("@email", comment.Email.ToString()),
                new Parametro("@body", comment.Body.ToString()),

            };

            bool resultadop = Ejecutar("sp_UpdateComments", lista);

            var resultado = new
            {
                success = resultadop,
                message = resultadop ? "Exito" : "Error al guardar",
                result = ""
            };
            return resultado;
        }
        public dynamic EliminarComment(int comentid)
        {
            List<Parametro> lista = new List<Parametro>
            {
                new Parametro("@comentid", comentid.ToString())
            };

            bool resultadop = Ejecutar("sp_DeleteComment", lista);

            var resultado = new
            {
                success = resultadop,
                message = resultadop ? "Exito" : "Error al Eliminar",
                result = ""
            };
            return resultado;
        }
        public dynamic ObtenerComment(int comentid)
        {
            List<Parametro> lista = new List<Parametro>
            {
                new Parametro("@comentid", comentid.ToString())
            };

            DataTable data = Listar("sp_ObtenerComment", lista);

            string jsongenero = JsonConvert.SerializeObject(data);

            var resultado = new
            {
                success = true,
                message = "Exito",
                result = new
                {
                    ObtenerComment = JsonConvert.DeserializeObject(jsongenero)
                }
            };

            return resultado;
        }
    }
}
