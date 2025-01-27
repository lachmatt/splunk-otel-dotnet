// <copyright file="UntilAsyncOperationIsSucceeded.cs" company="Splunk Inc.">
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

// <copyright file="UntilAsyncOperationIsSucceeded.cs" company="OpenTelemetry Authors">
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
using System.Threading.Tasks;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Logging;

namespace Splunk.OpenTelemetry.AutoInstrumentation.IntegrationTests.Helpers.TestContainers;

internal class UntilAsyncOperationIsSucceeded : IWaitUntil
{
    private readonly int _maxCallCount;
    private readonly Func<Task<bool>> _operation;
    private int _tryCount;

    public UntilAsyncOperationIsSucceeded(Func<Task<bool>> operation, int maxCallCount)
    {
        _operation = operation;
        _maxCallCount = maxCallCount;
    }

    public async Task<bool> Until(ITestcontainersContainer testcontainers, ILogger logger)
    {
        if (++_tryCount > _maxCallCount)
        {
            throw new TimeoutException($"Number of failed operations exceeded max count ({_maxCallCount}).");
        }

        return await _operation();
    }
}
