using RepositoryDesignPatternMvc.Models;
using System.Collections.Generic;

namespace RepositoryDesignPatternMvc.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeeById(int? id);
        void NewEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int? id);
        void Save();
    }
}
