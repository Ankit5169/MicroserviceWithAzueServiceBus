using System;
using System.Collections.Generic;

namespace MT.OnlineRestaurant.DataLayer.Context
{
    public partial class TblFoodCart
    {
        public TblFoodCart()
        {
            TblFoodCartMapping = new HashSet<TblFoodCartMapping>();
        }

        public int? TblCustomerId { get; set; }
        public int? TblRestaurantId { get; set; }
        public decimal TotalPrice { get; set; }
        public int Id { get; set; }
        public int UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime RecordTimeStamp { get; set; }
        public DateTime RecordTimeStampCreated { get; set; }

        public virtual ICollection<TblFoodCartMapping> TblFoodCartMapping { get; set; }
        
    }
}
