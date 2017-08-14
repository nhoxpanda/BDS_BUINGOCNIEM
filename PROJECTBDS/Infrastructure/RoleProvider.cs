using System;
using System.Data.Entity;
using System.Linq;
using PROJECTBDS;
using PROJECTBDS.Models;

namespace PROJECTBDS.Infrastructure
{
    public class RoleProvider : System.Web.Security.RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var db = new LandSoftEntities();

            var roles = db.AspNetUsers.Include(t => t.AspNetRoles).FirstOrDefault(t => t.UserName == username);

            if (roles == null) return new[] { "User" };

            var roleUser = roles.AspNetRoles.FirstOrDefault();

            if (roleUser != null) return new []{ roleUser.Name };

            return new[] { "User" };
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}