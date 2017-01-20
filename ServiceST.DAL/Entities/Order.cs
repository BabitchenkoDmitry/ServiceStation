namespace ServiceST.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Order")]
    public partial class Order
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        public int car_id { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        public int client_id { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Order Date")]
        public DateTime date { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        [Range(0, 10000, ErrorMessage = "Incorrect amount")]
        [Display(Name = "Order Amount")]
        public int amount { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        [Display(Name = "Order Status")]
		public int orderstatus_id { get; set; }

        public virtual OrderStatus OrderStatus { get; set; }

        public virtual Car Car { get; set; }

        public virtual Client Client { get; set; }
    }
}
