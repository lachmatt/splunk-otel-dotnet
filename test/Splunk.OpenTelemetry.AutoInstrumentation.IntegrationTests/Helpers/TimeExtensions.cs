// <copyright file="TimeExtensions.cs" company="Splunk Inc.">
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

// <copyright file="TimeExtensions.cs" company="OpenTelemetry Authors">
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

namespace Splunk.OpenTelemetry.AutoInstrumentation.IntegrationTests.Helpers;

internal static class TimeExtensions
{
    private const long NanoSecondsPerTick = 1000000 / TimeSpan.TicksPerMillisecond;

    private const long UnixEpochInTicks = 621355968000000000; // = DateTimeOffset.FromUnixTimeMilliseconds(0).Ticks

    /// <summary>
    /// Returns the number of nanoseconds that have elapsed since 1970-01-01T00:00:00.000Z.
    /// </summary>
    /// <param name="dateTimeOffset">The value to get the number of elapsed nanoseconds for.</param>
    /// <returns>The number of nanoseconds that have elapsed since 1970-01-01T00:00:00.000Z.</returns>
    public static long ToUnixTimeNanoseconds(this DateTimeOffset dateTimeOffset)
    {
        return (dateTimeOffset.Ticks - UnixEpochInTicks) * NanoSecondsPerTick;
    }

    /// <summary>
    /// Reconverts a long timestamp from TimeExtensions.ToUnixTimeMicroseconds.
    /// </summary>
    /// <param name="ts">The unix time in microseconds.</param>
    /// <returns>The corresponding DateTimeOffset.</returns>
    public static DateTimeOffset UnixMicrosecondsToDateTimeOffset(this long ts)
    {
        const long microsecondsPerMillisecond = 1000;
        const long ticksPerMicrosecond = TimeSpan.TicksPerMillisecond / microsecondsPerMillisecond;
        var ticks = (ts * ticksPerMicrosecond) + UnixEpochInTicks;
        return new DateTimeOffset(ticks, TimeSpan.Zero);
    }
}
