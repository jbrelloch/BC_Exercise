using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using BC_Exercise.Core.Interfaces;
using BC_Exercise.Models.User;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions.MonoHttp;

namespace BC_Exercise.Core.Services
{
    public class ExternalService : IExternalService
    {
        private static string _ApiKey = @"AIzaSyDkkKVIb22PqxWdHNyj-3P4byYrgQksAX4";

        /// <summary>
        /// Gets the address string from object.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The address.</returns>
        public string GetAddressStrFromObject(User model)
        {
            return model.StreetAddress + ", " + model.City + ", " + model.State + " " + model.Zip;
        }

        /// <summary>
        /// Gets the coordinates from address.
        /// </summary>
        /// <param name="addr">The address.</param>
        /// <returns>The coordinates.</returns>
        public string GetCoordinatesFromAddress(string addr)
        {
            var client = new RestClient("https://maps.googleapis.com/maps/api");

            var request = new RestRequest("geocode/json?address=" + HttpUtility.UrlEncode(addr) + "&key=" + _ApiKey);
            request.Method = Method.GET;

            var response = client.Execute(request);

            dynamic parsedResponse = JsonConvert.DeserializeObject(response.Content);

            if (parsedResponse == null || parsedResponse.status != "OK")
            {
                return null;
            }
            else
            {
                string latFinder = "\"lat\" : ";
                var latIndex = response.Content.IndexOf(latFinder);
                var lat = response.Content.Substring(latIndex+latFinder.Length, 8);
                string lngFinder = "\"lng\" : ";
                var lngIndex = response.Content.IndexOf(lngFinder);
                var lng = response.Content.Substring(lngIndex + lngFinder.Length, 8);

                return lat + ", " + lng;
            }
        }
    }
}
