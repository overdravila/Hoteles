//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HMandiola2.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Precio
    {
        public Precio()
        {
            this.Articuloes = new HashSet<Articulo>();
        }
    
        public int ID_Precio { get; set; }
        public string Tipo { get; set; }
        public decimal Monto { get; set; }
    
        public virtual ICollection<Articulo> Articuloes { get; set; }
    }
}