using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsTicket.API
{
    internal class TicketDTO
    {
        public TicketDTO()
        {
            Alert = true;
            AutoRespond = true;
            Source = "API";
        }

        public static TicketDTO CreateFromTicket(Ticket t)
        {
            TicketDTO response = new TicketDTO();

            response.Email = t.Email;
            response.Name = t.Name;
            response.Subject = t.Subject;
            response.Message = string.Format("data:{0},{1}", t.IsMessageHtml ? "text/html" : "text/plain", t.Message);
            response.TopicId = t.TopicId;
            response.Priority = t.PriorityId;

            foreach (Attachment attachment in t.Attachments)
            {
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add(attachment.Name, string.Format("data:{0};base64,{1}", attachment.Type, Convert.ToBase64String(attachment.Content)));
                response.Attachments.Add(item);
            }

            return response;
        }

        [JsonProperty(PropertyName="email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "alert")]
        public bool Alert { get; set; }
        [JsonProperty(PropertyName = "autorespond")]
        public bool AutoRespond { get; set; }
        //Don't bother sending the IP address
        //[JsonProperty(PropertyName = "ip")]
        //public string IP { get; set; }
        [JsonProperty(PropertyName = "priority")]
        public int Priority { get; set; }
        [JsonProperty(PropertyName = "source")]
        public string Source { get; set; }
        [JsonProperty(PropertyName = "topicId")]
        public int TopicId { get; set; }
        [JsonProperty(PropertyName = "attachments")]
        public List<Dictionary<string, string>> Attachments
        {
            get
            {
                if (attachments == null)
                {
                    attachments = new List<Dictionary<string, string>>();
                }
                return attachments;
            }
        }
        private List<Dictionary<string, string>> attachments;
    }
}
