using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOs.BasketModuleDto;

namespace Presentation.Controllers
{
  
    public class BasketController(IServiceManager serviceManager) : ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string Key)
        {
            var basket = await serviceManager.BasketService.GetBasketAsync(Key);
            return Ok(basket);
        }

        // Create or Update Basket
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket = await serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }
        [HttpDelete("{Key}")] // DELETE  BaseUrl/api/Basket/{Key}
        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        {
            var Result = await serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(Result);
        }
    }
}
