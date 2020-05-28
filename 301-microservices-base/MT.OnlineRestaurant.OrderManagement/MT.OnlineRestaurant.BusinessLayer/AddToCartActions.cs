using AutoMapper;
using Microsoft.Extensions.Options;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessEntities.ServiceModels;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.DataLayer;
using MT.OnlineRestaurant.DataLayer.interfaces;
using MT.OnlineRestaurant.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.BusinessLayer
{
    public class AddToCartActions : IAddToCartActions
    {
        // Create a field to store the mapper object
        private readonly IMapper _mapper;
        private readonly IAddToCartDbAccess _addToCartDbAccess;
        private readonly IOptions<ConnectionStrings> _connectionStrings;

        public AddToCartActions()
        {

        }

        public AddToCartActions(IAddToCartDbAccess addToCartDbAccess)
        {
            _addToCartDbAccess = addToCartDbAccess;
        }

        public AddToCartActions(IAddToCartDbAccess addToCartDbAccess, IMapper mapper, IOptions<ConnectionStrings> connectionStrings)
        {
            _addToCartDbAccess = addToCartDbAccess;
            _mapper = mapper;
            _connectionStrings = connectionStrings;
        }

        /// <summary>
        /// Place order
        /// </summary>
        /// <param name="cartEntity">Order details</param>
        /// <returns>order id</returns>
        public int AddToCart(AddToCartEntity cartEntity)
        {
            try
            {
                DataLayer.Context.TblFoodCart tblFoodCart = _mapper.Map<DataLayer.Context.TblFoodCart>(cartEntity);
                tblFoodCart.UserCreated = cartEntity.CustomerId;
                //calculate total price
                tblFoodCart.TotalPrice = TotalPrice(cartEntity);
                var OrderID = _addToCartDbAccess.AddToCart(tblFoodCart);

                List<DataLayer.Context.TblFoodCartMapping> tblFoodCartMappings = new List<DataLayer.Context.TblFoodCartMapping>();

                foreach (OrderMenus orderMenu in cartEntity.OrderMenuDetails)
                {
                    tblFoodCartMappings.Add(new DataLayer.Context.TblFoodCartMapping()
                    {
                        TblFoodCartId = OrderID,
                        TblMenuId = orderMenu.MenuId,
                        Price = orderMenu.Price,
                        UserCreated = cartEntity.CustomerId,
                        RecordTimeStampCreated = DateTime.Now
                    });
                }

                return _addToCartDbAccess.AddToCartMappingTable(tblFoodCartMappings);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        private decimal TotalPrice(AddToCartEntity cartEntity)
        {
            decimal sum = 0;
            foreach (OrderMenus orderMenu in cartEntity.OrderMenuDetails)
            {
                sum += orderMenu.Price;
            }
            return sum;
        }
        /// <summary>
        /// Cancel Order
        /// </summary>
        /// <param name="orderId">order id</param>
        /// <returns></returns>
        public int RemoveFromCart(int orderId)
        {
            return (orderId > 0 ? _addToCartDbAccess.RemoveFromCart(orderId) : 0);
        }

        
        public async Task<bool> IsValidRestaurantAsync(AddToCartEntity cartEntity, int UserId, string UserToken)
        {
            using (HttpClient httpClient = WebAPIClient.GetClient(UserToken, UserId, _connectionStrings.Value.RestaurantApiUrl))
            {
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("api/ResturantDetail?RestaurantID=" + cartEntity.RestaurantId);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string json = await httpResponseMessage.Content.ReadAsStringAsync();
                    RestaurantInformation restaurantInformation = JsonConvert.DeserializeObject<RestaurantInformation>(json);
                    if (restaurantInformation != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public async Task<bool> IsOrderItemInStock(AddToCartEntity cartEntity, int UserId, string UserToken)
        {
            using (HttpClient httpClient = WebAPIClient.GetClient(UserToken, UserId, _connectionStrings.Value.RestaurantApiUrl))
            {
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("/api/OrderDetail?RestaurantID=" + cartEntity.RestaurantId);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string json = await httpResponseMessage.Content.ReadAsStringAsync();
                    RestaurantInformation restaurantInformation = JsonConvert.DeserializeObject<RestaurantInformation>(json);
                    if (restaurantInformation != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
