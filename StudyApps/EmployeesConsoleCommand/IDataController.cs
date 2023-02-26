using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleCommand
{
    internal interface IDataController
    {
        public List<Employee> GetData();
        public void PushData(List<Employee> employees);
    }
}
