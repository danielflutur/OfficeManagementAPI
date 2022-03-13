using System.Collections.Generic;
using System.Linq;
using OfficeManagementAPI.Models;

namespace OfficeManagementAPI.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<Employees> WithoutPasswords(this IEnumerable<Employees> users)
        {
            if (users == null) return null;

            return users.Select(x => x.WithoutPassword());
        }

        public static Employees WithoutPassword(this Employees user)
        {
            if (user == null) return null;

            user.Passw = null;
            return user;
        }
    }
}
