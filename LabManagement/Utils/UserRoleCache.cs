using DatabaseConnector.DAO.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabManagement.Utils
{
    public static class UserRoleCache
    {
        private static IDictionary<string, UserRole> _cache;
        public static void Init()
        {
            _cache = new Dictionary<string, UserRole>
            {
            };
        }
        public static bool TryGetUserRole(string certification,out UserRole result)
        {
            bool res = _cache.ContainsKey(certification);
            result = res ? _cache[certification] : null;
            return res;
        }

    }
}
