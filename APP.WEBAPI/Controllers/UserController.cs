using System.Threading.Tasks;
using APP.DOMAIN;
using APP.DOMAIN.Identity;
using APP.REPOSITORY.Interface;
using APP.WEBAPI.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace APP.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IRepository<User> rep;
        private readonly IMapper mapper;
        private readonly IConfiguration config;

        public UserController(IConfiguration config,
                                UserManager<User> userManager,
                                SignInManager<User> signInManager,
                                IRepository<User> rep, IMapper mapper)
        {
            this.config = config;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.rep = rep;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await this.rep.GetAllAsync();
                 var results = this.mapper.Map<UserDto[]>(users);

                return Ok(results);
                // return Ok(new UserDto());
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError,
                   $"Banco de dados falhou: {ex.Message}");
            }


            // return Ok(new UserDto());
        }

        [HttpGet("{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var user = await this.rep.GetByIdAsync(id);
                var result = this.mapper.Map<UserDto>(user);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError,
                   $"Banco de dados falhou: {ex.Message}");
            }
        }

        // [HttpPost("Login")]
        // [AllowAnonymous]
        // public async Task<IActionResult> Login(UserLoginDto userLogin)
        // {
        //       try
        //       {

        //       }
        //       catch (System.Exception ex)
        //       {
        //           return this.StatusCode(StatusCodes.Status500InternalServerError,
        //           $"Banco de dados Falhou: {ex.Message}");
        //       }
        //       return Unauthorized();

        // }

        // [HttpPut("{Id}")]
        // public async Task<IActionResult> Put(string id, UserDto model)
        // {
        //     try
        //     {

        //         var user = await this.rep.GetByIdAsync(id);

        //         this.mapper.Map(model, user);

        //         this.rep.Udpate(user);

        //         if (await this.rep.SaveChangesAsync())
        //             return Created($"/api/user/{model.Id}", this.mapper.Map<UserDto>(user));
        //     }
        //     catch (System.Exception ex)
        //     {
        //         return this.StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError,
        //            $"Banco de dados falhou: {ex.Message}");
        //     }

        //     return BadRequest();
        // }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(UserDto model)
        {
            //Tela Usuario => Entidade Dto
            try
            {
                var user = this.mapper.Map<User>(model); 

                var result = await this.userManager.CreateAsync(user, model.Password);

                var userToReturn = this.mapper.Map<UserDto>(user);

                if(result.Succeeded)
                {
                    return Created("GetUser", userToReturn);
                } 
                return BadRequest(result.Errors);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Banco de Dados Falhou: {ex.Message}");
            }

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            try
            {
                var entity = await this.rep.GetByIdAsync(Id); //Buscando usuario pelo id
                if (entity == null) return NotFound(); //verificar se existe a entidade

                this.rep.Delete(entity); // Se existe a entidade, deletar.
                if (await this.rep.SaveChangesAsync())
                    return Ok();

                //
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Banco de dados falhou: {ex.Message}");

            }

            return BadRequest();


        }

    }
}