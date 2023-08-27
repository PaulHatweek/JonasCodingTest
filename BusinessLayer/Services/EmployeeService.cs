using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(ICompanyRepository companyRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public IEnumerable<EmployeeInfo> GetAllEmployees()
        {
            var result = from em in _employeeRepository.Employees
                         join c in _companyRepository.Companies on em.CompanyCode equals c.CompanyCode
                         select new EmployeeInfo
                         {
                             CompanyCode = c.CompanyCode,
                             CompanyName = c.CompanyName,
                             EmployeeName = em.EmployeeName,
                             OccupationName = em.Occupation,
                             EmployeeStatus = em.EmployeeStatus,
                             EmailAddress = em.EmailAddress,
                             PhoneNumber = em.Phone,
                             LastModifiedDateTime = em.LastModified
                         };
            return result.ToList();
        }

        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployeesAsync()
        {
            var result = from em in _employeeRepository.Employees
                          join c in _companyRepository.Companies on em.CompanyCode equals c.CompanyCode
                          select new EmployeeInfo
                          {
                              CompanyCode = c.CompanyCode,
                              CompanyName = c.CompanyName,
                              EmployeeName = em.EmployeeName,
                              OccupationName = em.Occupation,
                              EmployeeStatus = em.EmployeeStatus,
                              EmailAddress = em.EmailAddress,
                              PhoneNumber = em.Phone,
                              LastModifiedDateTime = em.LastModified
                          };

            return await Task.FromResult(result.ToList());
        }

        public EmployeeInfo GetEmployeeByCode(string employeeCode)
        {
            var employee = _employeeRepository.GetByCode(employeeCode);
            if (employee != null)
            {
                var company = _companyRepository.GetByCode(employee.CompanyCode);
                var empInfo = _mapper.Map<Employee, EmployeeInfo>(employee);
                empInfo.CompanyName = company?.CompanyName;
                return empInfo;
            }

            return _mapper.Map<Employee, EmployeeInfo>(employee);
        }

        public async Task<EmployeeInfo> GetEmployeeByCodeAsync(string employeeCode)
        {
            var employee = await _employeeRepository.GetByCodeAsync(employeeCode).ConfigureAwait(false);
            if(employee != null)
            {
                var company = await _companyRepository.GetByCodeAsync(employee.CompanyCode).ConfigureAwait(false);
                var empInfo = _mapper.Map<Employee, EmployeeInfo>(employee);
                empInfo.CompanyName = company?.CompanyName;
                return empInfo;
            }
            
            return _mapper.Map<Employee, EmployeeInfo>(employee);
        }

        public bool SaveEmployee(EmployeeInfo employeeInfo)
        {
            var employee = _mapper.Map<Employee>(employeeInfo);
            employee.LastModified = DateTime.Now;
            return _employeeRepository.Save(employee);
        }

        public async Task<bool> SaveEmployeeAsync(EmployeeInfo employeeInfo)
        {
            var employee = _mapper.Map<Employee>(employeeInfo);
            employee.LastModified = DateTime.Now;
            return await _employeeRepository.SaveAsync(employee).ConfigureAwait(false);
        }

        public bool DeleteEmployee(string employeeCode)
        {
            return _employeeRepository.Delete(employeeCode);
        }

        public async Task<bool> DeleteEmployeeAsync(string employeeCode)
        {
            return await _employeeRepository.DeleteAsync(employeeCode).ConfigureAwait(false);
        }

    }
}
