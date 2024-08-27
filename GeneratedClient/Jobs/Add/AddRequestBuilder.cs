// <auto-generated/>
using ApiSdk.Jobs.Add.Item;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
namespace ApiSdk.Jobs.Add
{
    /// <summary>
    /// Builds and executes requests for operations under \jobs\add
    /// </summary>
    public class AddRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the ApiSdk.jobs.add.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="ApiSdk.Jobs.Add.Item.WithNameItemRequestBuilder"/></returns>
        public ApiSdk.Jobs.Add.Item.WithNameItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("name", position);
                return new ApiSdk.Jobs.Add.Item.WithNameItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="ApiSdk.Jobs.Add.AddRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public AddRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/jobs/add", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="ApiSdk.Jobs.Add.AddRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public AddRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/jobs/add", rawUrl)
        {
        }
    }
}