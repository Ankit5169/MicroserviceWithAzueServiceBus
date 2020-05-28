using System;
using System.Collections.Generic;
using System.Text;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using System.Linq;
using MT.OnlineRestaurant.DataLayer.DataEntity;
using Microsoft.Extensions.Options;

namespace MT.OnlineRestaurant.DataLayer.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly RestaurantManagementContext db;
        public ReviewRepository(RestaurantManagementContext connection)
        {
            db = connection;
        }

        #region Interface Methods
        
        public IQueryable<TblRating> GetRestaurantRating(int restaurantID)
        {
            try
            {
                if (db != null)
                {
                    return (from rating in db.TblRating
                            join restaurant in db.TblRestaurant on
                            rating.TblRestaurantId equals restaurant.Id
                            where rating.TblRestaurantId == restaurantID
                            select new TblRating
                            {
                                Rating = rating.Rating,
                                Comments = rating.Comments,
                                TblRestaurant = restaurant,
                            }).AsQueryable();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<TblRating> GetRestaurantRating()
        {
            try
            {
                if (db != null)
                {
                    return (from rating in db.TblRating
                            join restaurant in db.TblRestaurant on
                            rating.TblRestaurantId equals restaurant.Id

                            select new TblRating
                            {
                                Rating = rating.Rating,
                                Comments = rating.Comments,
                                TblRestaurant = restaurant,
                            }).AsQueryable();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        /// <summary>
        /// Recording the customer rating the restaurants
        /// </summary>
        /// <param name="tblRating"></param>
        public bool RestaurantRating(TblRating tblRating)
        {
            try
            {
                tblRating.RecordTimeStamp = DateTime.Now;
                tblRating.RecordTimeStampCreated = DateTime.Now;

                db.Set<TblRating>().Add(tblRating);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }
        public bool UpdateRestaurantRating(int ID, TblRating tblRating)
        {
            try
            {
                
                var rating = db.TblRating.Find(ID);
                         
                if (rating != null)
                {
                    rating.Rating = tblRating.Rating;
                    rating.Comments = tblRating.Comments;
                    rating.TblCustomerId = tblRating.TblCustomerId;
                    rating.UserModified = tblRating.UserModified;
                    rating.RecordTimeStamp = DateTime.Now;

                    db.Set<TblRating>().Update(rating);
                    db.SaveChanges();

                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        #endregion

    }
}
