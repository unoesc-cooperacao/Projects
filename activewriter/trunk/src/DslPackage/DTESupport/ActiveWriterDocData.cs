// Copyright 2004-2010 Castle Project - http://www.castleproject.org/
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

namespace Castle.ActiveWriter
{
    using Microsoft.VisualStudio;
    using Microsoft.VisualStudio.Shell.Interop;

    internal partial class ActiveWriterDocData : IVsExtensibleObject
    {
        #region IVsExtensibleObject Members

        public int GetAutomationObject(string pszPropName, out object ppDisp)
        {
            if (pszPropName == "Model")
            {
                ppDisp = new ActiveWriterExtensibleObject((Model) this.RootElement);
                return VSConstants.S_OK;
            }
            
            ppDisp = null;
            return VSConstants.E_INVALIDARG;
        }

        #endregion
    }
}