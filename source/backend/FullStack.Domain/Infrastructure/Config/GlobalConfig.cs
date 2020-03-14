using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace FullStack.Domain.Infrastructure.Config
{
    [DataContract]
    [Serializable]
    [XmlRoot(ElementName = "GlobalConfig")]
    public sealed class GlobalConfig
    {
        #region Public Properties

        [DataMember, XmlElement(ElementName = "DefaultAdminPassword")]
        public string DefaultAdminPassword { get; set; }

        #endregion
    }
}
