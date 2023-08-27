using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Model.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Employees { get; }

        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByCodeAsync(string employeeCode);
        Task<bool> SaveAsync(Employee employee);
        Task<bool> DeleteAsync(string employeeCode);

        IEnumerable<Employee> GetAll();
        Employee GetByCode(string employeeCode);
        bool Save(Employee employee);
        bool Delete(string employeeCode);
    }
}
