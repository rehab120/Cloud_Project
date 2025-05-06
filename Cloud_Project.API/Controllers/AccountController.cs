using Cloud_Project.Application.Commond.LoginMerchant;
using Cloud_Project.Application.Commond.RegisterMerchant;
using Cloud_Project.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cloud_Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("Register_Merchant")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            var command = new RegisterCommand(register.UserName, register.Email, register.Passsword);
            var result = await mediator.Send(command);

            if (result.Success)
            {

                return Ok("User Register Successfully");
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var commandL = new LoginCommand(login.UserName,login.Password);
            var resultL = await mediator.Send(commandL);

            if (resultL.success)
            {

                return Ok(new {token = resultL.Token});
            }
            return BadRequest(resultL.Errors);
        }





    }
}
