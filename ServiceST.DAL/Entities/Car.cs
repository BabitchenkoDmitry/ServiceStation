namespace ServiceST.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Car")]
    public partial class Car
    {
        public Car()
        {
            Order = new HashSet<Order>();
        }

        public int id { get; set; }

        [Required(ErrorMessage="Required to fill")]
        [StringLength(50)]
        [Display(Name = "Make")]
        public string make { get; set; }

        [Required(ErrorMessage="Required to fill")]
        [StringLength(50)]
        [Display(Name = "Model")]
        public string model { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        [Range(1900, 2100, ErrorMessage = "Incorrect year")]
        [Display(Name = "Year")]
        public int year { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        [StringLength(20)]
        [Display(Name = "VIN")]
        public string vin { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        public int client_id { get; set; }

        public virtual Client Client { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
