using System.Data;

namespace SeleniumTestExample.Infrastructure
{
    public interface IUiTest
    {
        //void Execute();
        void Execute(DataRow parameter);
    }
}