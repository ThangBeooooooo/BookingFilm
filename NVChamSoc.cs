//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookingFilm
{
    using System;
    using System.Collections.Generic;
    
    public partial class NVChamSoc
    {
        public int MaNVCS { get; set; }
        public Nullable<int> MaNV { get; set; }
    
        public virtual NhanVien NhanVien { get; set; }
    }
}
