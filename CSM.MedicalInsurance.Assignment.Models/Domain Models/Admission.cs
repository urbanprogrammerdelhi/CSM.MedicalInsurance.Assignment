//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Admission
    {
        public int id { get; set; }
        public Nullable<int> patientid { get; set; }
        public Nullable<System.DateTime> dateofadmission { get; set; }
        public Nullable<System.DateTime> dateofdischarge { get; set; }
        public string patientstatus { get; set; }
        public string insurancepaymentstatus { get; set; }
    }
}
