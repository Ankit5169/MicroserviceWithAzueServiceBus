using MT.OnlineRestaurant.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT.OnlineRestaurant.BusinessLayer
{
    public interface IRestaurantBusiness
    {

        IQueryable<RestaurantRatingDetails> GetRestaurantRating(int restaurantID);
        /// <summary>
        /// Recording the customer rating the restaurants
        /// </summary>
        /// <param name=""></param>
        bool RestaurantRating(RestaurantRating restaurantRating);
        bool UpdateRestaurantRating(int ID, RestaurantRating restaurantRating);


    }
}
