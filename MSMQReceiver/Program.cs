// ******************************************************************************
//  <copyright file="Program.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  Program.cs
//  
//     Purpose:  Implementing Events and Delegates to send email
//     @author  Pranali Patil
//     @version 1.0
//     @since   18-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace MSMQReceiver
{ // Including the requried assemblies in to the program
    using System;
    using Experimental.System.Messaging;

    /// <summary>
    /// creating delegate
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void MessageReceivedEventHandler(object sender, MessageEventArgs args);

    /// <summary>
    /// this class contains methods to handle events and delegates
    /// </summary>
    public class Program
    {
        /// <summary>
        /// this method is entry point of this application
        /// </summary>
        public static void Main()
        {
            // inititalizing queue path where messages are stored
            const string queuePath = @".\Private$\EmailQueue";

            // creating object of MSMQ listner class
            MSMQListner msmqListner = new MSMQListner(queuePath);
            msmqListner.FormatterTypes = new Type[] { typeof(string) };

            // geting message through event
            msmqListner.MessageReceived += new MessageReceivedEventHandler(ListnerMessageReceived);

            msmqListner.Start();
        }

        /// <summary>
        /// this method is used to receive the message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageEventArgs"></param>
        static void ListnerMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            Console.WriteLine("Message Received");
        }
    }

    /// <summary>
    /// this class contains different methods to handle events and delegates
    /// </summary>
    public class MSMQListner
    {
        private bool listen;
        private Type[] types;

        // creating reference of Message Queue class
        private MessageQueue queue;

        // creating event which is reference of delegate
        public event MessageReceivedEventHandler MessageReceived;

        /// <summary>
        /// constructor is used to initialize the queue path
        /// </summary>
        /// <param name="queuePath"></param>
        public MSMQListner(string queuePath)
        {
            queue = new MessageQueue(queuePath);
        }

        /// <summary>
        /// get or set the format type of message
        /// </summary>
        public Type[] FormatterTypes
        {
            get { return types; }
            set { types = value; }
        }

        /// <summary>
        /// this methode is used to start the reading the message
        /// </summary>
        public void Start()
        {
            listen = true;

            if (types != null && types.Length > 0)
            {
                queue.Formatter = new XmlMessageFormatter(types);
            }

            // featching message from queue
            queue.ReceiveCompleted += new ReceiveCompletedEventHandler(OnReceiveCompleted);
            StartListening();
            Console.ReadKey();
        }

        /// <summary>
        /// stop featching message 
        /// </summary>
        public void Stop()
        {
            listen = false;
            queue.ReceiveCompleted -= new ReceiveCompletedEventHandler(OnReceiveCompleted);
        }

        /// <summary>
        /// this method is used to peek messages from queue
        /// </summary>
        private void StartListening()
        {
            if (!listen)
            {
                return;
            }

            if (queue.Transactional)
            {
                // Initiates an asynchronous peek operation by telling Message Queuing to begin peeking a message 
                // and notify the event handler when finished.
                queue.BeginPeek();
            }
            else
            {
                // Begins to asynchronously receive data from a connected Socket.
                queue.BeginReceive();
            }
        }

        /// <summary>
        /// this method is used to indicate message is received
        /// </summary>
        /// <param name="body"></param>
        private void FireReceiveEvent(object body)
        {
            if (MessageReceived != null)
            {
                // An event that indicates that a message was received on the Datagram Socket object
                MessageReceived(this, new MessageEventArgs(body));
            }
        }

        /// <summary>
        /// this method is used to send email address and token 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            Message message = queue.EndReceive(e.AsyncResult);
            Console.WriteLine("Message: " + message.Body + message.Label);

            // creating email service class object
            EmailService emailService = new EmailService();

            // sending token and email address to email service class method 
            emailService.Email(message.Body.ToString(), message.Label.ToString());
            StartListening();
            FireReceiveEvent(message.Body);
        }
    }

    /// <summary>
    /// this class passed as an argument to the message receive Event Handler delegate.
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        private object messageBody;

        /// <summary>
        /// passing the message
        /// </summary>
        public object MessageBody
        {
            get { return messageBody; }
        }

        /// <summary>
        /// initializing the message
        /// </summary>
        /// <param name="body"></param>
        public MessageEventArgs(object body)
        {
            messageBody = body;
        }
    }
}
