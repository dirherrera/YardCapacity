//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Yard
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int YardId { get; set; }
        public string Yard1 { get; set; }
        public string max_equipment { get; set; }
        public string max_units { get; set; }
        public string cur_equipment { get; set; }
        public string cur_units { get; set; }
        public System.DateTime update { get; set; }
    }
}
