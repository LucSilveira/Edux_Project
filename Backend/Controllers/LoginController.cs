using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using EduxProject.Contexts;
using EduxProject.Domains;
using EduxProject.Utils;

namespace EduxProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //estruturando o LoginController
    public class LoginController : ControllerBase
    {
        EduxContext _contexto = new EduxContext();

        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        private Usuario AuthenticateUser(Usuario login)
        {
            login.Senha = Crypto.Criptografar(login.Senha, login.Email.Substring(0, 4));


            return _contexto.Usuario.Include(a => a.IdPerfilNavigation).FirstOrDefault(u => u.Email == login.Email && u.Senha == login.Senha);
        }

        //geração de token
        private string GenerateJSONWebToken(Usuario userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.NameId, userInfo.Nome),
            new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, userInfo.IdPerfilNavigation.Permissao),
            new Claim("IdUsuario", userInfo.Id.ToString()),
            new Claim("Role", userInfo.IdPerfilNavigation.Permissao)
    };

            var token = new JwtSecurityToken
                (
                    _config["Jwt:Issuer"],
                    _config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Método para fazer o login da aplicação
        /// </summary>
        /// <param name="login">dados referente ao login de usuário</param>
        /// <returns>Código de login do usuário</returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] Usuario login)
        {

            IActionResult response = Unauthorized();

            var user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

    }
}
