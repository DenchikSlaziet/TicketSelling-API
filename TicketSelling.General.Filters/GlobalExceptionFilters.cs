using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using TicketSelling.Common.Entity.EntityInterface;
using TicketSelling.Services.Contracts.Exceptions;

namespace TicketSelling.General.Filters
{
    public class GlobalExceptionFilters : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                var exception = filterContext.Exception;

                int statusCode;

                switch (true)
                {
                    case bool _ when exception is TimeTableEntityNotFoundException<IEntity>:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;


                    case bool _ when exception is TimeTableInvalidOperationException:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        break;

                    case bool _ when exception is TimeTableNotFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
            }
        }
    }
}
