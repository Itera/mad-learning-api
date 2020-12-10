namespace MadLearning.API.Application.Services
{
    public record UserInfo(string Id, string Email, string FirstName, string LastName)
    {
        public string FullName => $"{this.FirstName} {this.LastName}";
    }

    public interface ICurrentUserService
    {
        UserInfo GetUserInfo();
    }
}
