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
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using AngularMinidemo.Models;

namespace AngularMinidemo.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using AngularMinidemo.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Sport>("Sports");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class SportsController : ODataController
    {
        private SportContext db = new SportContext();

        // GET: odata/Sports
        [EnableQuery]
        public IQueryable<Sport> GetSports()
        {
            return db.Sports;
        }

        // GET: odata/Sports(5)
        [EnableQuery]
        public SingleResult<Sport> GetSport([FromODataUri] int key)
        {
            return SingleResult.Create(db.Sports.Where(sport => sport.Id == key));
        }

        // PUT: odata/Sports(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Sport> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Sport sport = await db.Sports.FindAsync(key);
            if (sport == null)
            {
                return NotFound();
            }

            patch.Put(sport);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SportExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(sport);
        }

        // POST: odata/Sports
        public async Task<IHttpActionResult> Post(Sport sport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sports.Add(sport);
            await db.SaveChangesAsync();

            return Created(sport);
        }

        // PATCH: odata/Sports(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Sport> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Sport sport = await db.Sports.FindAsync(key);
            if (sport == null)
            {
                return NotFound();
            }

            patch.Patch(sport);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SportExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(sport);
        }

        // DELETE: odata/Sports(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Sport sport = await db.Sports.FindAsync(key);
            if (sport == null)
            {
                return NotFound();
            }

            db.Sports.Remove(sport);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SportExists(int key)
        {
            return db.Sports.Count(e => e.Id == key) > 0;
        }
    }
}
