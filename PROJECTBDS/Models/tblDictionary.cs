
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
    
public partial class tblDictionary
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public tblDictionary()
    {

        this.tblImage = new HashSet<tblImage>();

        this.tblLand = new HashSet<tblLand>();

        this.tblLand1 = new HashSet<tblLand>();

        this.tblLand2 = new HashSet<tblLand>();

        this.tblLand3 = new HashSet<tblLand>();

        this.tblLand4 = new HashSet<tblLand>();

        this.tblNews = new HashSet<tblNews>();

        this.tblNews1 = new HashSet<tblNews>();

        this.tblProjectDetail = new HashSet<tblProjectDetail>();

    }


    public int Id { get; set; }

    public string Title { get; set; }

    public string MetaTitle { get; set; }

    public string MetaDesc { get; set; }

    public Nullable<int> Orders { get; set; }

    public bool Delete { get; set; }

    public Nullable<int> CategoryId { get; set; }



    public virtual tblCategory tblCategory { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<tblImage> tblImage { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<tblLand> tblLand { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<tblLand> tblLand1 { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<tblLand> tblLand2 { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<tblLand> tblLand3 { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<tblLand> tblLand4 { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<tblNews> tblNews { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<tblNews> tblNews1 { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<tblProjectDetail> tblProjectDetail { get; set; }

}

}
