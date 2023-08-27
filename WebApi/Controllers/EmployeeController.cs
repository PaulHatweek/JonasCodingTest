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
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper, ILogger logger)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET api/<controller>
        public async Task<IHttpActionResult> GetAllAsync()
        {            
            try
            {
                var items = await _employeeService.GetAllEmployeesAsync().ConfigureAwait(false);
                return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(items));
            }
            catch (Exception ex)
            {
                _logger.ErrorException($"Error during Get all Employee", ex);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // GET api/<controller>/5
        public async Task<IHttpActionResult> GetAsync(string employeeCode)
        {
            try
            {
                var item = await _employeeService.GetEmployeeByCodeAsync(employeeCode).ConfigureAwait(false);
                if (item != null)
                {
                    return Ok(_mapper.Map<EmployeeDto>(item));
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.ErrorException($"Error during Get Employee {employeeCode}", ex);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // POST api/<controller>
        public async Task<IHttpActionResult> Post([FromBody]EmployeeDto employeeDto)
        {
            try
            {
                var employeeInfo = _mapper.Map<EmployeeInfo>(employeeDto);
                if (await _employeeService.SaveEmployeeAsync(employeeInfo).ConfigureAwait(false))
                {
                    return StatusCode(HttpStatusCode.Created);
                }
                _logger.Error($"Employee {employeeDto.EmployeeCode} in Company {employeeDto.CompanyCode}  is not created");
                return StatusCode(HttpStatusCode.NotModified);
            }
            catch (Exception ex)
            {
                _logger.ErrorException($"Error during create Employee {employeeDto.EmployeeCode} in Company {employeeDto.CompanyCode}", ex);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // PUT api/<controller>/
        public IHttpActionResult Put([FromBody]EmployeeDto employeeDto)
        {
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE api/<controller>/aa
        public IHttpActionResult Delete(string employeeCode)
        {
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}