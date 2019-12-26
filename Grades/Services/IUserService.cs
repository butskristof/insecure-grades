namespace GradesApi.Services
{
	public interface IUserService
	{
		string GetToken(string id, string password);
	}
}