namespace Demo.API.Infrastructure.Auth
{
    public class UserInfo
    {
        private const string AnonymousUserName = "Anonymous";
        private const string BackgroundJobName = "System Background Job";

        private UserInfo()
        {
        }

        public UserInfo(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public static UserInfo ForAnonymousUser()
        {
            return new UserInfo
            {
                Id = AnonymousUserName,
                Name = AnonymousUserName,
                Email = AnonymousUserName
            };
        }

        public static UserInfo ForBackgroundJob()
        {
            return new UserInfo
            {
                Id = BackgroundJobName,
                Name = BackgroundJobName,
                Email = BackgroundJobName
            };
        }

        public string Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string Email { get; private set; }
    }
}
