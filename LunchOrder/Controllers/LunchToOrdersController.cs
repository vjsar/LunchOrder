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
using LunchOrder.Models;
using LunchOrder.Models.EF;
using LunchOrder = LunchOrder.Models.EF.LunchOrder;

namespace LunchOrder.Controllers
{
   // [RoutePrefix("api")]
    [Authorize]
   // [RoutePrefix("api")]
    public class LunchToOrdersController : ApiController
    {
        private LunchOrderDBContext db = new LunchOrderDBContext();

        // GET: api/LunchToOrders
        [ResponseType(typeof(List<Models.LunchToOrder>))]
        public IHttpActionResult GetLunchToOrders()
        {
            var lunchToOrders = db.LunchOrders.Select(y => new LunchToOrder()
            {
                DateOrdered = y.DateOrdered,
                FK_ProfileId = y.FK_ProfileId,
                Id = y.Id,
                LunchDay = y.LunchDay,
                LunchOrder = y.Lunch

            }).ToList();

            return Ok(lunchToOrders);
        }

        // GET: api/LunchToOrders
        [ResponseType(typeof(List<Models.LunchToOrder>))]
        public IHttpActionResult GetLunchToOrders(string login)
        {
            var lunchToOrders = db.LunchOrders.Join(db.LunchProfiles, o => o.FK_ProfileId, p => p.Id, (o, p) => new { o = o, p = p })
                .Where(z => z.p.Login == login).Select(b => new LunchToOrder()
                {
                    DateOrdered = b.o.DateOrdered,
                    FK_ProfileId = b.o.FK_ProfileId,
                    Id = b.o.Id,
                    LunchDay = b.o.LunchDay,
                    LunchOrder = b.o.Lunch
                }).ToList();

            return Ok(lunchToOrders);
        }

        // GET: api/LunchToOrders/5
        [ResponseType(typeof(LunchToOrder))]
        [Route("GetLunchToOrderFromLogin/{login}")]
        [HttpGet]
        public IHttpActionResult GetLunchToOrderFromLogin(string login)
        {
            DateTime cutOffDateTime = DateTime.UtcNow.Date.AddHours(10).AddMinutes(0).AddMilliseconds(0);
            
            var lunchToOrders =
                db.LunchOrders.Join(db.LunchProfiles, o => o.FK_ProfileId, p => p.Id, (o, p) => new {o = o, p = p})
                    .Where(z => z.p.Login == login).Select(b => new LunchToOrder()
                    {
                        DateOrdered = b.o.DateOrdered,
                        FK_ProfileId = b.o.FK_ProfileId,
                        Id = b.o.Id,
                        LunchDay = b.o.LunchDay,
                        LunchOrder = b.o.Lunch
                    }).ToList();

            return Ok(lunchToOrders.Where(y=> y.LunchDay > cutOffDateTime));
        }

        [ResponseType(typeof(LunchToOrder))]
        [Route("GetLunchToOrderFromLoginAndDate/{login}/{dateTime}")]
        [HttpGet]
        public IHttpActionResult GetLunchToOrderFromLoginAndDate(string login, DateTime dateTime)
        {
            var ca = dateTime;
            DateTime cutOffDateTime = DateTime.UtcNow.Date.AddHours(10).AddMinutes(0).AddMilliseconds(0);

            var lunchToOrders =
                db.LunchOrders.Join(db.LunchProfiles, o => o.FK_ProfileId, p => p.Id, (o, p) => new { o = o, p = p })
                    .Where(z => z.p.Login == login).Select(b => new LunchToOrder()
                    {
                        DateOrdered = b.o.DateOrdered,
                        FK_ProfileId = b.o.FK_ProfileId,
                        Id = b.o.Id,
                        LunchDay = b.o.LunchDay,
                        LunchOrder = b.o.Lunch
                    }).ToList();

            return Ok(lunchToOrders.Where(y => y.LunchDay > cutOffDateTime));
        }

        //    // PUT: api/LunchToOrders/5
        //    [ResponseType(typeof(void))]
        //    public IHttpActionResult PutLunchToOrder(int id, LunchToOrder lunchToOrder)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        if (id != lunchToOrder.Id)
        //        {
        //            return BadRequest();
        //        }

        //        db.Entry(lunchToOrder).State = EntityState.Modified;

        //        try
        //        {
        //            db.SaveChanges();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!LunchToOrderExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return StatusCode(HttpStatusCode.NoContent);
        //    }

        //    // POST: api/LunchToOrders
        //    [ResponseType(typeof(LunchToOrder))]
        //    public IHttpActionResult PostLunchToOrder(LunchToOrder lunchToOrder)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        db.LunchOrders.Add(lunchToOrder);
        //        db.SaveChanges();

        //        return CreatedAtRoute("DefaultApi", new { id = lunchToOrder.Id }, lunchToOrder);
        //    }

        //    // DELETE: api/LunchToOrders/5
        //    [ResponseType(typeof(LunchToOrder))]
        //    public IHttpActionResult DeleteLunchToOrder(int id)
        //    {
        //        LunchToOrder lunchToOrder = db.LunchOrders.Find(id);
        //        if (lunchToOrder == null)
        //        {
        //            return NotFound();
        //        }

        //        db.LunchOrders.Remove(lunchToOrder);
        //        db.SaveChanges();

        //        return Ok(lunchToOrder);
        //    }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LunchToOrderExists(int id)
        {
            return db.LunchOrders.Count(e => e.Id == id) > 0;
        }
    }
}