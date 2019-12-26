using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GradesApi.Models;
using GradesDomain;
using GradesRepository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GradesApi.Services
{
	public class UserService : IUserService
	{
		private readonly IPersonRepository _personRepository;
		private readonly IOptions<Secrets> _secrets;

		public UserService(IPersonRepository personRepository, IOptions<Secrets> secrets)
		{
			_personRepository = personRepository;
			this._secrets = secrets;
		}

		public string GetToken(string id, string password)
		{
			var guid = Guid.Parse(id);
			var user = _personRepository.ReadPerson(guid);
			if (!user.Password.Equals(password))
				throw new UnauthorizedAccessException();

			var isTeacher = user is Teacher;
			var role = isTeacher ? "Teacher" : "Student";

			
			// generate jwt
			var tokenHandler = new JwtSecurityTokenHandler();
			var jwt = _secrets.Value.Jwt;
			var key = Encoding.ASCII.GetBytes(jwt);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[] 
				{
					new Claim(ClaimTypes.Name, user.Id.ToString()),
					new Claim(ClaimTypes.Role, role)
				}),
				Expires = DateTime.UtcNow.AddHours(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}