using System;
using System.Collections.Generic;

namespace MT.OnlineRestaurant.DataLayer.Context
{
    public partial class TblFoodCartMapping
    {
        public int? TblFoodCartId { get; set; }
        public int? TblMenuId { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public int Id { get; set; }
        public int UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime RecordTimeStamp { get; set; }
        public DateTime RecordTimeStampCreated { get; set; }

        public virtual TblFoodCart TblFoodCart { get; set; }
    }
}
