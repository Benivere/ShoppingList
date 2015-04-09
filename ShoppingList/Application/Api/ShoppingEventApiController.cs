using System.Collections.Generic;
using System.Web.Http;
using ShoppingList.Application.Api.CommandHandlers;
using ShoppingList.Application.Api.Sql;
using ShoppingList.Application.Api.ViewModels;
using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Api
{
    public class ShoppingEventApiController : ApiController
    {
        [Route("api/ShoppingEventApi/GetAllEvents/")]
        [HttpGet]
        public IList<ShoppingEvent> GetAllItems()
        {
            return new ShoppingEventSqlQueries().GetAllEvents();
        }

        [Route("api/ShoppingEventApi/GetEventById/")]
        [HttpGet]
        public ShoppingEvent GetById(int id)
        {
            return new ShoppingEventSqlQueries().GetByEventId(id);
        }

        //public void Post([FromBody]ShoppingList shoppingDate)
        //{
        //    throw new NotImplementedException("Use AddUpdateItem instead.");
        //}

        [Route("api/ShoppingEventApi/AddUpdate/")]
        [HttpPut]
        public ShoppingEventViewModel Put([FromBody]ShoppingEvent shoppingEvent)
        {
            return new ShoppingEventViewModel(new ShoppingEventTransitionHandler().AddUpdate(shoppingEvent));
        }

        [Route("api/ShoppingEventApi/Delete/")]
        [HttpDelete]
        public bool Delete(int id)
        {
            return new ShoppingEventTransitionHandler().Delete(id);
        }

    }
}
