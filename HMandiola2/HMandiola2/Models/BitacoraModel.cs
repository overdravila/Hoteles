using HMandiola2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HMandiola2.Models
{
    public class BitacoraModel : ResponseModel
    {
        public List<sproc_hoteles_GetBitacoraList_Result> Data { get; set; }
    }
}