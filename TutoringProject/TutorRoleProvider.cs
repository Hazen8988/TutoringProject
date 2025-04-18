using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace TutoringProject.Models
{
    public class TutorRoleProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get { return "TutoringProject"; }
            set { /* Do nothing */ }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            using (var context = new TutorContext())
            {
                var users = context.UserAccounts
                    .Where(u => u.Role.Equals(roleName, StringComparison.OrdinalIgnoreCase) && u.Email.Contains(usernameToMatch))
                    .Select(u => u.Email)
                    .ToArray();
                return users;
            }
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (var context = new TutorContext())
            {
                var user = context.UserAccounts.FirstOrDefault(u => u.Email == username);
                if (user != null)
                {
                    return new[] { user.Role };
                }
            }
            return new string[0];
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (var context = new TutorContext())
            {
                var user = context.UserAccounts.FirstOrDefault(u => u.Email == username);
                if (user != null)
                {
                    return user.Role.Equals(roleName, StringComparison.OrdinalIgnoreCase);
                }
            }
            return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}