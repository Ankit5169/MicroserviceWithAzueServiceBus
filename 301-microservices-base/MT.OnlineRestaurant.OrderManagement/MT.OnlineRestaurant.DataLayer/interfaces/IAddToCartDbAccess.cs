using MT.OnlineRestaurant.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.interfaces
{
    public interface IAddToCartDbAccess
    {
        int AddToCart(TblFoodCart foodCartDetails);
        int RemoveFromCart(int Id);
        int AddToCartMappingTable(List<TblFoodCartMapping> tblFoodCartMappings);
    }
}
