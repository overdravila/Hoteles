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
    
    public partial class Usuario
    {
        public Usuario()
        {
            this.Bitacoras = new HashSet<Bitacora>();
        }
    
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string PreguntaSeguridad { get; set; }
        public string RespuestaSeguridad { get; set; }
    
        public virtual ICollection<Bitacora> Bitacoras { get; set; }
    }
}
