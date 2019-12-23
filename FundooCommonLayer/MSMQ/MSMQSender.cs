// ******************************************************************************
//  <copyright file="MSMQSender.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  MSMQSender.cs
//  
//     Purpose:  Implementing MSMQ sender
//     @author  Pranali Patil
//     @version 1.0
//     @since   17-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooCommonLayer.MSMQ
{
    // Including the requried assemblies in to the program
    using Experimental.System.Messaging;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// this class contains different methods to send message through msmq
    /// </summary>
    public class MSMQSender
    {
        /// <summary>
        /// Sends the token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="token">The token.</param>
        /// <exception cref="Exception"></exception>
        public void SendToQueue(string email, string token)
        {
            // assigning message queue to null
            MessageQueue messageQueue = null;

            // storing the queue path
            const string QueuePath = @".\Private$\EmailQueue";

            // check whether the queue is already exist or not
            if (!MessageQueue.Exists(QueuePath))
            {
                // if not then create a new queue
                messageQueue = MessageQueue.Create(QueuePath);
            }
            else
            {
                // otherwise get the existing queue path
                messageQueue = new MessageQueue(QueuePath);
            }
            try
            {
                // send the emailid and token to queue
                messageQueue.Send(email, token);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                messageQueue.Close();
            }
            Console.WriteLine("Email Sent");
        }
    }
}

