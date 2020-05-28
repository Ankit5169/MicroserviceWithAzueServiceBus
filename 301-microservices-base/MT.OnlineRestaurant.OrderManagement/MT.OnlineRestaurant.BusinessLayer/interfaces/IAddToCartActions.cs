using MT.OnlineRestaurant.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.BusinessLayer.interfaces
{
    public interface IAddToCartActions
    {
        int AddToCart(AddToCartEntity cartEntity);
        int RemoveFromCart(int orderId);
        Task<bool> IsValidRestaurantAsync(AddToCartEntity cartEntity, int UserId, string UserToken);
        Task<bool> IsOrderItemInStock(AddToCartEntity cartEntity, int UserId, string UserToken);

    }
}
