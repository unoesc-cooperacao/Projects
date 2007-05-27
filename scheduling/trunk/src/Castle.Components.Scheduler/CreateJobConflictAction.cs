// Copyright 2007 Castle Project - http://www.castleproject.org/
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

using System;
using System.Collections.Generic;
using System.Text;

namespace Castle.Components.Scheduler
{
    /// <summary>
    /// Specifies how to handle the case where a job with the same name
    /// has already been created.
    /// </summary>
    public enum CreateJobConflictAction
    {
        /// <summary>
        /// Throws an exception.
        /// </summary>
        Throw,

        /// <summary>
        /// Updates the existing job.
        /// </summary>
        Update,

        /// <summary>
        /// Ignores the conflict and does not change the existing job.
        /// </summary>
        Ignore
    }
}
