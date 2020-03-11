using System.Threading.Tasks;
using System.Web.Mvc;
using Payspace_NextGen.Models;

namespace Payspace_NextGen.Controllers
{
    public interface ICalculationDetailsController
    {
        ActionResult Create();
        Task<ActionResult> Create([Bind(Include = "Id,EmployeID,DetailName,DetailValue,IdCalculation")] calculationDetail calculationDetail);
        Task<ActionResult> Delete(int? id);
        Task<ActionResult> DeleteConfirmed(int id);
        Task<ActionResult> Details(int? id);
        Task<ActionResult> Edit([Bind(Include = "Id,EmployeID,DetailName,DetailValue,IdCalculation")] calculationDetail calculationDetail);
        Task<ActionResult> Edit(int? id);
        bool Equals(CalculationDetailsController other);
        bool Equals(object obj);
        int GetHashCode();
        Task<ActionResult> Index();
        string ToString();
    }
}