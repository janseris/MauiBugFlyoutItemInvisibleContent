using DynamicMenu.Models;

namespace DynamicMenu.EventArg
{
    public class LoginEventArgs : EventArgs
    {
        public User User { get; private set; }
        public LoginEventArgs(User user)
        {
            User = user;
        }
    }
}
