using System.Xml.Serialization;

namespace MiddlewareNz.Evaluation.WebApi.Clients
{
    /// <summary>
    /// Schema mapping class for deserializing responses from the Company XML API. 
    /// </summary>
    [XmlRoot("Data")]
    public class XmlCompany
    {
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("description")]
        public string Description { get; set; }
    }
}