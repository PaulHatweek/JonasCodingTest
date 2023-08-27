using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using Ninject.Extensions.Logging;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CompanyController(ICompanyService companyService, IMapper mapper, ILogger logger)
        {
            _companyService = companyService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET api/<controller>
        public async Task<IHttpActionResult> GetAllAsync()
        {
            try
            {
                var items = await _companyService.GetAllCompaniesAsync().ConfigureAwait(false);
                return Ok(_mapper.Map<IEnumerable<CompanyDto>>(items));
            }
            catch (Exception ex)
            {
                _logger.ErrorException($"Error during Get all Companies", ex);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // GET api/<controller>/5
        public async Task<IHttpActionResult> GetAsync(string companyCode)
        {
            try
            {
                var item = await _companyService.GetCompanyByCodeAsync(companyCode).ConfigureAwait(false);
                if(item != null)
                {
                    return Ok(_mapper.Map<CompanyDto>(item));
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.ErrorException($"Error during Get Company {companyCode}", ex);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // POST api/<controller>
        public async Task<IHttpActionResult> PostAsync([FromBody]CompanyDto cieDto)
        {
            try
            {
                var cieInfo = _mapper.Map<CompanyInfo>(cieDto);
                if (await _companyService.SaveCompanyAsync(cieInfo).ConfigureAwait(false))
                {
                    return StatusCode(HttpStatusCode.Created);
                }
                _logger.Error($"Company {cieDto.CompanyCode} is not created");
                return StatusCode(HttpStatusCode.NotModified);
            }
            catch (Exception ex)
            {
                _logger.ErrorException($"Error during create Company {cieDto.CompanyCode}", ex);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // PUT api/<controller>/
        public async Task<IHttpActionResult> PutAsync([FromBody]CompanyDto cieDto)
        {
            try
            {
                var cieInfo = _mapper.Map<CompanyInfo>(cieDto);
                if (await _companyService.SaveCompanyAsync(cieInfo).ConfigureAwait(false))
                {
                    return Ok();
                }
                _logger.Error($"Company {cieDto.CompanyCode} is not updated");
                return StatusCode(HttpStatusCode.NotModified);
            }
            catch (Exception ex)
            {
                _logger.ErrorException($"Error during update Company {cieDto.CompanyCode}", ex);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // DELETE api/<controller>/aa
        public async Task<IHttpActionResult> DeleteAsync(string companyCode)
        {
            try
            {
                if (await _companyService.DeleteCompanyAsync(companyCode))
                {
                    return Ok();
                }
                _logger.Error($"Company {companyCode} is not deleted");
                return StatusCode(HttpStatusCode.NotModified);
            }
            catch (Exception ex)
            {
                _logger.ErrorException($"Error during delete Company {companyCode}", ex);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
    }
}