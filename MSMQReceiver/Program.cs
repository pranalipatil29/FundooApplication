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
    /// <param name="sender"> sender object</param>
    /// <param name="args"> reference of message event args class</param>
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
            const string QueuePath = @".\Private$\EmailQueue";

            // creating object of MSMQ listner class
            MSMQListner msmqListner = new MSMQListner(QueuePath);
            msmqListner.FormatterTypes = new Type[] { typeof(string) };

            // geting message through event
            msmqListner.MessageReceived += new MessageReceivedEventHandler(ListnerMessageReceived);

            msmqListner.Start();
        }

        /// <summary>
        /// this method is used to receive the message
        /// </summary>
        /// <param name="sender"> sender object</param>
        /// <param name="messageEventArgs">reference of message event args class</param>
        public static void ListnerMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            Console.WriteLine("Message Received");
        }
    }

    /// <summary>
    /// this class contains different methods to handle events and delegates
    /// </summary>
    public class MSMQListner
    {
        /// <summary>
        /// The listen
        /// </summary>
        private bool listen;

        /// <summary>
        /// The types
        /// </summary>
        private Type[] types;

        /// <summary>
        ///  creating reference of Message Queue class
        /// </summary>
        private MessageQueue queue;

        /// <summary>
        /// Initializes a new instance of the <see cref="MSMQListner"/> class.
        /// </summary>
        /// <param name="queuePath">The queue path.</param>
        public MSMQListner(string queuePath)
        {
            this.queue = new MessageQueue(queuePath);
        }

        /// <summary>
        /// Occurs when [message received].
        /// </summary>
        public event MessageReceivedEventHandler MessageReceived;

        /// <summary>
        /// Gets or sets the formatter types.
        /// </summary>
        /// <value>
        /// The formatter types.
        /// </value>
        public Type[] FormatterTypes
        {
            get { return this.types; }
            set { this.types = value; }
        }

        /// <summary>
        /// this method is used to start the reading the message
        /// </summary>
        public void Start()
        {
            this.listen = true;

            if (this.types != null && this.types.Length > 0)
            {
                this.queue.Formatter = new XmlMessageFormatter(this.types);
            }

            // featching message from queue
            this.queue.ReceiveCompleted += new ReceiveCompletedEventHandler(this.OnReceiveCompleted);
            this.StartListening();
            Console.ReadKey();
        }

        /// <summary>
        /// stop fetching message 
        /// </summary>
        public void Stop()
        {
            this.listen = false;
            this.queue.ReceiveCompleted -= new ReceiveCompletedEventHandler(this.OnReceiveCompleted);
        }

        /// <summary>
        /// this method is used to peek messages from queue
        /// </summary>
        private void StartListening()
        {
            if (!this.listen)
            {
                return;
            }

            if (this.queue.Transactional)
            {
                // Initiates an asynchronous peek operation by telling Message Queuing to begin peeking a message 
                // and notify the event handler when finished.
                this.queue.BeginPeek();
            }
            else
            {
                // Begins to asynchronously receive data from a connected Socket.
                this.queue.BeginReceive();
            }
        }

        /// <summary>
        /// this method is used to indicate message is received
        /// </summary>
        /// <param name="body"> object type parameter</param>
        private void FireReceiveEvent(object body)
        {
            if (this.MessageReceived != null)
            {
                // An event that indicates that a message was received on the Datagram Socket object
                this.MessageReceived(this, new MessageEventArgs(body));
            }
        }

        /// <summary>
        /// this method is used to send email address and token 
        /// </summary>
        /// <param name="sender"> object type sender</param>
        /// <param name="e"> reference of receive complete event class</param>
        private void OnReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            Message message = this.queue.EndReceive(e.AsyncResult);
            Console.WriteLine("Message: " + message.Body + message.Label);

            // creating email service class object
            EmailService emailService = new EmailService();

            // sending token and email address to email service class method 
            emailService.Email(message.Body.ToString(), message.Label.ToString());
            this.StartListening();
            this.FireReceiveEvent(message.Body);
        }
    }

    /// <summary>
    /// this class passed as an argument to the message receive Event Handler delegate.
    /// </summary>
    public class MessageEventArgs : EventArgs
    {      
        /// <summary>
        /// The message body
        /// </summary>
        private object messageBody;

        /// <summary>
        /// Gets the message body.
        /// </summary>
        /// <value>
        /// The message body.
        /// </value>
        public object MessageBody
        {
            get { return this.messageBody; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEventArgs"/> class.
        /// </summary>
        /// <param name="body">The body.</param>
        public MessageEventArgs(object body)
        {
            this.messageBody = body;
        }
    }
}
