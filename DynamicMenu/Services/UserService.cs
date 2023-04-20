using DynamicMenu.Models;

namespace DynamicMenu.Services
{
    public class UserService
    {
        private static readonly List<User> validUsers = new List<User>
        {
            new User
            {
                Name = "None",
                Rights = new List<MenuButtonRight>()
            },
            new User
            {
                Name = "Archeologie",
                Rights = new List<MenuButtonRight> { MenuButtonRight.Archeologie }
            },
            new User
            {
                Name = "Bozp",
                Rights = new List<MenuButtonRight> { MenuButtonRight.BOZP }
            },
            new User
            {
                Name = "MonitoringBozp",
                Rights = new List<MenuButtonRight> { MenuButtonRight.Monitoring, MenuButtonRight.BOZP }
            },
            new User
            {
                Name = "ArcheologieMonitoring",
                Rights = new List<MenuButtonRight> { MenuButtonRight.Archeologie, MenuButtonRight.Monitoring }
            },
            new User
            {
                Name = "All",
                Rights = new List<MenuButtonRight> { MenuButtonRight.Archeologie, MenuButtonRight.Monitoring, MenuButtonRight.BOZP }
            }
        };

        /// <summary>
        /// <c>null</c> if not found
        /// </summary>
        public async Task<User> GetUserByName(string name)
        {
            await Task.Delay(4000);
            var user = (from item in validUsers where item.Name == name select item).SingleOrDefault();
            return user;
        }

        public async Task<List<User>> GetAll()
        {
            await Task.Delay(4000);
            return validUsers;
        }
    }
}
