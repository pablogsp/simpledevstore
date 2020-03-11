using NUnit.Framework;
using Payspace_NextGen.Controllers;
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

        }

        [Test()]
        public void GetcalculationResultRateNoProgressiveTest(EmployesController emp)
        {
            decimal x = 0;
            x = 8;
            Assert.AreEqual(5, x);

        }
    }
}