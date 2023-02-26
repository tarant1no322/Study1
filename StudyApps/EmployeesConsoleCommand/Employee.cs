using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeesConsoleCommand
{
    class Employee : IComparable<Employee>
    {
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public Employee(string firstName, string lastName, string phoneNumber, string description)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Description = description;
            Guid = Guid.NewGuid();
        }

        public int CompareTo(Employee? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var LastNameComparison = string.Compare(LastName, other.LastName);
            if (LastNameComparison == 0) return string.Compare(FirstName, other.FirstName);
            return LastNameComparison;
        }
    }
}
