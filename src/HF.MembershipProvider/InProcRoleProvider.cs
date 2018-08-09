using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace HF.MembershipProvider
{
    public class InProcRoleProvider : RoleProvider
    {
        //存放用户权限的字典
        Dictionary<string, List<string>> _Users = new Dictionary<string, List<string>>();
        //权限的列表
        List<string> _Roles = new List<string>();
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            CheckRoles(roleNames);
            foreach (var username in usernames)
            {
                foreach (var roleName in roleNames)
                    AddUserToRole(username, roleName);
            }
        }

        private void CheckRoles(string[] roleNames)
        {
            foreach (var newRole in roleNames)
            {
                if (!_Roles.Any(item => item == newRole))
                    throw new Exception(newRole + " 不存在");
            }

        }
        private void AddUserToRole(string username, string roleName)
        {
            if (_Users.ContainsKey(username))
            {
                var roles = _Users[username];
                if (roles.Any(name => name == roleName))
                    return;
                roles.Add(roleName);
            }
            else
            {
                _Users.Add(username, new List<string>() { roleName });
            }
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            _Roles.Add(roleName);
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            _Roles.Remove(roleName);
            _Users = _Users.Where(item =>
            {
                return item.Value.Any(v => v == roleName) == false;
            })
                     .ToDictionary(item => item.Key, item => item.Value);
            return true;

        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            if (_Users.ContainsKey(username))
                return _Users[username].ToArray();
            return new string[0];
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            if (_Users.ContainsKey(username))
            {
                return _Users[username].Any(role => role == roleName);
            }
            return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            return _Roles.Any(role => role == roleName);
        }
    }
}
