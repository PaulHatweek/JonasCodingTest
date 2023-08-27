using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbWrapper<Employee> _employeeDbWrapper;

        public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper)
        {
            _employeeDbWrapper = employeeDbWrapper;
        }

        public IEnumerable<Employee> Employees => GetAll();

        public IEnumerable<Employee> GetAll()
        {
            return _employeeDbWrapper.FindAll();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeDbWrapper.FindAllAsync().ConfigureAwait(false);
        }

        public Employee GetByCode(string employeeCode)
        {
            return _employeeDbWrapper.Find(t => t.EmployeeCode.Equals(employeeCode))?.FirstOrDefault();
        }

        public async Task<Employee> GetByCodeAsync(string employeeCode)
        {
            var cies = await _employeeDbWrapper.FindAsync(t => t.EmployeeCode.Equals(employeeCode)).ConfigureAwait(false);
            return cies?.FirstOrDefault();
        }

        public bool Save(Employee employee)
        {
            var itemRepo = _employeeDbWrapper.Find(t =>
                t.SiteId.Equals(employee.SiteId) && 
                t.EmployeeCode.Equals(employee.EmployeeCode))?.FirstOrDefault();
            if (itemRepo != null)
            {
                itemRepo.EmployeeName = employee.EmployeeName;
                itemRepo.Occupation = employee.Occupation;
                itemRepo.EmployeeStatus = employee.EmployeeStatus;
                itemRepo.EmailAddress = employee.EmailAddress;
                itemRepo.Phone = employee.Phone;
                itemRepo.LastModified = employee.LastModified;
                return _employeeDbWrapper.Update(itemRepo);
            }

            return _employeeDbWrapper.Insert(employee);
        }

        public async Task<bool> SaveAsync(Employee employee)
        {
            var itemRepo = _employeeDbWrapper.Find(t =>
                t.SiteId.Equals(employee.SiteId) && 
                t.EmployeeCode.Equals(employee.EmployeeCode))?.FirstOrDefault();
            if (itemRepo != null)
            {
                itemRepo.EmployeeName = employee.EmployeeName;
                itemRepo.Occupation = employee.Occupation;
                itemRepo.EmployeeStatus = employee.EmployeeStatus;
                itemRepo.EmailAddress = employee.EmailAddress;
                itemRepo.Phone = employee.Phone;
                itemRepo.LastModified = employee.LastModified;
                return await _employeeDbWrapper.UpdateAsync(itemRepo).ConfigureAwait(false);
            }

            return await _employeeDbWrapper.InsertAsync(employee).ConfigureAwait(false);
        }

        public bool Delete(string employeeCode)
        {
            return _employeeDbWrapper.Delete(t => t.EmployeeCode.Equals(employeeCode));
        }

        public async Task<bool> DeleteAsync(string employeeCode)
        {
            return await _employeeDbWrapper.DeleteAsync(t => t.EmployeeCode.Equals(employeeCode)).ConfigureAwait(false);
        }
    }
}
