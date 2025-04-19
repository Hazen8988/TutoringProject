using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;
using TutoringProject.Models;

namespace TutoringProject
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
                    Debug.WriteLine($"User {username} found with role {user.Role}.");
                    return new[] { user.Role };
                }
            }
            Debug.WriteLine($"User {username} not found in the database.");
            return new string[0];
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            Debug.WriteLine($"Checking if user {username} is in role {roleName}.");
            using (var context = new TutorContext())
            {
                var user = context.UserAccounts.FirstOrDefault(u => u.Email == username);
                if (user != null)
                {
                    Debug.WriteLine($"User {username} found with role {user.Role}.");
                    return user.Role.Equals(roleName, StringComparison.OrdinalIgnoreCase);
                }
            }
            Debug.WriteLine($"User {username} not found in the database.");
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