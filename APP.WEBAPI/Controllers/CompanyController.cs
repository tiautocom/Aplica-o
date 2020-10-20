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
    public class CompanyController : ControllerBase
    {
        private readonly IRepository<Company> comp;
        private readonly IMapper mapper;

        public CompanyController(IRepository<Company> comp, IMapper mapper)
        {
            this.mapper = mapper;
            this.comp = comp;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var companies = await this.comp.GetAllAsync();
            var results = this.mapper.Map<CompanyDto[]>(companies);

            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CompanyDto model)
        {
             //Tela Empresa => Entidade Dto

            var entity = this.mapper.Map<Company>(model);
            await this.comp.Add(entity);

            if(await this.comp.SaveChangesAsync())
                return Created($"/api/company", this.mapper.Map<CompanyDto>(entity));

            return BadRequest();
        }

        
    }
}