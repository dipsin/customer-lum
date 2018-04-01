//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace CustomeInfo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Customer
    {
        //[Required]  

        [Key]
        public int CustomerID { get; set; }
        [Required]
        [StringLength(15)]
        public string CustomerName { get; set; }
        [Required]
        [Range(12,100)]
        public int CustomerAge { get; set; }
        [EmailAddress]
        public string CustomerEmail { get; set; }
        public byte[] CustomerImage { get; set; }
        [NotMapped]
        public string imagePath { get; set; }
    }
}