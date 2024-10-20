using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tnine.Application.Shared.ITodo;
using tnine.Application.Shared.ITodoService.Dto;
using tnine.Core.Shared.Dto;

namespace tnine.Web.Host.Api
{
    [RoutePrefix("api/todo")]
    public class TodoApiController : ApiController
    {
        private ITodoService _todoService;

        public TodoApiController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var todos = await _todoService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, todos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetById(long id)
        {
            var input = new EntityDto<long> { Id = id };
            var todo = await _todoService.GetById(input);
            return Request.CreateResponse(HttpStatusCode.OK, todo);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateOrEdit([FromBody] CreateOrEditTodoDto input)
        {
            if (input == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }

            try
            {
                await _todoService.CreateOrEdit(input);
                return Request.CreateResponse(HttpStatusCode.OK, "Todo created or updated successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<HttpResponseMessage> Delete(long id)
        {
            var input = new EntityDto<long> { Id = id };
            await _todoService.Delete(input);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}