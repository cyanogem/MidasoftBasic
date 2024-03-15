using BAL.DTO;
using BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BAL.Dominio
{
    public class UsuarioBAL : ConexionSQLServer
    {
        public dynamic ValidarUsuarios(string User)
        {
            List<Parametro> lista = new List<Parametro>
            {
                new Parametro("@usuario", User.ToString())
            };
            var data = ConexionSQLServer.Listar("sp_ValidarUsuario", lista);


            UsuarioDTO usuario = new UsuarioDTO();
            foreach (DataRow r in data.Rows)
            {
                usuario.Usuario = r[0].ToString().Trim();
                usuario.contrasena = r[1].ToString().Trim();
            }

            return usuario;
        }
    }
}
