using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleCommand.DataController
{
    internal interface IDataController
    {
        public List<Employee> GetData();
        public void Add(Employee emp);
        public void Remove(Guid guid);
        public void Edit(Guid guid, string newField, EmployeeFieldsEnum field);
    }
}
