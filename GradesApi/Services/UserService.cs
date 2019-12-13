using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GradesDomain;
using GradesRepository;
using Microsoft.IdentityModel.Tokens;

namespace GradesApi.Services
{
	public class UserService : IUserService
	{
		private readonly IPersonRepository _personRepository;

		public UserService(IPersonRepository personRepository)
		{
			_personRepository = personRepository;
		}

		public string GetToken(string id, string password)
		{
			var guid = Guid.Parse(id);
			var user = _personRepository.ReadPerson(guid);
			if (!user.Password.Equals(password))
				throw new UnauthorizedAccessException();

			var isTeacher = user is Teacher;
			var role = isTeacher ? "teacher" : "student";
			
			// generate jwt
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes("veryverysecretsecretykeykey");
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[] 
				{
					new Claim(ClaimTypes.Name, user.Id.ToString()),
					new Claim(ClaimTypes.Role, role)
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}