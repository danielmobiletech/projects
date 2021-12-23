using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using JWTAuth.Data;
using JWTAuth.Dtos;
using JWTAuth.Migrations;
using JWTAuth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWTAuth.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        public AuthController(IUserRepository userRepository,JwtService jwtService)
        {
            this._userRepository = userRepository;
            this._jwtService = jwtService;
        }
        // GET: api/values

        [HttpPost("Register")]
       
        //[Consumes]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            var user = new User()
            {
                Name = registerDto.Name,
                Email=registerDto.Email,
                Password=BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };
            return Created("created", _userRepository.Create(user));
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            //userRepository.GetUserByEmail(loginDto.Email);
            var vals = _userRepository.GetUserByEmail(loginDto.Email);
            if (vals == null) return BadRequest();
            if (BCrypt.Net.BCrypt.Verify(loginDto.Password, vals.Password) == false)
            {
                return BadRequest();
            }
            //var ttott=
            //ttott.
            var jwt = this._jwtService.Generate(vals.Id);
            Response.Cookies.Append("jwt", jwt, new CookieOptions { 
            });
            return Ok(new { message = "succes bro" });
        }


        [HttpPost("logout")]
        public IActionResult logout()
        {

            Response.Cookies.Delete("jwt");
            return Ok();
        }


        [HttpGet("user")]
        public IActionResult Userz()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var securityToken = _jwtService.Verify(jwt);

                var userId = int.Parse(securityToken.Issuer);
                var user = this._userRepository.GetUserById(userId);


                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }


        [HttpGet("userstate")]
        public IActionResult UserState()
        {
            bool state = false;
            if (Request.Cookies["jwt"] != null)
                return Ok(new { loginstate = true });
            return Ok(new { loginstate = state });


        }
        [HttpGet("userd" +
            "")]
        public IActionResult Userzed()
        {
           var data=this._jwtService.Verify(Request.Cookies["jwt"]);
            var pp = int.Parse(data.Issuer);
            var whh = _userRepository.GetUserById(pp);
            return Ok(new { identity = whh });


        }
    }
}
