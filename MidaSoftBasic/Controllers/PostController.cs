using BAL.Dominio;
using BAL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MidaSoftBasic.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PostController : ControllerBase
    {
        private PostBAL bussinessAccessLogic = new PostBAL();
        [HttpGet]
        [Route("ConsumirjsonplaceholderPost")]
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
        [Route("ListarPost")]
        public dynamic ListarPost()
        {
            try
            {

                return bussinessAccessLogic.ListarPost();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        [Route("CrearPost")]
        public dynamic CrearPost(PostDTO post)
        {
            try
            {
                return bussinessAccessLogic.CrearPost(post);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        [Route("ActualizarPost")]
        public dynamic ActualizarPost(PostDTO post)
        {
            try
            {
                return bussinessAccessLogic.ActualizarPost(post);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpGet]
        [Route("ObtenerPost")]
        public dynamic ObtenerPost(int IdPost)
        {
            try
            {
                return bussinessAccessLogic.ObtenerPost(IdPost);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpDelete]
        [Route("EliminarPost")]
        public dynamic EliminarPost(int IdPost)
        {
            try
            {
                return bussinessAccessLogic.EliminarPost(IdPost);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
