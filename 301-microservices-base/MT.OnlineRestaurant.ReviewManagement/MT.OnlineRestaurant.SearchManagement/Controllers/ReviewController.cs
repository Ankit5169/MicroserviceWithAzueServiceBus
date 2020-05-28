using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer;


namespace MT.OnlineRestaurant.ReviewManagement.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class ReviewController : Controller
    {
        private readonly IRestaurantBusiness business_Repo;

        public ReviewController(IRestaurantBusiness _business_Repo)
        {
            business_Repo = _business_Repo;
        }

        [HttpGet]
        [Route("ResturantRating")]
        public IActionResult GetResturantRating([FromQuery] int RestaurantID)
        {
            IQueryable<RestaurantRatingDetails> restaurantRatings;
            restaurantRatings = business_Repo.GetRestaurantRating(RestaurantID);
            if (restaurantRatings != null)
            {
                return this.Ok(restaurantRatings);
            }

            return this.StatusCode((int)HttpStatusCode.InternalServerError, string.Empty);
        }

        [HttpPost]
        [Route("ResturantRating")]
        public IActionResult ResturantRating([FromBody] RestaurantRating restaurantRating)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            if (business_Repo.RestaurantRating(restaurantRating))
                return this.Ok("Submitted the reviewes");
            else
                return this.StatusCode((int)HttpStatusCode.InternalServerError, "Unable to process request.");
        }

        [HttpPut]
        [Route("ResturantRating/{ID:int}")]
        public IActionResult UpdateResturantRating(int ID, [FromBody] RestaurantRating restaurantRating)
        {
            if ((!ModelState.IsValid) || ID <= 0)
            {
                return this.BadRequest();
            }

            if (business_Repo.UpdateRestaurantRating(ID, restaurantRating))
                return this.Ok("Updated the reviewes");
            else
                return this.StatusCode((int)HttpStatusCode.InternalServerError, "Unable to process request.");
        }

    }
}