//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PROJECTBDS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCategory()
        {
            this.tblDictionary = new HashSet<tblDictionary>();
            this.tblNews = new HashSet<tblNews>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDesc { get; set; }
        public Nullable<int> Orders { get; set; }
        public bool Delete { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblDictionary> tblDictionary { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblNews> tblNews { get; set; }
    }
}
