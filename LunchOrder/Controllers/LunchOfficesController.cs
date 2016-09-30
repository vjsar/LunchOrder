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
using AutoMapper;
using LunchOrder.Models;
//using LunchOrder.Models.Data;
using LunchOrder.Models.EF;


namespace LunchOrder.Controllers
{
    [Authorize]
    public class LunchOfficesController : ApiController
    {
       private readonly LunchOrderDBContext _db = new LunchOrderDBContext();
       
        // GET: api/LunchOffices
        [ResponseType( typeof(List<Models.LunchOffice>))]
        public IHttpActionResult GetLunchOffices()
        {

           var lunchOffices = _db.LunchOffices.Select(y => new Models.LunchOffice()
            {
                Country = y.Country,
                Office = y.Office,
                OfficeId = y.OfficeId,
                LunchLocations = y.LunchLocations.Select(x=> new Models.LunchLocations() {Location = x.Location,LocationId = x.LocationId,OfficeId = y.OfficeId}).ToList(),
                LunchProviders = y.LunchProviders.Select(x => new Models.LunchProviders() { Provider= x.Provider, ProviderId = x.ProviderId, OfficeId = y.OfficeId }).ToList()
            }).ToList();

            return Ok(lunchOffices);
        }

        // GET: api/LunchOffices/5
        [ResponseType(typeof(Models.LunchOffice))]
        public IHttpActionResult GetLunchOffice(int id)
        {
            var lunchOffice = _db.LunchOffices.Where(x=> x.OfficeId == id)
                                            .Select(y => new Models.LunchOffice()
                {
                    Country = y.Country,
                    Office = y.Office,
                    OfficeId = y.OfficeId,
                    LunchLocations = y.LunchLocations.Select(x => new Models.LunchLocations() { Location = x.Location, LocationId = x.LocationId, OfficeId = y.OfficeId }).ToList(),
                    LunchProviders = y.LunchProviders.Select(x => new Models.LunchProviders() { Provider = x.Provider, ProviderId = x.ProviderId, OfficeId = y.OfficeId }).ToList()
                    }).FirstOrDefault();
            if (lunchOffice == null)
            {
                return NotFound();
            }

            return Ok(lunchOffice);
        }

        //// PUT: api/LunchOffices/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutLunchOffice(int id, Models.LunchOffice lunchOffice)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != lunchOffice.OfficeId)
        //    {
        //        return BadRequest();
        //    }
        //    LunchOffice lunch = new LunchOffice()
        //    {
        //        Country = lunchOffice.Country,
        //        Office = lunchOffice.Office,
        //        OfficeId = lunchOffice.OfficeId,
        //        LunchLocations = lunchOffice.OfficeLocation.Select(x => new LunchLocation() { Location = x.Location, LocationId = x.LocationId,FK_OfficeId = lunchOffice.OfficeId }).ToList(),
        //        LunchProviders = lunchOffice.OfficeProvider.Select(x => new LunchProvider() { Provider = x.Provider, ProviderId = x.ProviderId, FK_OfficeId = lunchOffice.OfficeId }).ToList()
        //    };

        //    _db.Entry(lunch).State = EntityState.Modified;

        //    try
        //    {
        //        await _db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LunchOfficeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/LunchOffices
        [ResponseType(typeof(Models.LunchOffice))]
        public async Task<IHttpActionResult> PostLunchOffice(Models.LunchOffice lunchOffice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lunch = new Models.EF.LunchOffice()
            {
                Country = lunchOffice.Country,
                Office = lunchOffice.Office,
                OfficeId = lunchOffice.OfficeId,
            };

            _db.LunchOffices.Add(lunch);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = lunchOffice.OfficeId }, lunchOffice);
        }

        //// DELETE: api/LunchOffices/5
        //[ResponseType(typeof(LunchOffice))]
        //public async Task<IHttpActionResult> DeleteLunchOffice(int id)
        //{
        //    LunchOffice lunchOffice = await _db.LunchOffices.FindAsync(id);
        //    if (lunchOffice == null)
        //    {
        //        return NotFound();
        //    }

        //    _db.LunchOffices.Remove(lunchOffice);
        //    await _db.SaveChangesAsync();

        //    return Ok(lunchOffice);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LunchOfficeExists(int id)
        {
            return _db.LunchOffices.Count(e => e.OfficeId == id) > 0;
        }
    }
}