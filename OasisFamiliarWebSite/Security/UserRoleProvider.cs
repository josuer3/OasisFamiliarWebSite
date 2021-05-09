using Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace OasisFamiliarWebSite.Security
{

        public class UserRoleProvider : RoleProvider
        {

            public override string ApplicationName
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public override void AddUsersToRoles(string[] usernames, string[] roleNames)
            {
                throw new NotImplementedException();
            }

            public override void CreateRole(string roleName)
            {
                throw new NotImplementedException();
            }

            public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
            {
                throw new NotImplementedException();
            }

            public override string[] FindUsersInRole(string roleName, string usernameToMatch)
            {
                throw new NotImplementedException();
            }

            public override string[] GetAllRoles()
            {
                throw new NotImplementedException();
            }

            public override string[] GetRolesForUser(string username)
            {

            var db = new ContextDB();
            string data = db.Usuario.Where(x => x.Nombre_Usuario == username).FirstOrDefault().Role.Nombre_Rol;
            string[] result = { data };
            return result;


            //using (var _Context = new ContextDB())
            //{
            //    var userRoles = (from user in _Context.Users
            //                     join roleMapping in _Context.UserRoleMappings
            //                     on user.Id equals roleMapping.UserId
            //                     join role in _Context.Roles
            //                     on roleMapping.RoleId equals role.Id
            //                     where user.Username == username
            //                     select role.RoleName).ToArray();

            //    return userRoles;
            //}
        }

            public override string[] GetUsersInRole(string roleName)
            {
                throw new NotImplementedException();
            }

            public override bool IsUserInRole(string username, string roleName)
            {
                throw new NotImplementedException();
            }

            public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
            {
                throw new NotImplementedException();
            }

            public override bool RoleExists(string roleName)
            {
                throw new NotImplementedException();
            }


        }

    
}