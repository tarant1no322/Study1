
class Employee
{
    public string? FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
    public Employee(string firstName, string lastName, string phoneNumber, string description)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Description = description;
    }
}

