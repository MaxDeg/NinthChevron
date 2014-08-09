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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using NinthChevron.Data.MySql.Test.Sakila;
using System.Linq;

namespace NinthChevron.Data.MySql.Test
{
    [TestClass]
    public class EntityTest
    {
        private MySqlDataContext _context = new MySqlDataContext(ConfigurationManager.ConnectionStrings["T4Connection"].ConnectionString);

        [TestMethod]
        public void TestSelectJoinEntities()
        { 
            var actors = this._context.Query<FilmActor>().Where(f => f.FilmId == 827).Select(f => f.Actor);
            
            Assert.AreEqual(actors.Count(), 11);
        }

        [TestMethod]
        public void TestSelectEntities()
        {
            var data = this._context.Query<Film>().Where(f => f.FilmId == 827).Select(f => new
            {
                Name = f.Title,
                Description = f.Description,
                ReleaseYear = f.ReleaseYear
            }).First();

            Assert.AreEqual(data.Name, "SPICE SORORITY");
            Assert.AreEqual(data.ReleaseYear, (short)2006);
        }

        [TestMethod]
        public void TestInsertEntity()
        {
            Actor actor = new Actor
            {
                FirstName = "Nathalie",
                LastName = "Portman",
                LastUpdate = DateTime.Now
            };

            this._context.Insert(actor);

            Assert.AreNotEqual(actor.ActorId, 0);

            Actor storedActor = this._context.Query<Actor>().FirstOrDefault(a => a.ActorId == actor.ActorId);
            Assert.IsNotNull(storedActor);
            Assert.AreEqual(actor.FirstName, storedActor.FirstName);
        }

        [TestMethod]
        public void TestUpdateEntity()
        {
            Actor actor = this._context.Query<Actor>()
                        .FirstOrDefault(a => a.FirstName == "nathalie" && a.LastName == "portman");
            Assert.AreNotEqual(actor.ActorId, 0);

            Console.WriteLine(actor.LastUpdate);
            actor.LastUpdate = DateTime.Now;

            this._context.Update(actor);

            Actor storedActor = this._context.Query<Actor>().FirstOrDefault(a => a.ActorId == actor.ActorId);
            //Assert.AreEqual(actor.LastUpdate.TimeOfDay, storedActor.LastUpdate.TimeOfDay);
        }

        [TestMethod]
        public void TestDeleteEntity()
        {
            Actor actor = this._context.Query<Actor>()
                        .FirstOrDefault(a => a.FirstName == "nathalie" && a.LastName == "portman");
            Assert.AreNotEqual(actor.ActorId, 0);
            int id = actor.ActorId;

            this._context.Delete(actor);

            Assert.AreEqual(actor.ActorId, 0);

            Actor storedActor = this._context.Query<Actor>().FirstOrDefault(a => a.ActorId == id);
            Assert.IsNull(storedActor);
        }
    }
}