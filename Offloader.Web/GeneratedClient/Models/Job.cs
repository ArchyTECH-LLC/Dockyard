// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace ApiSdk.Models
{
    #pragma warning disable CS1591
    public class Job : IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The args property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public UntypedNode? Args { get; private set; }
#nullable restore
#else
        public UntypedNode Args { get; private set; }
#endif
        /// <summary>The arguments property</summary>
        [Obsolete("")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Arguments { get; private set; }
#nullable restore
#else
        public List<string> Arguments { get; private set; }
#endif
        /// <summary>The method property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ApiSdk.Models.MethodInfo? Method { get; set; }
#nullable restore
#else
        public ApiSdk.Models.MethodInfo Method { get; set; }
#endif
        /// <summary>The queue property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Queue { get; private set; }
#nullable restore
#else
        public string Queue { get; private set; }
#endif
        /// <summary>The type property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ApiSdk.Models.TypeObject? Type { get; set; }
#nullable restore
#else
        public ApiSdk.Models.TypeObject Type { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="ApiSdk.Models.Job"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static ApiSdk.Models.Job CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new ApiSdk.Models.Job();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "args", n => { Args = n.GetObjectValue<UntypedNode>(UntypedNode.CreateFromDiscriminatorValue); } },
                { "arguments", n => { Arguments = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
                { "method", n => { Method = n.GetObjectValue<ApiSdk.Models.MethodInfo>(ApiSdk.Models.MethodInfo.CreateFromDiscriminatorValue); } },
                { "queue", n => { Queue = n.GetStringValue(); } },
                { "type", n => { Type = n.GetObjectValue<ApiSdk.Models.TypeObject>(ApiSdk.Models.TypeObject.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<ApiSdk.Models.MethodInfo>("method", Method);
            writer.WriteObjectValue<ApiSdk.Models.TypeObject>("type", Type);
        }
    }
}