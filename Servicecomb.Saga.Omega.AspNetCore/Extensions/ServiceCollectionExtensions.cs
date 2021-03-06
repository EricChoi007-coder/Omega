/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Servicecomb.Saga.Omega.Abstractions.Transaction.Extensions;
using Servicecomb.Saga.Omega.Core.DependencyInjection;
using Servicecomb.Saga.Omega.Core.Transport.HttpClient;

namespace Servicecomb.Saga.Omega.AspNetCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static OmegaBuilder AddOmegaCore(this IServiceCollection services,
            Action<OmegaOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var service = services.Configure(options).AddOmegaCore(options, "");
            ServiceLocator.SetLocatorProvider(services.BuildServiceProvider());
            return service;
        }
        private static OmegaBuilder AddOmegaCore([NotNull]this IServiceCollection services, Action<OmegaOptions> options, string servcie)
        {
            var builder = new OmegaBuilder(services);
            builder.AddHosting(options).AddDiagnostics().AddHttpClient();
            return builder;
        }
    }
}
