using MT.OnlineRestaurant.DataLayer.DataEntity;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using System.Collections.Generic;
using System.Linq;

namespace MT.OnlineRestaurant.DataLayer.Repository
{
    public interface IReviewRepository
    {
        //TblRestaurant GetResturantDetails(int restaurantID);
        IQueryable<TblRating> GetRestaurantRating(int restaurantID);
        IQueryable<TblRating> GetRestaurantRating();
        //IQueryable<MenuDetails> GetRestaurantMenu(int restaurantID);

        //IQueryable<TblRestaurantDetails> GetTableDetails(int restaurantID);
        //IQueryable<RestaurantSearchDetails> GetRestaurantsBasedOnLocation(LocationDetails location_Details);
        //IQueryable<RestaurantSearchDetails> GetRestaurantsBasedOnMenu(AddtitionalFeatureForSearch searchDetails);
        //IQueryable<RestaurantSearchDetails> SearchForRestaurant(SearchForRestautrant searchDetails);
        //IQueryable<RestaurantSearchDetails> SearchForRestaurants(SearchForRestautrant searchDetails);

        /// <summary>
        /// Recording the customer rating the restaurants
        /// </summary>
        /// <param name="tblRating"></param>
        bool RestaurantRating(TblRating tblRating);
        bool UpdateRestaurantRating(int ID, TblRating tblRating);
        //TblMenu ItemInStock(int restaurantID,int MenuID);

    }
}
