using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Payspace_NextGen.Models;

namespace Payspace_NextGen.Controllers
{
    public interface IAPIEmployesController
    {
        Task<IHttpActionResult> DeleteEmploye(string id);
        bool Equals(APIEmployesController other);
        bool Equals(object obj);
        Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken);
        Task<IHttpActionResult> GetEmploye(string id);
        IQueryable<Employe> GetEmployes();
        int GetHashCode();
        Task<IHttpActionResult> PostEmploye(Employe employe);
        Task<IHttpActionResult> PutEmploye(string id, Employe employe);
        string ToString();
    }
}