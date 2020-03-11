using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payspace_NextGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payspace_NextGen.Controllers.Tests
{
    [TestClass()]
    public class APIEmployesControllerTests
    {
        [TestMethod()]
        public async Task APIEmployesControllerTestAsync()
        {

            APIcalculationDetailsController ap = new APIcalculationDetailsController(new Models.localdbEntities());
            decimal teste = ap.GetcalculationResultRateNoProgressive(new Models.Employe()
            {
                CPF = "0000000",
                Name = "CalcTestePaySpace",
                PostCode = new Models.PostCode()
                {
                    PostCodeName = "TaxTeste PaySpace",
                    Id = "",
                    TaxRate = "Progressive",
                    Type = new Models.Type() { TypeRate = "Progressive" },
                    
                } });

            Assert.Fail();
        }
    }
}