// <auto-generated/>
using ApiSdk.Container.Image;
using ApiSdk.Container.Instances;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
namespace ApiSdk.Container
{
    /// <summary>
    /// Builds and executes requests for operations under \container
    /// </summary>
    public class ContainerRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The image property</summary>
        public ApiSdk.Container.Image.ImageRequestBuilder Image
        {
            get => new ApiSdk.Container.Image.ImageRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The instances property</summary>
        public ApiSdk.Container.Instances.InstancesRequestBuilder Instances
        {
            get => new ApiSdk.Container.Instances.InstancesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="ApiSdk.Container.ContainerRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ContainerRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/container", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="ApiSdk.Container.ContainerRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ContainerRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/container", rawUrl)
        {
        }
    }
}
