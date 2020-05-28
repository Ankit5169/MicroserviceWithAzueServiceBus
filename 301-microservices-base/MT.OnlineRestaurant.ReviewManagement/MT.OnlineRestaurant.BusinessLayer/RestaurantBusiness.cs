using MT.OnlineRestaurant.BusinessEntities;
using System;
using System.Collections.Generic;
using MT.OnlineRestaurant.DataLayer.Repository;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using MT.OnlineRestaurant.DataLayer.DataEntity;
using System.Text;
using System.Linq;

namespace MT.OnlineRestaurant.BusinessLayer
{
    public class RestaurantBusiness : IRestaurantBusiness
    {
        IReviewRepository review_Repository;
        private readonly string connectionstring;
        public RestaurantBusiness(IReviewRepository _reviewRepository)
        {
            review_Repository = _reviewRepository;
        }


        public IQueryable<RestaurantRatingDetails> GetRestaurantRating(int restaurantID)
        {
            try
            {
                List<RestaurantRatingDetails> restaurantRatings = new List<RestaurantRatingDetails>();
                IQueryable<TblRating> rating;

                if (restaurantID > 0)
                    rating = review_Repository.GetRestaurantRating(restaurantID);
                else
                    rating = review_Repository.GetRestaurantRating();

                foreach (var item in rating)
                {
                    RestaurantRatingDetails ratings = new RestaurantRatingDetails
                    {
                        rating = item.Rating,
                        user_Comments = item.Comments,
                        customerId = item.TblCustomerId,
                        RestaurantId = item.TblRestaurant.Id,
                        Name = item.TblRestaurant.Name,
                        ContactNo = item.TblRestaurant.ContactNo,
                        Address = item.TblRestaurant.Address,
                        Website = item.TblRestaurant.Website,
                        OpeningTime = item.TblRestaurant.OpeningTime,
                        CloseTime = item.TblRestaurant.CloseTime

                    };
                    restaurantRatings.Add(ratings);
                }
                return restaurantRatings.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Recording the customer rating the restaurants
        /// </summary>
        /// <param name=""></param>
        public bool RestaurantRating(RestaurantRating restaurantRating)
        {
            if (restaurantRating != null)
            {
                TblRating rating = new TblRating()
                {
                    Rating = restaurantRating.rating,
                    TblRestaurantId = restaurantRating.RestaurantId,
                    Comments = restaurantRating.user_Comments,
                    TblCustomerId = restaurantRating.customerId,
                    UserCreated = restaurantRating.customerId,
                    UserModified = restaurantRating.customerId

                };

                return review_Repository.RestaurantRating(rating);
            }
            return false;
        }
        public bool UpdateRestaurantRating(int ID, RestaurantRating restaurantRating)
        {
            if (restaurantRating != null)
            {
                TblRating rating = new TblRating()
                {
                    Rating = restaurantRating.rating,
                    Comments = restaurantRating.user_Comments,
                    TblCustomerId = restaurantRating.customerId,
                    UserModified = restaurantRating.customerId

                };

                return review_Repository.UpdateRestaurantRating(ID, rating);
            }
            return false;
        }
    }
}
