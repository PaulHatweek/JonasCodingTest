﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeInfo>> GetAllEmployeesAsync();
        Task<EmployeeInfo> GetEmployeeByCodeAsync(string employeeCode);
        Task<bool> SaveEmployeeAsync(EmployeeInfo employeeInfo);
        Task<bool> DeleteEmployeeAsync(string employeeCode);

        IEnumerable<EmployeeInfo> GetAllEmployees();
        EmployeeInfo GetEmployeeByCode(string employeeCode);
        bool SaveEmployee(EmployeeInfo employeeInfo);
        bool DeleteEmployee(string employeeCode);
    }
}
