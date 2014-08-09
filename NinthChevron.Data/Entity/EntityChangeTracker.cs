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

namespace NinthChevron.Data.Entity
{
    public class EntityChangeTracker : IEntityTracker
    {
        public IEntity Entity { get; private set; }
        public List<EntityPropertyChange> Changes { get; private set; }

        internal EntityChangeTracker(IEntity entity)
        {
            this.Entity = entity;
            this.Changes = new List<EntityPropertyChange>();

            this.Entity.PropertyChanged += Entity_PropertyChanged;
        }

        private void Entity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            EntityPropertyChangedEventArgs eventArgs = (EntityPropertyChangedEventArgs)e;

            if (this.Entity.GetType().GetProperty(e.PropertyName).GetCustomAttributes(true).OfType<DoNotTrackAttribute>().Any())
                return;

            if (eventArgs.OldValue == null && eventArgs.NewValue == null) return;
            if (eventArgs.OldValue is string && (string)eventArgs.OldValue == "" && eventArgs.NewValue == null) return;
            //if (eventArgs.OldValue != null && eventArgs.OldValue.Equals(eventArgs.NewValue)) return;

            EntityPropertyChange change = new EntityPropertyChange();
            change.Property = eventArgs.PropertyName;
            change.OldValue = eventArgs.OldValue;
            change.NewValue = eventArgs.NewValue;

            this.Changes.Add(change);
        }
    }
}
