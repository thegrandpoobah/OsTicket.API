using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OsTicket.API
{
    internal class TicketDTO
    {
        private TicketDTO()
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

        [JsonProperty(PropertyName = "email", Required = Required.Always)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "subject", Required = Required.Always)]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "message", Required = Required.Always)]
        public string Message { get; set; }

        [DefaultValue(true)]
        [JsonProperty(PropertyName = "alert", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Alert { get; set; }

        [DefaultValue(true)]
        [JsonProperty(PropertyName = "autorespond", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool AutoRespond { get; set; }

        [JsonProperty(PropertyName = "ip", NullValueHandling = NullValueHandling.Ignore)]
        public string IP { get; set; }

        [JsonProperty(PropertyName = "priority", NullValueHandling = NullValueHandling.Ignore)]
        public int? Priority { get; set; }

        [DefaultValue("API")]
        [JsonProperty(PropertyName = "source", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Source { get; set; }

        [JsonProperty(PropertyName = "topicId", NullValueHandling = NullValueHandling.Ignore)]
        public int? TopicId { get; set; }

        [JsonProperty(PropertyName = "attachments", NullValueHandling = NullValueHandling.Ignore)]
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
