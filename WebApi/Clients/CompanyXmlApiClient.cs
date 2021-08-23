using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;

namespace MiddlewareNz.Evaluation.WebApi.Clients
{
    /// <summary>
    /// Client for the Middleware XML API.  
    /// </summary>
    public class CompanyXmlApiClient : ICompanyXmlApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CompanyXmlApiClient> _logger;

        public CompanyXmlApiClient(HttpClient httpClient, ILogger<CompanyXmlApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// Get a company by id. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ApiClientResponse<XmlCompany>> Get(int id, CancellationToken cancellationToken)
        {
            var resource = $"{id}.xml";
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.GetAsync(resource, cancellationToken);
            }
            catch (HttpRequestException e)
            {
                var resourceUri = $"{_httpClient.BaseAddress}{resource}";
                _logger.LogError(e, "Failed to fetch from {resourceUri}", resourceUri);

                return new ApiClientResponse<XmlCompany>(HttpStatusCode.InternalServerError, e.Message);
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new ApiClientResponse<XmlCompany>(HttpStatusCode.NotFound);
            }

            if (!response.IsSuccessStatusCode)
            {
                return new ApiClientResponse<XmlCompany>(response.StatusCode, response.ReasonPhrase);
            }

            var xml = await response.Content.ReadAsStringAsync(cancellationToken);
            
            if (!TryDeserializeXml(xml, out XmlCompany xmlCompany))
            {
                _logger.LogError($"Failed to deserialize response from {_httpClient.BaseAddress}{resource}");

                return new ApiClientResponse<XmlCompany>(HttpStatusCode.InternalServerError, "Failed to deserialize the response.");
            }

            return new ApiClientResponse<XmlCompany>(HttpStatusCode.OK, xmlCompany);
        }

        private bool TryDeserializeXml<T>(string xml, out T model) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            using var stringReader = new System.IO.StringReader(xml);
            using var xmlReader = XmlReader.Create(stringReader);

            try
            {
                model = (T)serializer.Deserialize(xmlReader);
                if (model == null) return false;
                return true;
            }
            catch (InvalidOperationException)
            {
                model = null;
                return false;
            }
        }
    }
}
