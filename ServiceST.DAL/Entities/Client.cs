namespace ServiceST.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Client")]
    public partial class Client
    {
        public Client()
        {
            Car = new HashSet<Car>();
            Order = new HashSet<Order>();
        }

        public int id { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string first_name { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string last_name { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime birthday { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        [StringLength(50)]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        [Display(Name = "Phone")]
        public int phone { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email")]
        public string email { get; set; }

        public virtual ICollection<Car> Car { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
