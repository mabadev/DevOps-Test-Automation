# DevOps-Test-Automation
Test automation on DevOps is a process that involves several steps. I try to map all steps and create a test project here. You can start with this example.

# Problem
How do you run data-driven tests in TFS without using the ```[DataSource]``` attribute?

```c#
 [DataSource("Microsoft.VisualStudio.TestTools.DataSource.TestCase",
            "http://TFS URL", 
            "1234", 
            DataAccessMethod.Sequential), 
            TestMethod]
 ```
 
# Solution
You can implement a ```[CustomDataSource]'``` attribute. With this you can connect your TFS-server and retrieve test parameters

## STEP 1
Get test data by implementing a class that can retrieve the test data
```c# 
        public void GetParameterValueByTestCaseId()
        {
            TfsTeamProjectCollection tfs = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri("TFS_URL"));
            ITestManagementTeamProject project = tfs.GetService<ITestManagementService>().GetTeamProject("ProjectName");
            var testCase = project.TestCases.Find(1234);
            var testData = testCase.Data.Tables[0];
            this._dataSource = testData.Select();
        }
```
## STEP 2
You have to implement a ```[CustomDataSource]``` attribute to be able to call test data. this attribute is inherited ITestDataSource by using MSTest V2
```c#
        private string _projectCollection { get; }
        private string _teamProject { get; }
        private int _testCaseId { get; }
        private DataRow[] _dataSources;

        public CustomDataSourceAttribute(string projectCollection, string teamProject, int testCaseId)
        {
            this._projectCollection = projectCollection;
            this._teamProject = teamProject;
            this._testCaseId = testCaseId;
        }
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            this.GetParameterValueByTestCaseId();
            foreach (var data in this._dataSources)
            {
                yield return new object[] {data};
            }

            if (this._dataSources.Length == 0)
                yield return new object[] {null};
        }

        public string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            var className = methodInfo.DeclaringType.Name;
            if (data != null)
            {
                return $"{className} - {data}";
            }

            return $"{className} - No parameter";
        }
```
## STEP 3
Invoke the ```[CustomDataSource]``` attribute above your test method
```c#
        [TestMethod]
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
```

