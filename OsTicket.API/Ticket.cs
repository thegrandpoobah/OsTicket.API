using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsTicket.API
{
    /// <summary>
    /// Represents a Ticket in the OsTicket system
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// Constructs a new Ticket
        /// </summary>
        public Ticket()
        {
            Attachments = new List<Attachment>();
            ExtraFields = new Dictionary<string, object>();
        }

        /// <summary>
        /// The e-mail address of the user submitting the ticket. This
        /// value must parse into an e-mail address otherwise OsTicket will
        /// throw an exception on submission
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The name of the user submitting the ticket.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A summary of the issue that the user is facing
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// A description of the issue that the user is facing.
        /// If IsMessageHtml is true, then this field is treated as HTML
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Whether or not to treat the Message property as an HTML fragment or plain text.
        /// By default, it is treated as Plain Text.
        /// </summary>
        public bool IsMessageHtml { get; set; }

        /// <summary>
        /// The IP Address of the user sending the message.
        /// While this is not a required property, OsTicket can be configured to blacklist
        /// users based on their IP Address.
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// The Help Topic Id of the Ticket.
        /// </summary>
        /// <remarks>
        /// OsTicket does not expose this value by default, but it can be found via inspection of the HTML
        /// or via the API extensions provided by this library.
        /// </remarks>
        public int? TopicId { get; set; }

        /// <summary>
        /// The Priority Id of the Ticket
        /// </summary>
        /// <remarks>
        /// OsTicket does not expose this value by default, but it can be found via inspection of the HTML
        /// or via the API extensions provided by this library.
        /// </remarks>
        public int? PriorityId { get; set; }

        /// <summary>
        /// The list of attachments to add to this Ticket
        /// </summary>
        public List<Attachment> Attachments { get; private set; }

        /// <summary>
        /// The list of extra fields to populate with this ticket. The keys are specified in the OsTicket UI
        /// </summary>
        public Dictionary<string, object> ExtraFields { get; private set; }
    }
}
