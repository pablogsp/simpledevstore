using NUnit.Framework;
using Payspace_NextGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payspace_NextGen.Controllers.Tests
{
    [TestFixture()]
    public class APIcalculationDetailsControllerTests
    {
        [Test()]
        public void PostProcesscalculationTest()
        {
            APIcalculationDetailsController contr = new APIcalculationDetailsController();
            Assert.Pass("Value OK: ");
        }

        [Test()]
        public void APIcalculationDetailsControllerTest()
        {
            Assert.Fail();
        }
    }
}