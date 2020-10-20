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
    public class AddressController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRepository<Address> rep;

        public AddressController(IRepository<Address> rep, IMapper mapper)
        {
            this.mapper = mapper;
            this.rep = rep;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           var addresses = await this.rep.GetAllAsync();
           var results = this.mapper.Map<AddressDto[]>(addresses);

           return Ok(results);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(string id)
        {
           var address = await this.rep.GetByIdAsync(id);
           var result = this.mapper.Map<AddressDto[]>(id);

           return Ok(result);
        }

         

        [HttpPost]
        public async Task<IActionResult> Post(AddressDto model)
        {
            //Tela Endereco => Entidade Dto

            var entity = this.mapper.Map<Address>(model);
            await this.rep.Add(entity);

            if (await this.rep.SaveChangesAsync())
                return Created($"api//address", this.mapper.Map<AddressDto>(entity));

            return BadRequest();

        }

    }
}