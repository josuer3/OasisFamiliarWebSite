using Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace OasisFamiliarWebSite.Servics.Call
{
    public class MailServices
    {

        public bool SendMail(string receiver, string subject, string message) {
            try
            {
                    var senderEmail = new MailAddress("2511782015@mail.utec.edu.sv", "Oasis Familiar");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "19041997";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp-mail.outlook.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                return true;
            }
            catch (Exception)
            {
                string error = "Error parte 1";
                return false;
            }            
        }


        public bool PublicarPromo(string subject, string message) {

            try {
                using (var bd = new ContextDB())
                {
                    var clientes = bd.Usuario.Where(x => x.idRol == 1 && x.Promociones == true).ToList();

                    foreach (Usuario cliente in clientes)
                    {

                        if (cliente.Correo != null || cliente.Correo != "")
                        {

                            SendMail(cliente.Correo, subject, message);

                        }
                    }
                }

                return true;
            }
            catch (Exception) {
                return false;
            }          
        }


    }
}