using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ShoppingList.Application.Api.CommandHandlers;
using ShoppingList.Application.Api.Sql;
using ShoppingList.Application.Api.ViewModels;
using ShoppingList.Application.DatabaseModel;
using ShoppingList.Application.Entities;

namespace ShoppingList.Application.Api
{
    public class ShoppingItemApiController : ApiController
    {
        [Route("api/ShoppingItemApi/GetByEventId/")]
        [HttpGet]
        public IList<ShoppingItemViewModel> GetByEventId(int id)
        {
            return new ShoppingItemSqlQueries().GetByEventId(id).Select(x => new ShoppingItemViewModel(x)).ToList();
        }

        //public ShoppingItem Post([FromBody]ShoppingItem shoppingItem)
        //{
        //    throw new NotImplementedException("Use AddUpdateItem instead.");
        //}

        [Route("api/ShoppingItemApi/AddUpdateItem/")]
        [HttpPut]
        public ShoppingItemViewModel Put([FromBody]ShoppingItemViewModel shoppingItemViewModel)
        {
            var shoppingItem = new ShoppingItem
            {
                Id = shoppingItemViewModel.Id,
                IsPurchased = shoppingItemViewModel.IsPurchased,
                ShoppingEventId = shoppingItemViewModel.ShoppingEventId
            };

            var updatedShoppingItem = new ShoppingItemTransitionHandler().AddUpdate(shoppingItem);

            var item = new Item()
            {
                Id = updatedShoppingItem.ItemId,
                Name = shoppingItemViewModel.Name,
                PictureId = shoppingItemViewModel.PictureId
            };

            var updatedItem = new ItemTransitionHandler().AddUpdate(item);

            return new ShoppingItemViewModel(updatedShoppingItem);
        }

        [Route("api/ShoppingItemApi/DeleteItem/")]
        [HttpDelete]
        public bool Delete(int id)
        {
            return new ShoppingItemTransitionHandler().Delete(id);
        }
    }
}
