//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Originarios.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Postagem
    {
        public int id_post { get; set; }
        public int usuario { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public string corpo { get; set; }
        public string nm_img1 { get; set; }
        public byte[] vb_img1 { get; set; }
        public string nm_img2 { get; set; }
        public byte[] vb_img2 { get; set; }
        public string nm_img3 { get; set; }
        public byte[] vb_img3 { get; set; }
        public string nm_img4 { get; set; }
        public byte[] vb_img4 { get; set; }
    
        public virtual Usuario Usuario1 { get; set; }
    }
}
