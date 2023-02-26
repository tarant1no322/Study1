namespace EmployeesConsoleCommand.ConsoleCommands
{
    enum DataEmployee
    {
        FirstName,
        LastName,
        PhoneNumber,
        Description
    }
    internal class EditEmpCommand : IConsoleCommand
    {
        private readonly IConsoleCommand _prevCommand;
        private readonly IDataController _dataController = new JsonController();
        private Guid _guid;
        private readonly List<Employee> _employees;
        private Employee _currentEmployee;
        private DataEmployee _dataEmployee;
        public EditEmpCommand(IConsoleCommand prevCommand, Guid guid, DataEmployee dataEmployee)
        {
            _prevCommand = prevCommand;
            _dataEmployee = dataEmployee;
            _guid = guid;
            _employees = _dataController.GetData();
            _currentEmployee = _employees.Find(x => x.Guid == _guid)!;
        }
        public void Functionality()
        {
            Print.PrintLogo("EditEmployee");
            switch (_dataEmployee)
            {
                case DataEmployee.FirstName:
                    _currentEmployee.FirstName = Print.InputString("Введите новое имя сотрудника: ")!;
                    if (IsEditCancel(_currentEmployee.FirstName))
                        return;
                    break;
                case DataEmployee.LastName:
                    _currentEmployee.LastName = Print.InputString("Введите новую фамилию сотрудника: ")!;
                    if (IsEditCancel(_currentEmployee.LastName))
                        return;
                    break;
                case DataEmployee.PhoneNumber:
                    _currentEmployee.PhoneNumber = Print.InputString("Введите новый номер телефона сотрудника: ")!;
                    if (IsEditCancel(_currentEmployee.PhoneNumber))
                        return;
                    break;
                case DataEmployee.Description:
                    _currentEmployee.Description = Print.InputString("Введите новое описание сотрудника: ")!;
                    if (IsEditCancel(_currentEmployee.Description))
                        return;
                    break;
            }
            Console.WriteLine("\nДанные сотрудника отредактированы!\nНажмите любую клавишу для возврата...");
            _dataController.PushData(_employees);
        }
        public IConsoleCommand Execute(ConsoleKey key)
        {
            return new ViewProfileEmpCommand(_prevCommand, _guid);
        }

        public IConsoleCommand PrevCommand() => _prevCommand;
        private bool IsEditCancel(string? data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                Console.WriteLine("\nОтмена ввода...\nНажмите любую клавишу для возврата...");
                return true;
            }
            return false;
        }
    }
}
