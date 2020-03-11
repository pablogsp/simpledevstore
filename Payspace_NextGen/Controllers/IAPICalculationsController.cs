using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Payspace_NextGen.Models;

namespace Payspace_NextGen.Controllers
{
    public interface IAPICalculationsController
    {
        Task<IHttpActionResult> DeleteCalculation(int id);
        bool Equals(object obj);
        Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken);
        Task<IHttpActionResult> GetCalculation(int id);
        IQueryable<Calculation> GetCalculations();
        int GetHashCode();
        Task<IHttpActionResult> PostCalculation(Calculation calculation);
        Task<IHttpActionResult> PutCalculation(int id, Calculation calculation);
        string ToString();
    }
}