using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Basket;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

namespace Presentation.Controllers
{
  
    public class BasketController(IServiceManager _serviceManager) : ApiController
    {
        #region CreateOrUpdate
        [HttpPost]
        public async Task<ActionResult<BasketDto>> UpdateBasket(BasketDto basket)
        {
            var result = await _serviceManager.BasketService.UpdateBasketAsync(basket);
            return Ok(result);
        }
            #endregion
            #region Get
            [HttpGet("{id}")]
        public async Task<ActionResult<BasketDto>> GetBasketById(string id)
        {
            var item = await _serviceManager.BasketService.GetBasketAsync(id);
            return Ok(item);
        }
        #endregion
        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<BasketDto>> DeleteBasket (string id)
        {
             await _serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }
        #endregion
    }
}
