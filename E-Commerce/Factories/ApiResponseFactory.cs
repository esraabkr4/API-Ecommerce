using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commerce.Factories
{
    public class ApiResponseFactory
    {
        public ApiResponseFactory()
        {
           
        }

        //public async Task<HttpStatusCode> CustomValidationErrorResponse()
        //{
        //    var errors = context.ModelState.where(error => error.value.Errors.any())
        //       .select(error => new ValidationError()
        //       {
        //           Field = error.key,
        //           Error = error.value.Errors.select(e => e.errormessage)
        //       });
        //    var response = new ValidationErrorResponse
        //    {
        //        StatusCode = (int)HttpStatusCode.BadRequest,
        //        ErrorMessage = "Validation Failed",
        //        Errors = errors
        //    };
        //    return new BadRequestObjectResult(response);
        //}
    }
}
