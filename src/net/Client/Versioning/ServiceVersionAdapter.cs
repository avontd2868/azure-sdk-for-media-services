﻿//-----------------------------------------------------------------------
// <copyright file="ServiceVersionAdapter.cs" company="Microsoft">Copyright 2012 Microsoft Corporation</copyright>
// <license>
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </license>

using System;
using System.Data.Services.Client;
using System.Net;

namespace Microsoft.WindowsAzure.MediaServices.Client.Versioning
{
    

    /// <summary>
    /// An adapter to add the service version to the request.
    /// </summary>
    public class ServiceVersionAdapter
    {
        private readonly Version _serviceVersion;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceVersionAdapter"/> class.
        /// </summary>
        /// <param name="serviceVersion">The service version.</param>
        public ServiceVersionAdapter(Version serviceVersion)
        {
            this._serviceVersion = serviceVersion;
        }

        /// <summary>
        /// Adapts the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Adapt(DataServiceContext context)
        {
            context.SendingRequest += this.AddRequestVersion;
        }

        /// <summary>
        /// Adds the version to request.
        /// </summary>
        /// <param name="request">The request.</param>
        public void AddVersionToRequest(WebRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            request.Headers.Add("x-ms-version", this._serviceVersion.ToString());
        }

        /// <summary>
        /// Adds the request version.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Data.Services.Client.SendingRequestEventArgs"/> instance containing the event data.</param>
        private void AddRequestVersion(object sender, SendingRequestEventArgs e)
        {
            this.AddToRequestHeaders(e);
        }

        /// <summary>
        /// Adds to request headers.
        /// </summary>
        /// <param name="sendingRequestEventArgs">The <see cref="System.Data.Services.Client.SendingRequestEventArgs"/> instance containing the event data.</param>
        private void AddToRequestHeaders(SendingRequestEventArgs sendingRequestEventArgs)
        {
            this.AddVersionToRequest(sendingRequestEventArgs.Request);
        }
    }
}
