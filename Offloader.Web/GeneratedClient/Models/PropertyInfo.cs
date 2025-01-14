// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace ApiSdk.Models
{
    #pragma warning disable CS1591
    public class PropertyInfo : IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The attributes property</summary>
        public int? Attributes { get; set; }
        /// <summary>The canRead property</summary>
        public bool? CanRead { get; private set; }
        /// <summary>The canWrite property</summary>
        public bool? CanWrite { get; private set; }
        /// <summary>The customAttributes property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ApiSdk.Models.CustomAttributeData>? CustomAttributes { get; private set; }
#nullable restore
#else
        public List<ApiSdk.Models.CustomAttributeData> CustomAttributes { get; private set; }
#endif
        /// <summary>The declaringType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ApiSdk.Models.TypeObject? DeclaringType { get; set; }
#nullable restore
#else
        public ApiSdk.Models.TypeObject DeclaringType { get; set; }
#endif
        /// <summary>The getMethod property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ApiSdk.Models.MethodInfo? GetMethod { get; set; }
#nullable restore
#else
        public ApiSdk.Models.MethodInfo GetMethod { get; set; }
#endif
        /// <summary>The isCollectible property</summary>
        public bool? IsCollectible { get; private set; }
        /// <summary>The isSpecialName property</summary>
        public bool? IsSpecialName { get; private set; }
        /// <summary>The memberType property</summary>
        public int? MemberType { get; set; }
        /// <summary>The metadataToken property</summary>
        public int? MetadataToken { get; private set; }
        /// <summary>The module property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ApiSdk.Models.Module? Module { get; set; }
#nullable restore
#else
        public ApiSdk.Models.Module Module { get; set; }
#endif
        /// <summary>The name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; private set; }
#nullable restore
#else
        public string Name { get; private set; }
#endif
        /// <summary>The propertyType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ApiSdk.Models.TypeObject? PropertyType { get; set; }
#nullable restore
#else
        public ApiSdk.Models.TypeObject PropertyType { get; set; }
#endif
        /// <summary>The reflectedType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ApiSdk.Models.TypeObject? ReflectedType { get; set; }
#nullable restore
#else
        public ApiSdk.Models.TypeObject ReflectedType { get; set; }
#endif
        /// <summary>The setMethod property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ApiSdk.Models.MethodInfo? SetMethod { get; set; }
#nullable restore
#else
        public ApiSdk.Models.MethodInfo SetMethod { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="ApiSdk.Models.PropertyInfo"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static ApiSdk.Models.PropertyInfo CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new ApiSdk.Models.PropertyInfo();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "attributes", n => { Attributes = n.GetIntValue(); } },
                { "canRead", n => { CanRead = n.GetBoolValue(); } },
                { "canWrite", n => { CanWrite = n.GetBoolValue(); } },
                { "customAttributes", n => { CustomAttributes = n.GetCollectionOfObjectValues<ApiSdk.Models.CustomAttributeData>(ApiSdk.Models.CustomAttributeData.CreateFromDiscriminatorValue)?.ToList(); } },
                { "declaringType", n => { DeclaringType = n.GetObjectValue<ApiSdk.Models.TypeObject>(ApiSdk.Models.TypeObject.CreateFromDiscriminatorValue); } },
                { "getMethod", n => { GetMethod = n.GetObjectValue<ApiSdk.Models.MethodInfo>(ApiSdk.Models.MethodInfo.CreateFromDiscriminatorValue); } },
                { "isCollectible", n => { IsCollectible = n.GetBoolValue(); } },
                { "isSpecialName", n => { IsSpecialName = n.GetBoolValue(); } },
                { "memberType", n => { MemberType = n.GetIntValue(); } },
                { "metadataToken", n => { MetadataToken = n.GetIntValue(); } },
                { "module", n => { Module = n.GetObjectValue<ApiSdk.Models.Module>(ApiSdk.Models.Module.CreateFromDiscriminatorValue); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "propertyType", n => { PropertyType = n.GetObjectValue<ApiSdk.Models.TypeObject>(ApiSdk.Models.TypeObject.CreateFromDiscriminatorValue); } },
                { "reflectedType", n => { ReflectedType = n.GetObjectValue<ApiSdk.Models.TypeObject>(ApiSdk.Models.TypeObject.CreateFromDiscriminatorValue); } },
                { "setMethod", n => { SetMethod = n.GetObjectValue<ApiSdk.Models.MethodInfo>(ApiSdk.Models.MethodInfo.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteIntValue("attributes", Attributes);
            writer.WriteObjectValue<ApiSdk.Models.TypeObject>("declaringType", DeclaringType);
            writer.WriteObjectValue<ApiSdk.Models.MethodInfo>("getMethod", GetMethod);
            writer.WriteIntValue("memberType", MemberType);
            writer.WriteObjectValue<ApiSdk.Models.Module>("module", Module);
            writer.WriteObjectValue<ApiSdk.Models.TypeObject>("propertyType", PropertyType);
            writer.WriteObjectValue<ApiSdk.Models.TypeObject>("reflectedType", ReflectedType);
            writer.WriteObjectValue<ApiSdk.Models.MethodInfo>("setMethod", SetMethod);
        }
    }
}
