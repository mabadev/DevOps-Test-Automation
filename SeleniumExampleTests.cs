using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumTestExample.Attributes;
using System.Data;
using SeleniumTestExample.Infrastructure;

namespace SeleniumTestExample.TestPlans.Project_Configuration
{
    [TestClass]
    public class TestPlan12754 : TestPlanBase, IUiTest
    {
        [TestMethod]
        [TestCategory("UITest")]
        [CustomDataSource(<"TFS-Server">, <"PROJECT-NAME">, <TEST-CASE-ID>)]
        public void Execute(DataRow parameter)
        {
            var sw = Stopwatch.StartNew();
                    
            var user = parameter["User"].ToString();
            var password = parameter["Password"].ToString();
            var language = parameter["Language"].ToString();

            // Do Something
            
            this.TestContext.WriteLine($"Elapsed: {sw.Elapsed.ToString()}");
        }
    }
}
