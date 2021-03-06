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

package castle.flexbridge.collections
{
	import mx.events.CollectionEventKind;
	
	/**
	 * An enumeration of <code>CollectionDelta</code> kinds.
	 */
	public final class CollectionDeltaKind
	{
		public static const ADD:String = CollectionEventKind.ADD;
		public static const MOVE:String = CollectionEventKind.MOVE;
		public static const REFRESH:String = CollectionEventKind.REFRESH;
		public static const REMOVE:String = CollectionEventKind.REMOVE;
		public static const REPLACE:String = CollectionEventKind.REPLACE;
		public static const RESET:String = CollectionEventKind.RESET;
		public static const UPDATE:String = CollectionEventKind.UPDATE;		
	}
}