// <copyright file="HealthzHelper.cs" company="Splunk Inc.">
// Copyright Splunk Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

// <copyright file="HealthzHelper.cs" company="OpenTelemetry Authors">
// Copyright The OpenTelemetry Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

#nullable disable

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Splunk.OpenTelemetry.AutoInstrumentation.IntegrationTests.Helpers;

internal static class HealthzHelper
{
    public static async Task TestAsync(string healthzUrl, ITestOutputHelper output)
    {
        output.WriteLine($"Testing healthz endpoint: {healthzUrl}");
        HttpClient client = new();

        var intervalMilliseconds = 500;
        var maxMillisecondsToWait = 15_000;
        var maxRetries = maxMillisecondsToWait / intervalMilliseconds;
        for (int retry = 0; retry < maxRetries; retry++)
        {
            try
            {
                var response = await client.GetAsync(healthzUrl);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return;
                }

                output.WriteLine($"Healthz endpoint retured HTTP status code: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                output.WriteLine($"Healthz endpoint call failed: {ex.Message}");
            }

            await Task.Delay(intervalMilliseconds);
        }

        throw new InvalidOperationException($"Healthz endpoint never returned OK: {healthzUrl}");
    }
}
