using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FullStack.Domain.Infrastructure.Config
{
    [DataContract]
    [Serializable]
    [XmlRoot(ElementName = "Configuration")]
    public sealed class IdentityServerConfig
    {
        #region Public Properties

        [DataMember, XmlElement(ElementName = "Authority")]
        public string Authority { get; set; }

        [DataMember, XmlElement(ElementName = "Clients")]
        public IEnumerable<IdentityServerClientConfig> Clients { get; set; }

        #endregion

        #region Public Read-Only Properties

        [XmlElement(ElementName = "TokenUrl")]
        public string TokenUrl => $"{Authority}/connect/token";

        [XmlElement(ElementName = "AuthorizationUrl")]
        public string AuthorizationUrl => $"{Authority}/connect/authorize";

        #endregion
    }
}
