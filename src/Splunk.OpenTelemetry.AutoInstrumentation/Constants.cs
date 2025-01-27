﻿// <copyright file="Constants.cs" company="Splunk Inc.">
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

namespace Splunk.OpenTelemetry.AutoInstrumentation;

internal static class Constants
{
    public const string None = "none";

    public static class Ingest
    {
        public const string TracesIngestTemplate = "https://ingest.{0}.signalfx.com/v2/trace/otlp";
        public const string MetricsIngestTemplate = "https://ingest.{0}.signalfx.com/v2/datapoint";
    }
}
