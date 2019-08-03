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
    public class BitacoraController : Controller
    {
        ErrorController manejoDeErrores = new ErrorController();

        // GET: Consecutivos
        public ActionResult Index()
        {
            return View();
        }


        [System.Web.Http.HttpGet]
        public ActionResult ConsultaBitacora(int? cambio_id)
        {

            if (Session["Usuario"] != null)
            {
                Data.HotelesEntities db = new HotelesEntities();
                db.sproc_hoteles_GetBitacora(cambio_id);

                return RedirectToAction("Index", "Bitacora");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [System.Web.Http.HttpPost]
        public ActionResult ConsultaBitacora(string usuario, DateTime? fecha, string tipo)
        {
            if (Session["Usuario"] != null)
            {
                Data.HotelesEntities db = new HotelesEntities();
                db.sproc_hoteles_GetDataBitacora(usuario, fecha, tipo);

                return RedirectToAction("Index", "Bitacora");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [System.Web.Http.HttpGet]
        public string AddBitacora()
        {
            Data.HotelesEntities db = new HotelesEntities();
            try
            {
                List<sproc_hoteles_GetBitacoraList_Result> agregarBitacora = db.sproc_hoteles_GetBitacoraList().ToList();

                var responseModel = new BitacoraModel()
                {
                    Success = true,
                    Message = "Lista completa",
                    Data = agregarBitacora
                };
                return JsonConvert.SerializeObject(responseModel);
            }
            catch (DbEntityValidationException e)
            {
                return JsonConvert.SerializeObject(manejoDeErrores.errorBaseDeDatos(e));
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(manejoDeErrores.errorGeneral(ex));
            }
        }
        [System.Web.Http.HttpGet]
        public ActionResult Borrar(int? cambio_id)
        {
            Data.HotelesEntities db = new HotelesEntities();
            db.sproc_hoteles_DeleteBitacora(cambio_id);

            return RedirectToAction("Index", "Bitacora");
        }

        public ActionResult BitacoraPage(int? cambio_id)
        {
            if (cambio_id == null)
            {
                return View();
            }
            else
            {
                Data.HotelesEntities db = new HotelesEntities();
                List<sproc_hoteles_GetBitacora_Result> listaBitacora = db.sproc_hoteles_GetBitacora(cambio_id).ToList();
                ViewData["listaBitacora"] = listaBitacora;
                return View();
            }
        }

        [System.Web.Http.HttpPost]
        public string insertarBitacora([FromBody]Bitacora bitacora)
        {
            HotelesEntities db = new HotelesEntities();

            try
            {
                if (bitacora.ID_Cambio == 0)
                {
                    db.sproc_hoteles_InsertBitacora(bitacora.Usuario_Cedula, bitacora.Fecha, bitacora.ID_Registro, bitacora.Tipo, bitacora.Descripcion, bitacora.Detalles);
                }
                else
                {
                    db.sproc_hoteles_UpdateBitacora(bitacora.ID_Cambio, bitacora.Usuario_Cedula, bitacora.Fecha, bitacora.ID_Registro, bitacora.Tipo, bitacora.Descripcion, bitacora.Detalles);
                }


                var responseModel = new ResponseModel
                {
                    Success = true,
                    Message = "Cambio registrado"
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
