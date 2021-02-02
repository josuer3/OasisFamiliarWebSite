using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataModel;
using System.Security.Cryptography;

namespace OasisFamiliarServices.Services
{
   public  class NEtUSersREpositoryCLS
    {
        ContextDB db = new ContextDB();

        public void ReistrarUsuarios(Usuario model)
        {
            model.Password = Encrypt(model.Password);
            model.idRol = 1; //Siempre se asignara el Rol de Cliente, en su primera creación

            try
            {
                db.Usuario.Add(model);
                db.SaveChanges();
            }
            catch (Exception e) { };
            
        }

        public bool VerifyIfUserNameExist(string Name)
        {
            int UserResult = db.Usuario.Where(x => x.Nombre_Usuario == Name).Count();
            if (UserResult > 0)
            {
                return false;
            }
            return true;
        }

        public string Encrypt(string Password)
        {
            byte[] salt;
            byte[] bytes;

            if (Password == null)
            {
                Password = "DummyPass";
            }
            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(Password, 16, 1000))
            {
                salt = rfc2898DeriveBytes.Salt;
                bytes = rfc2898DeriveBytes.GetBytes(32);
            }
            byte[] numArray = new byte[49];
            Buffer.BlockCopy(salt, 0, numArray, 1, 16);
            Buffer.BlockCopy(bytes, 0, numArray, 17, 32);
            return Convert.ToBase64String(numArray);
        }
    }
}
