//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Motorcycle_Operation
    {
        public int CoID { get; set; }
        public Nullable<int> CID { get; set; }
        [Required]
        public Nullable<System.DateTime> TimeDate { get; set; }
        [Required]
        public string MotorcycleType { get; set; }
    
        public virtual Sign Sign { get; set; }
    }
}
