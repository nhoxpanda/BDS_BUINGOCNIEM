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
    
    public partial class tblProjectDetail
    {
        public int Id { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public Nullable<int> DictionaryId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Nullable<int> Orders { get; set; }
    
        public virtual tblDictionary tblDictionary { get; set; }
        public virtual tblProject tblProject { get; set; }
    }
}