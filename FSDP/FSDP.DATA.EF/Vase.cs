//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FSDP.DATA.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vase()
        {
            this.Reservations = new HashSet<Reservation>();
        }
    
        public int VaseID { get; set; }
        public string VaseMaterial { get; set; }
        public string OwnerId { get; set; }
        public string VasePhoto { get; set; }
        public string SpecialNotes { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime DateAdded { get; set; }
    
        public virtual OwnerDetail OwnerDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
