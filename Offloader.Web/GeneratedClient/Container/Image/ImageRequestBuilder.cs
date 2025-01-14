// <auto-generated/>
using ApiSdk.Container.Image.List;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
namespace ApiSdk.Container.Image
{
    /// <summary>
    /// Builds and executes requests for operations under \container\image
    /// </summary>
    public class ImageRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The list property</summary>
        public ApiSdk.Container.Image.List.ListRequestBuilder List
        {
            get => new ApiSdk.Container.Image.List.ListRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="ApiSdk.Container.Image.ImageRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ImageRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/container/image", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="ApiSdk.Container.Image.ImageRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ImageRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/container/image", rawUrl)
        {
        }
    }
}
