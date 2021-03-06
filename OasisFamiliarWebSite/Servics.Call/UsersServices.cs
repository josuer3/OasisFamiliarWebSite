﻿using System.Linq;
using Model.DataModel;
using OasisFamiliarWebSite.Models;
using Microsoft.AspNet.Identity;
using OasisFamiliarServices.Services;
using System.Web.Security;
using System;

namespace OasisFamiliarWebSite.Servics.Call
{
    public class UsersServices
    {
        NEtUSersREpositoryCLS _manejoUsuarioRepo = new NEtUSersREpositoryCLS();
        ContextDB db = new ContextDB();
        SessionCLS sessiom = new SessionCLS();

        public bool IniciarSesion( LoginVM model)/* login vista modelo = es el modelo que se usara para la vista de login*/
        {

            try
            {
                var user = db.Usuario.Where(y => y.Nombre_Usuario == model.UUsuario).Single();
                /*SELECT * from db.Usuario where nombre-usario = model.UUsuario--- expresiones Lambda*/

                if (ValidacionPassword(user.idUsuario, model.Password))
                {
                    //    FormsAuthentication.SetAuthCookie(user.Nombre_Usuario, false);
                    sessiom.setSession(user.Nombre_Usuario, user.Role.Nombre_Rol, user);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }                                     
        }

        public bool CrearUsuario(RegisterVM model)
        {
            Usuario user = new Usuario();
            user.Nombre_Usuario = model.Nombre_Usuario;
            user.Password = model.Password;
            //user.Posicion = model.Posicion;
            //user.premio = model.premio;

            _manejoUsuarioRepo.ReistrarUsuarios(user);

            return true;
        }


        public bool ValidandoNombreDeUsuario(string Name)
        {
            bool Exito = _manejoUsuarioRepo.VerifyIfUserNameExist(Name);
                return Exito;

        }

        public bool ValidacionPassword( int iduser , string pass)
        {

            bool Exito = false;
            var hasher = new PasswordHasher();
  
            var user = db.Usuario.Where(y => y.idUsuario ==  iduser).Single();
            var result = hasher.VerifyHashedPassword(user.Password, pass);

            if (result != PasswordVerificationResult.Failed)
            {
                return Exito = true;
            }
            return Exito;

        }


        //Obtener rol
        public string getRoll(LoginVM model)
        {
            string rol = "";
            var user = db.Usuario.Where(y => y.Nombre_Usuario == model.UUsuario).Single();

            rol = user.Role.Nombre_Rol.ToString();

            return rol;
        }

    }
}