using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Ponta.Dominio.Entidades;
using Ponta.Dominio.IRepositorios;
using Ponta.Dominio.IServicos;
using Ponta.Servico.Validadores;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Ponta.Servico.Servicos
{
    public class ServicoLogin : ServicoBase<Usuario>,IServicoLogin
    {

        private readonly IRepositorioUsuario _repositorioBaseUsuario;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public ServicoLogin(IRepositorioUsuario repositorioBaseUsuario, IConfiguration configuration, IMapper mapper) : base(repositorioBaseUsuario, mapper)
        {
            _repositorioBaseUsuario = repositorioBaseUsuario;
            _configuration = configuration;
            _mapper = mapper;
        }
        
        public string Login(string login, string senha)
        {
            var usuario = _repositorioBaseUsuario.Login(login, senha);
            if(usuario!= null)
                return GerarTokenJWT(usuario);

            return "Usuário não encontrado ou senha e/ou login incorretos.";
        }
        private string GerarTokenJWT(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claimsIdentity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
           
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, usuario.Login));

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(4),
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
