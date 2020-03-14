using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace FullStack.Domain.Infrastructure.Config
{
    [DataContract]
    [Serializable, XmlRoot(ElementName = "Document")]
    public sealed class SwaggerDocumentConfig
    {
        #region Public Properties

        [DataMember, XmlElement(ElementName = "Path")]
        public string Path { get; set; }

        #endregion
    }
}
