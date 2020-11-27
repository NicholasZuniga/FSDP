using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FSDP.UI.MVC.Models;

namespace FSDP.UI.MVC.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Contact(ContactViewModel cvm)
        {
            //When a class has validation attributes , that validation should be checked BEFORE attempting to process any data
            if (!ModelState.IsValid)
            {
                //Send them back to the form and pass teh object they created to the View.
                //This restores the info they typed in the textboxes so they don't have to type it again.
                return View(cvm);
            }
            // GET: Contact
            //Steps to send an email
            //1) Create a string for the message
            string emailBody = $"You have recieved an email from {cvm.Name} with a subject {cvm.Subject}. Please respond to {cvm.Email} with your reponse to the following: <br/> <br /> {cvm.Message}";
            //2) Create email message object
            MailMessage msg = new MailMessage(
                //From
                "no-reply@nicholaszuniga.com",
                //TO, where message is sent
                "Nicholas.M.Zuniga@outlook.com",
                "Email From no-reply@nicholaszuniga.com",
                emailBody
                );
            //3) (Optional Customize Mail Object)
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;


            //4) Create SMTP Client
            SmtpClient client = new SmtpClient("mail.nicholaszuniga.com");
            client.Credentials = new NetworkCredential("no-reply@nicholaszuniga.com", "Deamus58!");


            //5) Attempt to send the  email
            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Sorry, Something went wrong! Error message: {ex.Message}<br />{ex.StackTrace}";
                return View(cvm);
            }

            return View("EmailConfirmation", cvm);
        }
    }
}