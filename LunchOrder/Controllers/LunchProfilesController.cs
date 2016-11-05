using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LunchOrder.Models.EF;
using Newtonsoft.Json;

namespace LunchOrder.Controllers
{
    public class LunchProfilesController : ApiController
    {
        private LunchOrderDBContext db = new LunchOrderDBContext();

        // GET: api/LunchProfiles
        [ResponseType(typeof(List<Models.Profile>))]
        public IHttpActionResult GetLunchProfiles()
        {
            var lunchProfiles = db.LunchProfiles.Select(y => new Models.Profile()
            {
                Id = y.Id,
                Fav1 = y.Fav1,
                Fav2 = y.Fav2,
                Fav3 = y.Fav3,
                FirstName = y.FirstName,
                FK_LocationId = y.FK_LocationId,
                FK_OfficeId = y.FK_OfficeId,
                FK_ProviderId = y.FK_ProviderId,
                LastName = y.LastName,
                Login = y.Login,
                LunchToOrder = y.LunchOrders.Select(x => new Models.LunchToOrder()
                {
                    DateOrdered = x.DateOrdered,
                    FK_ProfileId = x.FK_ProfileId,
                    Id = x.Id,
                    LunchOrder = x.Lunch,
                    LunchDay = x.LunchDay

                }).OrderByDescending(z => z.LunchDay).Take(5).ToList()
            }).ToList();
            return Ok(lunchProfiles);
        }

        // GET: api/LunchProfiles/login
        [ResponseType(typeof(Models.Profile))]
        [Route("GetLunchProfileFromLogin/{login}")]
        public IHttpActionResult GetLunchProfileFromLogin(string login)
        {
            var lunchProfile = db.LunchProfiles.Where(x => x.Login == login).Select(y => new Models.Profile()
            {
                Id = y.Id,
                Fav1 = y.Fav1,
                Fav2 = y.Fav2,
                Fav3 = y.Fav3,
                FirstName = y.FirstName,
                FK_LocationId = y.FK_LocationId,
                FK_OfficeId = y.FK_OfficeId,
                FK_ProviderId = y.FK_ProviderId,
                LastName = y.LastName,
                Login = y.Login,
                LunchToOrder = y.LunchOrders.Select(x => new Models.LunchToOrder()
                {
                    DateOrdered = x.DateOrdered,
                    FK_ProfileId = x.FK_ProfileId,
                    Id = x.Id,
                    LunchOrder = x.Lunch,
                    LunchDay = x.LunchDay

                }).OrderByDescending(z=> z.LunchDay).Take(1).ToList()
            }).ToList();

            if (lunchProfile == null)
            {
                return NotFound();
            }

            return Ok(lunchProfile);
        }

        // PUT: api/LunchProfiles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLunchProfile(int id, Models.Profile lunchProfile)
        {

            var profile = new LunchProfile()
            {
                Id = lunchProfile.Id,
                Fav1 = lunchProfile.Fav1,
                Fav2 = lunchProfile.Fav2,
                Fav3 = lunchProfile.Fav3,
                FirstName = lunchProfile.FirstName,
                FK_LocationId = lunchProfile.FK_LocationId,
                FK_OfficeId = lunchProfile.FK_OfficeId,
                FK_ProviderId = lunchProfile.FK_ProviderId,
                LastName = lunchProfile.LastName,
                Login = lunchProfile.Login,
                LunchOrders = lunchProfile.LunchToOrder.Select(x=> new Models.EF.LunchOrder()
                {
                    DateOrdered = x.DateOrdered,FK_ProfileId = x.FK_ProfileId, Id = x.Id, Lunch = x.LunchOrder,LunchDay = x.LunchDay
                }).ToList()
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != profile.Id)
            {
                return BadRequest();
            }

            db.Entry(profile).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LunchProfileExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/LunchProfiles
        [ResponseType(typeof(Models.Profile))]
        public IHttpActionResult PostLunchProfile(Models.Profile lunchProfile)
        {
            var profileExists = db.LunchProfiles.Any(x => x.Login == lunchProfile.Login);
            
           var profile = new LunchProfile()
            {
                Id = lunchProfile.Id,
                Fav1 = lunchProfile.Fav1,
                Fav2 = lunchProfile.Fav2,
                Fav3 = lunchProfile.Fav3,
                FirstName = lunchProfile.FirstName,
                FK_LocationId = lunchProfile.FK_LocationId,
                FK_OfficeId = lunchProfile.FK_OfficeId,
                FK_ProviderId = lunchProfile.FK_ProviderId,
                LastName = lunchProfile.LastName,
                Login = lunchProfile.Login
                ,
               LunchOrders = lunchProfile.LunchToOrder.Select(x => new Models.EF.LunchOrder()
               {
                   DateOrdered = x.DateOrdered,
                   FK_ProfileId = x.FK_ProfileId,
                   Id = x.Id,
                   Lunch = x.LunchOrder,
                   LunchDay = x.LunchDay
               }).ToList()
           };

            var lOrder = lunchProfile.LunchToOrder.Select(x => new Models.EF.LunchOrder()
            {
                DateOrdered = x.DateOrdered,
                FK_ProfileId = x.FK_ProfileId,
                Id = x.Id,
                Lunch = x.LunchOrder,
                LunchDay = x.LunchDay
            }).ToList();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!profileExists)
            {
                db.LunchProfiles.Add(profile);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = lunchProfile.Id }, lunchProfile);
            }
            else
            {
                db.Entry(profile).State = EntityState.Modified;
                try
                {
                    if (profile.LunchOrders.Any() && profile.LunchOrders.First().Id != 0)
                    {
                       db.LunchOrders.Remove(profile.LunchOrders.FirstOrDefault());
                    }
                    db.LunchOrders.Add(lOrder.FirstOrDefault());
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LunchProfileExists(lunchProfile.Login))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return StatusCode(HttpStatusCode.OK);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LunchProfileExists(int id)
        {
            return db.LunchProfiles.Count(e => e.Id == id) > 0;
        }

        private bool LunchProfileExists(string login)
        {
            return db.LunchProfiles.Count(e => e.Login == login) > 0;
        }
    }
}