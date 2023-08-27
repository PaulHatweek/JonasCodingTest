using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Model.Models;
using BusinessLayer.Model.Interfaces;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;

namespace BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyInfo>> GetAllCompaniesAsync()
        {
            var res = await _companyRepository.GetAllAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }

        public async Task<CompanyInfo> GetCompanyByCodeAsync(string companyCode)
        {
            var result = await _companyRepository.GetByCodeAsync(companyCode).ConfigureAwait(false);
            return _mapper.Map<CompanyInfo>(result);
        }

        public async Task<bool> SaveCompanyAsync(CompanyInfo cieInfo)
        {
            var cie = _mapper.Map<Company>(cieInfo);
            cie.LastModified = DateTime.Now;
            return await _companyRepository.SaveCompanyAsync(cie).ConfigureAwait(false);
        }

        public async Task<bool> DeleteCompanyAsync(string companyCode)
        {
            return await _companyRepository.DeleteCompanyAsync(companyCode).ConfigureAwait(false);
        }

        public IEnumerable<CompanyInfo> GetAllCompanies()
        {
            var res = _companyRepository.GetAll();
            return _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }

        public CompanyInfo GetCompanyByCode(string companyCode)
        {
            var result = _companyRepository.GetByCode(companyCode);
            return _mapper.Map<CompanyInfo>(result);
        }

        public bool SaveCompany(CompanyInfo cieInfo)
        {
            var cie = _mapper.Map<Company>(cieInfo);
            return _companyRepository.SaveCompany(cie);
        }

        public bool DeleteCompany(string companyCode)
        {
            return _companyRepository.DeleteCompany(companyCode);
        }
    }
}
