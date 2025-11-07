using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ConnectApp.Shared.Results;

namespace ConnectApp.Api.Controllers.Base
{
    public abstract class BaseController : ControllerBase
    {

        protected async Task<IActionResult> CreateGetResponse(object? result, object? service = null)
        {
            return await CreateResponse(result, "202", "Operação concluída com sucesso.");
        }


        protected async Task<IActionResult> CreatePostResponse(object? result, object? service = null)
        {
            return await CreateResponse(result, "201", "Registro criado com sucesso.");
        }


        private async Task<IActionResult> CreateResponse(object? result, string successCode, string successMessage)
        {
            if (result == null)
            {
                return NotFound(new ResponseMessage
                {
                    Code = "404",
                    Message = "Nenhum registro encontrado."
                });
            }

            // Verifica se é um Result<T> com erros
            if (IsGenericResultWithErrors(result))
            {
                var errors = GetErrorsFromResult(result);
                return BadRequest(new ResponseMessage
                {
                    Code = "400",
                    Message = string.Join("; ", errors)
                });
            }

            
            if (result is bool b && !b)
            {
                return NotFound(new ResponseMessage
                {
                    Code = "404",
                    Message = "Operação não realizada."
                });
            }

            // Extrai o valor se for Result<T>
            var finalResult = ExtractValueFromResult(result);

            if (finalResult == null)
            {
                return NotFound(new ResponseMessage
                {
                    Code = "404",
                    Message = "Nenhum registro encontrado."
                });
            }

            return Ok(new ResponseMessage
            {
                Code = successCode,
                Message = successMessage,
                Id = (Guid)TryGetIdFromResult(finalResult)!
            });
        }


        protected async Task<IActionResult> CreateExceptionResponse(Exception e)
        {
            return BadRequest(new ResponseMessage
            {
                Code = "500",
                Message = $"Erro interno: {e.Message}"
            });
        }


        private static bool IsGenericResultWithErrors(object result)
        {
            var type = result.GetType();
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Result<>))
            {
                var hasErrorsProperty = type.GetProperty("HasErrors");
                if (hasErrorsProperty != null)
                {
                    return (bool)hasErrorsProperty.GetValue(result)!;
                }
            }
            return false;
        }


        private static string[] GetErrorsFromResult(object result)
        {
            var type = result.GetType();
            var errorsProperty = type.GetProperty("Errors");
            if (errorsProperty != null)
            {
                var errors = errorsProperty.GetValue(result) as List<string>;
                return errors?.ToArray() ?? [];
            }
            return [];
        }


        private static object? ExtractValueFromResult(object result)
        {
            var type = result.GetType();
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Result<>))
            {
                var valueProperty = type.GetProperty("Value");
                return valueProperty?.GetValue(result);
            }
            return result;
        }


        private static Guid? TryGetIdFromResult(object result)
        {

            var prop = result.GetType().GetProperty("AccountId");
            if (prop != null)
            {
                var value = prop.GetValue(result);
                if (value is Guid guid)
                    return guid;
            }

            prop = result.GetType().GetProperty("Id");
            if (prop != null)
            {
                var value = prop.GetValue(result);
                if (value is Guid guid)
                    return guid;
            }

            return null;
        }
    }
}