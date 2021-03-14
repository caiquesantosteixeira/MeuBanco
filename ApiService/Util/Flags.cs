using System.ComponentModel;
using System.Xml.Serialization;

namespace ApiService.Util
{
    /// <summary>
    ///     Métodos
    ///     <para>1 - Get</para>
    ///     <para>2 - Post</para>
    ///     <para>3 - Put</para>
    ///     <para>4 - Delete</para>
    /// </summary>
    public enum TypeMetodo
    {
        [XmlEnum("1")]
        [Description("Get")]
        Get = 1,

        [XmlEnum("2")]
        [Description("Post")]
        Post = 2,

        [XmlEnum("3")]
        [Description("Put")]
        Put = 3,

        [XmlEnum("4")]
        [Description("Delete")]
        Delete = 4,

        [XmlEnum("5")]
        [Description("PostLote")]
        PostLote = 5,
    }

    /// <summary>
    ///  Tipos de Autenticação
    ///  <para>1 - Basic Auth</para>
    ///  <para>2 - Bearer Token</para>
    /// </summary>
    public enum TypeAuthentication
    {
        [XmlEnum("0")]
        [Description("Basic")]
        BasicToken = 0,
        [XmlEnum("1")]
        [Description("BasicAuth")]
        BasicAuth = 1,
        [XmlEnum("2")]
        [Description("Bearer")]
        BearerToken = 2,
        [XmlEnum("3")]
        [Description("None")]
        None = 3
    }

}
