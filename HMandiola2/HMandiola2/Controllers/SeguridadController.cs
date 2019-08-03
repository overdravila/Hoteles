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
    public class SeguridadController : Controller
    {
        ErrorController manejoDeErrores = new ErrorController();
        // GET: Seguridad
        public ActionResult VerUsuarios()
        {
            return View();
        }

        public ActionResult CrearUsuario()
        {
            return View();
        }

        [System.Web.Http.HttpGet]
        public string devuelveUsuarios()
        {
            HotelesEntities db = new HotelesEntities();
            try
            {
                List<sproc_hoteles_GetUsuarios_List_Result> listUsers = db.sproc_hoteles_GetUsuarios_List().ToList();

                var responseModel = new UsuariosModel
                {
                    Success = true,
                    Message = "Lista completa",
                    Data = listUsers
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
        public string devuelveIdRoles()
        {
            HotelesEntities db = new HotelesEntities();
            try
            {
                List<sproc_hoteles_GetRolList_Result> listUsers = db.sproc_hoteles_GetRolList(1).ToList();

                var responseModel = new RolesModel
                {
                    Success = true,
                    Message = "Lista completa",
                    Data = listUsers
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

        [System.Web.Http.HttpPost]
        public string devuelveRolesPorUsuario(Rol_Usuario rol_Usuario)
        {
            HotelesEntities db = new HotelesEntities();
            try
            {
                List<sproc_hoteles_GetRol_Usuario_Por_Cedula_Result> listUsers = db.sproc_hoteles_GetRol_Usuario_Por_Cedula(rol_Usuario.Usuario_Cedula).ToList();

                var responseModel = new RolUsuariosModel
                {
                    Success = true,
                    Message = "Lista completa",
                    Data = listUsers
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

        [System.Web.Http.HttpPost]
        public string eliminarRolesDeUsuario(Rol_Usuario rol_Usuario_eliminar)
        {
            HotelesEntities db = new HotelesEntities();
            try
            {
                db.sproc_hoteles_DeleteRol_Usuario(rol_Usuario_eliminar.Usuario_Cedula);

                var responseModel = new RolUsuariosModel
                {
                    Success = true
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



        [System.Web.Http.HttpPost]
        public string insertaRolesPorUsuario(Rol_Usuario rol_Usuario)
        {
            HotelesEntities db = new HotelesEntities();
            try
            {
                db.sproc_hoteles_InsertRol_Usuario(rol_Usuario.Rol_ID_Rol, rol_Usuario.Usuario_Cedula);

                var responseModel = new RolUsuariosModel
                {
                    Success = true
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


        [System.Web.Http.HttpPost]
        public string RegistrarUsuario([FromBody]Usuario usuario, string repetir_contrasena)
        {
            HotelesEntities db = new HotelesEntities();

            try
            {
                db.sproc_hoteles_InsertUsuario(usuario.Cedula, usuario.Nombre, usuario.PrimerApellido, usuario.SegundoApellido, usuario.Correo, usuario.Contrasena, usuario.PreguntaSeguridad, usuario.RespuestaSeguridad);

                List<Rol> listRoles = db.Rols.ToList();
                int idRoleConsulta = 0;
                for (int x = 0; x < listRoles.Count; x++)
                {
                    if (listRoles[x].Descripcion == "Consulta")
                    {
                        idRoleConsulta = listRoles[x].ID_Rol;
                        break;
                    }
                }

                db.sproc_hoteles_InsertRol_Usuario(idRoleConsulta, usuario.Cedula);

                var responseModel = new ResponseModel
                {
                    Success = true,
                    Message = "Usuario registrado"
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
    }
}