// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace ApiSdk.Models
{
    #pragma warning disable CS1591
    public class Port : IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The ip property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Ip { get; set; }
#nullable restore
#else
        public string Ip { get; set; }
#endif
        /// <summary>The privatePort property</summary>
        public int? PrivatePort { get; set; }
        /// <summary>The publicPort property</summary>
        public int? PublicPort { get; set; }
        /// <summary>The type property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Type { get; set; }
#nullable restore
#else
        public string Type { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="ApiSdk.Models.Port"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static ApiSdk.Models.Port CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new ApiSdk.Models.Port();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "ip", n => { Ip = n.GetStringValue(); } },
                { "privatePort", n => { PrivatePort = n.GetIntValue(); } },
                { "publicPort", n => { PublicPort = n.GetIntValue(); } },
                { "type", n => { Type = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("ip", Ip);
            writer.WriteIntValue("privatePort", PrivatePort);
            writer.WriteIntValue("publicPort", PublicPort);
            writer.WriteStringValue("type", Type);
        }
    }
}
