//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Api_Payspace_NextGen.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Calculation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Calculation()
        {
            this.calculationDetails = new HashSet<calculationDetail>();
        }
    
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string NamePayment { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<calculationDetail> calculationDetails { get; set; }
    }
}
