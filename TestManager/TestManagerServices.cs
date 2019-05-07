using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.TestManagement.Client;
using System;
using System.Data;

namespace MWM.AISTOXT.TeamFoundationServer.TestManager
{
    public class TestManagerServices
    {
        private readonly string _tfsTfsServer;
        private readonly string _project;
        private readonly int _testCaseId;
        private DataRow[] _dataSource;

        public TestManagerServices(string tfsServer, string project, int testCaseId)
        {
            this._tfsTfsServer = tfsServer;
            this._project = project;
            this._testCaseId = testCaseId;
        }

        public void GetParameterValueByTestCaseId()
        {
            TfsTeamProjectCollection tfs = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(this._tfsTfsServer));
            ITestManagementTeamProject project = tfs.GetService<ITestManagementService>().GetTeamProject(this._project);
            var testCase = project.TestCases.Find(this._testCaseId);
            var testData = testCase.Data.Tables[0];
            this._dataSource = testData.Select();
        }

        public DataRow[] DataSource
        {
            get
            {
                return _dataSource; 
            }
        }
    }
}
