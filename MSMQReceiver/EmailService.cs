// ******************************************************************************
//  <copyright file="EmailService.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  EmailService.cs
//  
//     Purpose:  Implementing program to handle email services using SMTP
//     @author  Pranali Patil
//     @version 1.0
//     @since   18-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace MSMQReceiver
{ // Including the requried assemblies in to the program
    using Experimental.System.Messaging;
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;
    using System.Text;

    /// <summary>
    /// this class containes different methods to implementing email services
    /// </summary>
    public class EmailService
    {
        /// <summary>
        /// Emails the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="token">The token.</param>
        public void Email(string email, string token)
        {
            // storing the queue path
            const string QueuePath = @".\Private$\EmailQueue";

            // creating object of message queue class and passing it queue path to assess messages
            MessageQueue messageQueue = new MessageQueue(QueuePath);

            // initializing url address of forget password
            string url = "https://localhost:44345/api/Account/ResetPassword";

            //  used to serialize an object into or deserialize an object from the body of a message read from or written to the queue.
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            // creating object of Mail message class
            MailMessage mail = new MailMessage();

            // initializing host 
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = true;

            mail.From = new MailAddress(email);
            mail.To.Add(email);
            mail.Subject = "Link for Reseting Password";
            mail.Body = "Click on link " + url + " to reset the password  Token: " + token;

            // Gets or sets the credentials used to authenticate the sender
            smtpClient.Credentials = new System.Net.NetworkCredential("pranalipatil2996@gmail.com", "nohalijai2996");
            smtpClient.EnableSsl = true;

            // sending email
            smtpClient.Send(mail);
            Console.WriteLine("link has been sent to your mail....");
        }
    }
}
