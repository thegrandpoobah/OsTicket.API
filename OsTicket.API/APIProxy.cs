using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OsTicket.API
{
    public class APIProxy
    {
        public APIProxy(string osTicketUrl, string apiKey)
        {
            this.OsTicketUrl = osTicketUrl;
            this.ApiKey = apiKey;
        }

        public Dictionary<int, string> GetPriorities()
        {
            RestClient client = new RestClient(OsTicketUrl);

            IRestRequest request = new RestRequest("/api/priority.php", Method.POST);
            request.AddHeader("X-API-KEY", ApiKey);

            IRestResponse response = client.Execute(request);

            JObject root = JObject.Parse(response.Content);
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<Dictionary<int, string>>(root.CreateReader());
        }

        public Dictionary<int, string> GetTopics()
        {
            RestClient client = new RestClient(OsTicketUrl);

            IRestRequest request = new RestRequest("/api/topic.php", Method.POST);
            request.AddHeader("X-API-KEY", ApiKey);

            IRestResponse response = client.Execute(request);

            JObject root = JObject.Parse(response.Content);
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<Dictionary<int, string>>(root.CreateReader());
        }

        public int SubmitTicket(Ticket ticket)
        {
            RestClient client = new RestClient(OsTicketUrl);

            IRestRequest request = new RestRequest("/api/tickets.json", Method.POST);
            request.JsonSerializer = new RestSharp.Serializers.NetwonsoftJsonSerializer();
            request.AddHeader("X-API-KEY", ApiKey);
            request.RequestFormat = DataFormat.Json;

            TicketDTO ticketDTO = TicketDTO.CreateFromTicket(ticket);
            JObject jo = JObject.FromObject(ticketDTO);
            foreach (KeyValuePair<string, object> pair in ticket.ExtraFields)
            {
                jo.Add(pair.Key, JToken.FromObject(pair.Value));
            }

            request.AddBody(jo);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return int.Parse(response.Content);
            }
            else
            {
                throw new Exception(response.Content);
            }
        }

        public string OsTicketUrl
        {
            get;
            private set;
        }

        public string ApiKey
        {
            get;
            private set;
        }
    }
}
