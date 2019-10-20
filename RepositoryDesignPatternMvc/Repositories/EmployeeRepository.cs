using RepositoryDesignPatternMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RepositoryDesignPatternMvc.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _dbContext;

        public EmployeeRepository()
        {
            _dbContext = new EmployeeContext();
        }

        public EmployeeRepository(EmployeeContext context)
        {
            _dbContext = context;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _dbContext.Employees.ToList();
        }

        public Employee GetEmployeeById(int? id)
        {
            return _dbContext.Employees.Find(id);
        }

        public void NewEmployee(Employee employee)
        {
            _dbContext.Employees.Add(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            _dbContext.Entry(employee).State = EntityState.Modified;
        }

        public void DeleteEmployee(int? id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee != null) _dbContext.Employees.Remove(employee);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}