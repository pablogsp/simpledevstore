using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using Newtonsoft.Json;
using Payspace_NextGen.Models;

namespace Payspace_NextGen.Controllers
{
    public class APICalculationsController : ApiController, IAPICalculationsController
    {
        private localdbEntities db = new localdbEntities();

        public APICalculationsController(localdbEntities db)
        {
            this.db = db;
        }

        public APICalculationsController()
        {
        }

        // GET: api/APICalculations
        public IQueryable<Calculation> GetCalculations()
        {
            return db.Calculations;
        }

        // GET: api/APICalculations/5
        [ResponseType(typeof(Calculation))]
        public async Task<IHttpActionResult> GetCalculation(int id)
        {
            Calculation calculation = await db.Calculations.FindAsync(id);
            if (calculation == null)
            {
                return NotFound();
            }

            return Ok(calculation);
        }

        // PUT: api/APICalculations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCalculation(int id, Calculation calculation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != calculation.Id)
            {
                return BadRequest();
            }

            db.Entry(calculation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalculationExists(id))
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

        // POST: api/APICalculations
        [ResponseType(typeof(Calculation))]
        public async Task<IHttpActionResult> PostCalculation(Calculation calculation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Calculations.Add(calculation);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = calculation.Id }, calculation);
        }

        // DELETE: api/APICalculations/5
        [ResponseType(typeof(Calculation))]
        public async Task<IHttpActionResult> DeleteCalculation(int id)
        {
            Calculation calculation = await db.Calculations.FindAsync(id);
            if (calculation == null)
            {
                return NotFound();
            }

            db.Calculations.Remove(calculation);
            await db.SaveChangesAsync();

            return Ok(calculation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CalculationExists(int id)
        {
            return db.Calculations.Count(e => e.Id == id) > 0;
        }

        public override bool Equals(object obj)
        {
            var controller = obj as APICalculationsController;
            return controller != null &&
                   EqualityComparer<localdbEntities>.Default.Equals(db, controller.db);
        }

        public override int GetHashCode()
        {
            return 794399803 + EqualityComparer<localdbEntities>.Default.GetHashCode(db);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            return base.ExecuteAsync(controllerContext, cancellationToken);
        }

        protected override BadRequestResult BadRequest()
        {
            return base.BadRequest();
        }

        protected override BadRequestErrorMessageResult BadRequest(string message)
        {
            return base.BadRequest(message);
        }

        protected override InvalidModelStateResult BadRequest(ModelStateDictionary modelState)
        {
            return base.BadRequest(modelState);
        }

        protected override ConflictResult Conflict()
        {
            return base.Conflict();
        }

        protected override NegotiatedContentResult<T> Content<T>(HttpStatusCode statusCode, T value)
        {
            return base.Content(statusCode, value);
        }

        protected override FormattedContentResult<T> Content<T>(HttpStatusCode statusCode, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType)
        {
            return base.Content(statusCode, value, formatter, mediaType);
        }

        protected override CreatedNegotiatedContentResult<T> Created<T>(Uri location, T content)
        {
            return base.Created(location, content);
        }

        protected override CreatedAtRouteNegotiatedContentResult<T> CreatedAtRoute<T>(string routeName, IDictionary<string, object> routeValues, T content)
        {
            return base.CreatedAtRoute(routeName, routeValues, content);
        }

        protected override InternalServerErrorResult InternalServerError()
        {
            return base.InternalServerError();
        }

        protected override ExceptionResult InternalServerError(Exception exception)
        {
            return base.InternalServerError(exception);
        }

        protected override JsonResult<T> Json<T>(T content, JsonSerializerSettings serializerSettings, Encoding encoding)
        {
            return base.Json(content, serializerSettings, encoding);
        }

        protected override NotFoundResult NotFound()
        {
            return base.NotFound();
        }

        protected override OkResult Ok()
        {
            return base.Ok();
        }

        protected override OkNegotiatedContentResult<T> Ok<T>(T content)
        {
            return base.Ok(content);
        }

        protected override RedirectResult Redirect(string location)
        {
            return base.Redirect(location);
        }

        protected override RedirectResult Redirect(Uri location)
        {
            return base.Redirect(location);
        }

        protected override RedirectToRouteResult RedirectToRoute(string routeName, IDictionary<string, object> routeValues)
        {
            return base.RedirectToRoute(routeName, routeValues);
        }

        protected override ResponseMessageResult ResponseMessage(HttpResponseMessage response)
        {
            return base.ResponseMessage(response);
        }

        protected override StatusCodeResult StatusCode(HttpStatusCode status)
        {
            return base.StatusCode(status);
        }

        protected override UnauthorizedResult Unauthorized(IEnumerable<AuthenticationHeaderValue> challenges)
        {
            return base.Unauthorized(challenges);
        }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
        }
    }
}