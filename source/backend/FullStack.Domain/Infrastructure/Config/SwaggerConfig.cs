using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace FullStack.Domain.Infrastructure.Config
{
    [DataContract]
    [Serializable, XmlRoot(ElementName = "SwaggerConfig")]
    public sealed class SwaggerConfig
    {
        #region Public Properties

        [DataMember, XmlElement(ElementName = "Title")]
        public string Title { get; set; }

        [DataMember, XmlElement(ElementName = "Version")]
        public string Version { get; set; }

        [DataMember, XmlElement(ElementName = "Description")]
        public string Description { get; set; }

        [DataMember, XmlElement(ElementName = "Document")]
        public SwaggerDocumentConfig Document { get; set; }

        #endregion
    }
}
