using EmployeesConsoleCommand.ConsoleCommands;
using EmployeesConsoleCommand.DataController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleCommand
{
    internal class GenerateList
    {
        /// <summary>
        /// класс должен иметь публичные методы для смены страницы, метод печати списка, метод получения словаря
        /// </summary>
        private Dictionary<ConsoleKey, Guid> _result = new();
        private List<Employee> _employees = new();
        private List<string> _pointsMenu = new();
        private IDataController _dataController = new SQLiteController();
        private static int _currentPage = 1, _countPages;
        private bool _isListEmpty;

        public void PrintList()
        {
            MathList();
            if (_isListEmpty)
                return;
            Print.PrintMenu(_pointsMenu);
            if (_countPages > 1)
            {
                Console.WriteLine($"\n\tТекущая страница [{_currentPage}/{_countPages}]\n\tНажимайте стрелки ВВЕРХ/ВНИЗ для переключения страниц");
            }

        }
        public Dictionary<ConsoleKey, Guid> GetList()
        {
            MathList();
            return _result;
        }

        private void MathList()
        {
            _employees = _dataController.GetData();
            _result = new();
            _pointsMenu = new();
            if (_employees.Count == 0)
                return;
            MathPages();
            int startInt = 0;
            if (_countPages > 1)
                startInt = _currentPage * 9 - 9;
            for (int i = startInt, j = 0; i < _employees.Count && j < 9; i++, j++)
            {
                _result.Add((ConsoleKey)j + 49, _employees[i].Id);
                _pointsMenu.Add($"{_employees[i].LastName} {_employees[i].FirstName}");
            }
        }
        private void MathPages()
        {
            _countPages = (int)Math.Ceiling((decimal)_employees.Count / 9);
            if (_countPages < _currentPage)
                _currentPage = _countPages;

        }

        public void PageUp()
        {
            if (_currentPage != 1)
                _currentPage--;
        }
        public void PageDoun()
        {
            if (_currentPage < _countPages)
                _currentPage++;
        }
        public bool IsListEmpty()
        {
            _employees = _dataController.GetData();
            _isListEmpty = _employees.Count == 0;
            return _isListEmpty;
        }
    }
}
