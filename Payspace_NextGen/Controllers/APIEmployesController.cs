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
    public class APIEmployesController : ApiController, IEquatable<APIEmployesController>, IAPIEmployesController
    {
        private localdbEntities db = new localdbEntities();

        public APIEmployesController(localdbEntities db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public APIEmployesController()
        {
        }

        // GET: api/APIEmployes
        public IQueryable<Employe> GetEmployes()
        {
            return db.Employes;
        }

        // GET: api/APIEmployes/5
        [ResponseType(typeof(Employe))]
        public async Task<IHttpActionResult> GetEmploye(string id)
        {
            Employe employe = await db.Employes.FindAsync(id);
            if (employe == null)
            {
                return NotFound();
            }

            return Ok(employe);
        }

        // PUT: api/APIEmployes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmploye(string id, Employe employe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employe.CPF)
            {
                return BadRequest();
            }

            db.Entry(employe).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeExists(id))
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

        // POST: api/APIEmployes
        [ResponseType(typeof(Employe))]
        public async Task<IHttpActionResult> PostEmploye(Employe employe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employes.Add(employe);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeExists(employe.CPF))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = employe.CPF }, employe);
        }

        // DELETE: api/APIEmployes/5
        [ResponseType(typeof(Employe))]
        public async Task<IHttpActionResult> DeleteEmploye(string id)
        {
            Employe employe = await db.Employes.FindAsync(id);
            if (employe == null)
            {
                return NotFound();
            }

            db.Employes.Remove(employe);
            await db.SaveChangesAsync();

            return Ok(employe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeExists(string id)
        {
            return db.Employes.Count(e => e.CPF == id) > 0;
        }

        public override bool Equals(object obj)
        {
            var controller = obj as APIEmployesController;
            return controller != null &&
                   EqualityComparer<localdbEntities>.Default.Equals(db, controller.db);
        }

        public bool Equals(APIEmployesController other)
        {
            return other != null &&
                   EqualityComparer<localdbEntities>.Default.Equals(db, other.db);
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

        public static bool operator ==(APIEmployesController controller1, APIEmployesController controller2)
        {
            return EqualityComparer<APIEmployesController>.Default.Equals(controller1, controller2);
        }

        public static bool operator !=(APIEmployesController controller1, APIEmployesController controller2)
        {
            return !(controller1 == controller2);
        }
    }
}