using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeesConsoleCommand
{
    class JsonController  : IDataController
    {
        public List<Employee> GetData()
        {
            List<Employee>? employees = new();
            try
            {
                employees = JsonSerializer.Deserialize<List<Employee>>(File.ReadAllText("EmployeesDataBase.json"));
                
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine("Отсутствует файл базы сотрудников!\n\n" + ex.Message);
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Отсутствует файл базы сотрудников!\n\n" + ex.Message);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Неизвестная ошибка!\n\n" + ex.Message);
            }
            return employees != null ? employees : new();
        }
        public void PushData(List<Employee> employees)
        {
            File.WriteAllText("EmployeesDataBase.json", JsonSerializer.Serialize(employees));
        }
    }
}
