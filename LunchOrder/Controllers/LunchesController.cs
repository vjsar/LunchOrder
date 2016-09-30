using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LunchOrder.Models.Data;

namespace LunchOrder.Controllers
{
    [Authorize]
    public class LunchesController : ApiController
    {
        private LunchOrderContext db = new LunchOrderContext();

        // GET: api/Lunches
        public IQueryable<Lunch> GetLunches()
        {
            return db.Lunches;
        }

        // GET: api/Lunches/5
        [ResponseType(typeof(Lunch))]
        public async Task<IHttpActionResult> GetLunch(int id)
        {
            Lunch lunch = await db.Lunches.FindAsync(id);
            if (lunch == null)
            {
                return NotFound();
            }

            return Ok(lunch);
        }

        // PUT: api/Lunches/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLunch(int id, Lunch lunch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lunch.ID)
            {
                return BadRequest();
            }

            db.Entry(lunch).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LunchExists(id))
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

        // POST: api/Lunches
        [ResponseType(typeof(Lunch))]
        public async Task<IHttpActionResult> PostLunch(Lunch lunch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lunches.Add(lunch);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LunchExists(lunch.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = lunch.ID }, lunch);
        }

        // DELETE: api/Lunches/5
        [ResponseType(typeof(Lunch))]
        public async Task<IHttpActionResult> DeleteLunch(int id)
        {
            Lunch lunch = await db.Lunches.FindAsync(id);
            if (lunch == null)
            {
                return NotFound();
            }

            db.Lunches.Remove(lunch);
            await db.SaveChangesAsync();

            return Ok(lunch);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LunchExists(int id)
        {
            return db.Lunches.Count(e => e.ID == id) > 0;
        }
    }
}