/**
 *   Copyright 2013
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NinthChevron.Data.AutoMap
{
    public delegate void EnterModelHandler(PropertyDescriptor property, IModelVisitorContext context, IModelVisitorContext parentContext);
    public delegate void ExitModelHandler(PropertyDescriptor property, IModelVisitorContext context, IModelVisitorContext parentContext);
    public delegate void VisitPropertyHandler(PropertyDescriptor property, IModelVisitorContext context);

    public interface IModelVisitor
    {
        event EnterModelHandler OnEnterModel;
        event ExitModelHandler OnExitModel;
        event VisitPropertyHandler OnVisitProperty;

        void Visit<TModel>(object model);
    }
}
