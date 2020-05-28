using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MT.OnlineRestaurant.DataLayer.Context;
using MT.OnlineRestaurant.DataLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer
{
    public class AddToCartDbAccess : IAddToCartDbAccess
    {
        private readonly OrderManagementContext _context;

        public AddToCartDbAccess(OrderManagementContext context)
        {
            _context = context;
        }

        public int AddToCart(TblFoodCart foodCartDetails)
        {
            _context.TblFoodCart.Add(foodCartDetails);
            _context.SaveChanges();
            return foodCartDetails.Id;
        }
        public int AddToCartMappingTable(List<TblFoodCartMapping> tblFoodCartMappings)
        {
            _context.TblFoodCartMapping.AddRange(tblFoodCartMappings);
            _context.SaveChanges();
            return tblFoodCartMappings.Count;
        }

        public int RemoveFromCart(int Id)
        {
            var cart = _context.TblFoodCart.Include(p => p.TblFoodCartMapping)
                .SingleOrDefault(p => p.Id == Id);

            cart.TblFoodCartMapping.ToList().ForEach(p => _context.TblFoodCartMapping.Remove(p));

            _context.TblFoodCart.Remove(cart);
            _context.SaveChanges();

            return (cart != null ? cart.Id : 0);

        }

    }
}
