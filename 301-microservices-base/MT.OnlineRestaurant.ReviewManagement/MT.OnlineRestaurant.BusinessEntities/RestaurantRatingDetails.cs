using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
    public class RestaurantRatingDetails
    {
        public int RestaurantId { get; set; }
        public string rating { get; set; }
        public string user_Comments { get; set; }
        public int customerId { get; set; }
        
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string OpeningTime { get; set; }
        public string CloseTime { get; set; }

    }
}
