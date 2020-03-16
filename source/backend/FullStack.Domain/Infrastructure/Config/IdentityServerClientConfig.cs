using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace FullStack.Domain.Infrastructure.Config
{
    [DataContract]
    [Serializable]
    [XmlRoot(ElementName = "Client")]
    public sealed class IdentityServerClientConfig
    {
        #region Public Properties

        [DataMember, XmlElement(ElementName = "ClientId")]
        public string ClientId { get; set; }

        [DataMember, XmlElement(ElementName = "ClientSecret")]
        public string ClientSecret { get; set; }

        [DataMember, XmlElement(ElementName = "AccessTokenLifetime")]
        public int AccessTokenLifetime { get; set; }

        #endregion
    }
}
