//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ThuongMaiDienTu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.Ratings = new HashSet<Rating>();
            Image = "~/Content/Image/logo.PNG";
            Image2 = "~/Content/Image/logo.PNG";
            Image3 = "~/Content/Image/logo.PNG";
            Image4 = "~/Content/Image/logo.PNG";
            Image5 = "~/Content/Image/logo.PNG";
            Image6 = "~/Content/Image/logo.PNG";
        }
    
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string FullName { get; set; }
        public Nullable<int> Price { get; set; }
        public string Screen { get; set; }
        public string ROM { get; set; }
        public string RAM { get; set; }
        public string FrontCamera { get; set; }
        public string BehindCamera { get; set; }
        public string Battery { get; set; }
        public Nullable<int> BrandID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<System.DateTime> ImportDate { get; set; }
        public string Chipset { get; set; }
        public string SIMCard { get; set; }
        public string OS { get; set; }
        public string Image { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public string Description { get; set; }
        public string Design { get; set; }
        public string Image6 { get; set; }
        public string Reason { get; set; }
    
        public virtual Brand Brand { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rating> Ratings { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload2 { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload3 { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload4 { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload5 { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload6 { get; set; }
    }
}
