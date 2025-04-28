using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

namespace Presentation.Controllers
{
    public class PaymentsController(IServiceManager serviceManager):ApiController
    {
        #region CreateOrUpdatePaymentIntend
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntend(string basketId)
        {
            var result= await serviceManager.paymentService.CreateOrUpdatePaymentAsync(basketId);
            return Ok(result);
        }
        #endregion
    }
}
