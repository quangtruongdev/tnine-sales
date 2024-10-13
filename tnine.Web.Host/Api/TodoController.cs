using System.Net;
using System.Net.Http;
using System.Web.Http;
using tnine.Application.Shared.ITodo;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/todo")]
    public class TodoController : ApiController
    {
        private ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _todoService.GetAll());
        }
    }
}