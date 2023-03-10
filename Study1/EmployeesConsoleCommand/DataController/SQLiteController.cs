using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeesConsoleCommand.DataController
{
    class SQLiteController : IDataController
    {
        SQLiteContext db = new SQLiteContext();

        public void Add(Employee emp)
        {
            db.Employees?.Add(emp);
            db.SaveChanges();
        }

        public void Edit(Guid guid, string newField, EmployeeFieldsEnum field)
        {
            switch (field)
            {
                case EmployeeFieldsEnum.FirstName:
                    db.Employees.FirstOrDefault(x => x.Id == guid)!.FirstName = newField;
                    break;
                case EmployeeFieldsEnum.LastName:
                    db.Employees.FirstOrDefault(x => x.Id == guid)!.LastName = newField;
                    break;
                case EmployeeFieldsEnum.PhoneNumber:
                    db.Employees.FirstOrDefault(x => x.Id == guid)!.PhoneNumber = newField;
                    break;
                case EmployeeFieldsEnum.Description:
                    db.Employees.FirstOrDefault(x => x.Id == guid)!.Description = newField;
                    break;
            }
            db.SaveChanges();
        }

        public List<Employee> GetData()
        {
            List<Employee>? employees = db?.Employees?.ToList();
            if(employees == null)
                return new();
            return employees;
        }

        public void Remove(Guid guid)
        {
            Employee emp = db.Employees.FirstOrDefault(x => x.Id == guid)!;
            db.Employees.Remove(emp);
            db.SaveChanges();
        }
    }
}
