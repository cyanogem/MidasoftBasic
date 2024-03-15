using BAL.Dominio;
using BAL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MidaSoftBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private CommentBAL bussinessAccessLogic = new CommentBAL();


        [HttpGet]
        [Route("Consumirjsonplaceholder")]
        public void ConsumirApiguardarbd()
        {
            try
            {

                bussinessAccessLogic.ConsumirApiguardarbd();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        [Route("ListarComment")]
        public dynamic ListarComment()
        {
            try
            {

                return bussinessAccessLogic.ListarComment();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpGet]
        [Route("ListarCommentxPost")]
        public dynamic ListarCommentxPost(int PostId)
        {
            try
            {

                return bussinessAccessLogic.ListarCommentxPost(PostId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        [Route("CrearComment")]
        public dynamic CrearComment(CommentDTO comment)
        {
            try
            {
                return bussinessAccessLogic.CrearComment(comment);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        [Route("ActualizarPost")]
        public dynamic ActualizarComment(CommentDTO comment)
        {
            try
            {
                return bussinessAccessLogic.ActualizarComment(comment);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpGet]
        [Route("ObtenerPost")]
        public dynamic Obtenercomment(int IdComment)
        {
            try
            {
                return bussinessAccessLogic.ObtenerComment(IdComment);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpDelete]
        [Route("EliminarPost")]
        public dynamic EliminarPost(int IdComment)
        {
            try
            {
                return bussinessAccessLogic.EliminarComment(IdComment);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
