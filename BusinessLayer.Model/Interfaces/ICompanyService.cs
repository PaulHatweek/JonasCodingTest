﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyInfo>> GetAllCompaniesAsync();
        Task<CompanyInfo> GetCompanyByCodeAsync(string companyCode);
        Task<bool> SaveCompanyAsync(CompanyInfo cieInfo);
        Task<bool> DeleteCompanyAsync(string companyCode);

        IEnumerable<CompanyInfo> GetAllCompanies();
        CompanyInfo GetCompanyByCode(string companyCode);
        bool SaveCompany(CompanyInfo cieInfo);
        bool DeleteCompany(string companyCode);
    }
}
