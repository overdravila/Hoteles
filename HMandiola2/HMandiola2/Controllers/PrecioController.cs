using HMandiola2.Classes;
using HMandiola2.Data;
using HMandiola2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace HMandiola2.Controllers
{
    public class PrecioController : Controller
    {
        ErrorController manejoDeErrores = new ErrorController();

        // GET: Consecutivos
        public ActionResult Index()
        {
            return View();
        }


        [System.Web.Http.HttpGet]
        public string devuelvePrecios()
        {
            Data.HotelesEntities db = new HotelesEntities();
            try
            {
                List<sproc_hoteles_GetPrecioList_Result> listaPrecios = db.sproc_hoteles_GetPrecioList(1).ToList();

                var responseModel = new PrecioModel()
                {
                    Success = true,
                    Message = "Lista completa",
                    Data = listaPrecios
                };
                return JsonConvert.SerializeObject(responseModel);
            }
            catch (DbEntityValidationException e)
            {
                manejoDeErrores.GuardarError(e.ToString());
                return JsonConvert.SerializeObject(manejoDeErrores.errorBaseDeDatos(e));
            }
            catch (Exception ex)
            {
                manejoDeErrores.GuardarError(ex.ToString());
                return JsonConvert.SerializeObject(manejoDeErrores.errorGeneral(ex));
            }
        }

        [System.Web.Http.HttpGet]
        public ActionResult Borrar(int? precio_id)
        {
            Data.HotelesEntities db = new HotelesEntities();
            db.sproc_hoteles_DeletePrecio(precio_id);

            return RedirectToAction("Index", "Precio");
        }


        public ActionResult PrecioPage(int? precio_id)
        {
            if (precio_id == null)
            {
                return View();
            }
            else
            {
                Data.HotelesEntities db = new HotelesEntities();
                List<sproc_hoteles_GetPrecio_Result> listaPrecios = db.sproc_hoteles_GetPrecio(precio_id).ToList();
                ViewData["listaPrecios"] = listaPrecios;
                return View();
            }
        }


        [System.Web.Http.HttpPost]
        public string insertarPrecio([FromBody]Precio precio)
        {
            HotelesEntities db = new HotelesEntities();

            try
            {
                if (precio.ID_Precio == 0)
                {
                    db.sproc_hoteles_InsertPrecio(precio.Tipo, precio.Monto);
                }
                else
                {
                    db.sproc_hoteles_UpdatePrecio(precio.ID_Precio, precio.Tipo, precio.Monto);
                }


                var responseModel = new ResponseModel
                {
                    Success = true,
                    Message = "Precio registrado"
                };

                return JsonConvert.SerializeObject(responseModel);
            }
            catch (DbEntityValidationException e)
            {
                manejoDeErrores.GuardarError(e.ToString());
                return JsonConvert.SerializeObject(manejoDeErrores.errorBaseDeDatos(e));
            }
            catch (Exception ex)
            {
                manejoDeErrores.GuardarError(ex.ToString());
                return JsonConvert.SerializeObject(manejoDeErrores.errorGeneral(ex));
            }
        }
    }
}