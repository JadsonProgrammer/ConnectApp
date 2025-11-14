using Microsoft.AspNetCore.Mvc;
using ConnectApp.Shared.Results;

namespace ConnectApp.Api.Controllers.Base
{
    public abstract class BaseController : ControllerBase
    {
        protected async Task<IActionResult> CreateGetResponse<T>(Result<T> result)
            => await CreateResponse(result, "200");

        protected async Task<IActionResult> CreatePostResponse<T>(Result<T> result)
            => await CreateResponse(result, "201");

        protected async Task<IActionResult> CreateResponse<T>(Result<T> result, string successCode)
        {
            if (result == null)
            {
                return NotFound(new ResponseMessage { Code = "404", Message = "Nenhum registro encontrado." });
            }

            if (!result.Success)
            {
                return BadRequest(new ResponseMessage { Code = "400", Message = result.Message, Data = result.Errors });
            }
            var foundId = TryGetIdFromResult(result.Data);
            // sucesso
            var message = string.IsNullOrWhiteSpace(result.Message) ? GetDefaultMessageFromAction() : result.Message;

            return Ok(new ResponseMessage
            {
                Code = successCode,
                Message = message,
                Data = result.Data,
                Id = foundId
            });
        }

        private string GetDefaultMessageFromAction()
        {
            var controllerName = ControllerContext.ActionDescriptor.ControllerName;
            var actionName = ControllerContext.ActionDescriptor.ActionName;

            // normaliza (UserController -> User)
            string entity = controllerName.EndsWith("Controller")
                ? controllerName.Substring(0, controllerName.Length - "Controller".Length)
                : controllerName;

            var verb = actionName.ToLowerInvariant();

            if (verb.Contains("create") || verb.Contains("post"))
                return $"{entity} criado com sucesso.";
            if (verb.Contains("update") || verb.Contains("put") || verb.Contains("edit"))
                return $"{entity} atualizado com sucesso.";
            if (verb.Contains("delete") || verb.Contains("remove"))
                return $"{entity} excluído com sucesso.";
            if (verb.Contains("get") || verb.Contains("list"))
                return $"{entity} encontrado com sucesso.";

            return $"{entity} processado com sucesso.";
        }

        private static Guid? TryGetIdFromResult(object? finalResult)
        {
            if (finalResult == null) return null;
            var prop = finalResult.GetType().GetProperty("Id");
            if (prop == null) return null;
            var value = prop.GetValue(finalResult);
            
            return value is Guid g ? g : null;
        }

        protected async Task<IActionResult> CreateExceptionResponse(Exception e)
        {
            return BadRequest(new ResponseMessage { Code = "500", Message = $"Erro interno: {e.Message}" });
        }
    }

}
