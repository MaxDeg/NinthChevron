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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using NinthChevron.Helpers;

namespace NinthChevron.Data.AutoMap
{
    public class ModelVisitor : IModelVisitor
    {
        private Stack<IModelVisitorContext> _stack = new Stack<IModelVisitorContext>();
        private List<object> _alreadyVisited = new List<object>();

        public event EnterModelHandler OnEnterModel;
        public event ExitModelHandler OnExitModel;
        public event VisitPropertyHandler OnVisitProperty;

        public virtual void Visit<TModel>(object model)
        {
            this._stack.Push(new ModelVisitorContext { Model = model, Data = null });
            if (model != null) this._alreadyVisited.Add(model);

            if (OnEnterModel != null)
                OnEnterModel(null, this._stack.Peek(), null);

            PropertyDescriptorCollection propertyCollection;
            if (model == null)
                propertyCollection = TypeDescriptor.GetProperties(typeof(TModel));
            else
                propertyCollection = TypeDescriptor.GetProperties(model);

            foreach (PropertyDescriptor prop in propertyCollection)
                Visit(prop, model);

            if (OnExitModel != null)
                OnExitModel(null, this._stack.Peek(), null);
        }

        protected virtual void Visit(PropertyDescriptor property, object model)
        {
            try
            {
                IModelVisitorContext context = this._stack.Peek();

                // Need to check inner property
                if (!TypeHelper.IsBaseType(property.PropertyType) && !typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    object value = property.GetValue(context.Model);
                    if (value != null)
                    {
                        if (this._alreadyVisited.Any(v => object.ReferenceEquals(v, value)))
                            return;
                        else
                            this._alreadyVisited.Add(value);
                    }

                    IModelVisitorContext tmpContext = new ModelVisitorContext { Model = value, Data = null };

                    if (OnEnterModel != null)
                        OnEnterModel(property, tmpContext, this._stack.Peek());

                    this._stack.Push(tmpContext);

                    foreach (PropertyDescriptor subProp in TypeDescriptor.GetProperties(property.PropertyType))
                        Visit(subProp, value);

                    IModelVisitorContext oldContext = this._stack.Pop();
                    if (OnExitModel != null)
                        OnExitModel(property, oldContext, this._stack.Peek());

                    return;
                }

                if (OnVisitProperty != null)
                    OnVisitProperty(property, this._stack.Peek());
            }
            catch (Exception e)
            {
                throw new Exception("[ModelVisitor] Property: " + property.Name, e);
            }
        }
    }
}
