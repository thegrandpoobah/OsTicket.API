using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsTicket.API
{
    public class Ticket
    {
        public Ticket()
        {
            Attachments = new List<Attachment>();
            ExtraFields = new Dictionary<string, object>();
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsMessageHtml { get; set; }
        public int TopicId { get; set; }
        public int PriorityId { get; set; }
        public List<Attachment> Attachments { get; private set; }
        public Dictionary<string, object> ExtraFields { get; private set; }
    }
}
