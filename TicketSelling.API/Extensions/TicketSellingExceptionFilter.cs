using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.Services.Contracts.Exceptions;
using TicketSelling.API.Exceptions;
using FluentValidation;
using TicketSelling.General;
using Microsoft.EntityFrameworkCore;

namespace TicketSelling.API.Extensions
{
    /// <summary>
    /// Фильтр для обработки ошибок раздела администрирования
    /// </summary>
    public class TicketSellingExceptionFilter : IExceptionFilter
    {
        /// <inheritdoc/>
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception; //TODO: убрали AS
            if (exception == null)
            {
                return;
            }

            switch (exception)
            {
                case TimeTableValidationException ex:
                    SetDataToContext(
                        new ConflictObjectResult(new ApiValidationExceptionDetail
                        {
                            Errors = ex.Errors,
                        }),
                        context);
                    break;

                //case TimeTableInvalidOperationException ex:
                //    SetDataToContext(
                //        new BadRequestObjectResult(new ApiExceptionDetail { Message = ex.Message, })
                //        {
                //            StatusCode = StatusCodes.Status406NotAcceptable,
                //        },
                //        context);
                //    break;

                case TimeTableNotFoundException ex:
                    SetDataToContext(new NotFoundObjectResult(new ApiExceptionDetail
                    {
                        Message = ex.Message,
                    }), context);
                    break;

                case ValidationException ex:
                    SetDataToContext(new ConflictObjectResult(new ApiValidationExceptionDetail
                    {
                        Errors = ex.Errors.Select(x => InvalidateItemModel.New(x.PropertyName, x.ErrorMessage))
                    }), context);
                    break;

                case DbUpdateException ex:
                    SetDataToContext(new BadRequestObjectResult(new ApiExceptionDetail
                    {
                        Message = $"Ошибка записи в Базу данных (проверьте по индексам). {ex.TargetSite}"
                    }), context);
                    break;

                default:
                    SetDataToContext(new BadRequestObjectResult(new ApiExceptionDetail
                    {
                        Message = $"Ошибка приведения типов. {exception.Message}",
                    }), context);
                    break;
            }
        }

        /// <summary>
        /// Определяет контекст ответа
        /// </summary>
        static protected void SetDataToContext(ObjectResult data, ExceptionContext context)
        {
            context.ExceptionHandled = true;
            var response = context.HttpContext.Response;
            response.StatusCode = data.StatusCode ?? StatusCodes.Status400BadRequest;
            context.Result = data;
        }
    }
}
