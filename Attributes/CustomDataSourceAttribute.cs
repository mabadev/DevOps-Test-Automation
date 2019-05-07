using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeamFoundationServer.TestManager;

namespace SeleniumTestExample.Attributes
{
    public class CustomDataSourceAttribute : BaseAttribute, ITestDataSource
    {
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
                var info = AllParameters((DataRow)data.FirstOrDefault());
                return $"{className} - {info}";
            }

            return $"{className} - No parameter";
        }

        private void GetParameterValueByTestCaseId()
        {
            TestManagerServices TestManagerServices = new TestManagerServices(this._projectCollection, this._teamProject, this._testCaseId);
            TestManagerServices.GetParameterValueByTestCaseId();
            this._dataSources = TestManagerServices.DataSource;
        }

        private string AllParameters(DataRow dataRow)
        {
            List<string> value = new List<string>();
            var tt = dataRow.ItemArray.Length;
            for (int i = 0; i < dataRow.ItemArray.Length; i++)
            {
                value.Add($"{dataRow.Table.Columns[i]}({dataRow.ItemArray.GetValue(i)})");
            }
            return string.Join(" | ", value);
        }
    }
}
