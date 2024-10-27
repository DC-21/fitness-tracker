using System;

namespace Fit
{
    public class UserSession
    {
        private static readonly UserSession _instance = new UserSession();

        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Id { get; private set; }
        public bool IsLoggedIn { get; private set; }

        private UserSession()
        {
            IsLoggedIn = false;
        }

        public static UserSession Instance => _instance;

        public void SetUserData(string userName, string email, string userId)
        {
            UserName = userName;
            Email = email;
            Id = userId;
            IsLoggedIn = true;
        }

        public void ClearUserData()
        {
            UserName = null;
            Email = null;
            Id = null;
            IsLoggedIn = false;
        }
    }
}
