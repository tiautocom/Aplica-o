using System.Threading.Tasks;
using APP.DOMAIN;
using APP.REPOSITORY.Interface;
using APP.WEBAPI.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APP.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IRepository<Company> rep;
        private readonly IMapper mapper;

        public EmpresaController(IRepository<Company> rep, IMapper mapper)
        {
            this.rep = rep;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var empresas = await this.rep.GetAllAsync();
            var results = this.mapper.Map<CompanyDto[]>(rep);

            return Ok(results);
        }
    }
}