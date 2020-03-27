using DatabaseConnector.DAO.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabManagement.Utils
{
    public static class UserRoleCache
    {
        private static IDictionary<string, UserRoleWithTimestamp> _cache;
        public static void Init()
        {
            _cache = new Dictionary<string, UserRoleWithTimestamp>();
        }
        public static bool TryGetUserRole(string certification,out UserRoleResult result)
        {
            bool exist = _cache.ContainsKey(certification);
            result = null;
            var res = exist ? _cache[certification] : null;
            if (exist) result = res.Data;
            else return false;
            if (res.ExpireTime > DateTime.Now)
            {
                return true;
            }
            else
            {
                // Todo: move this part to EndPointJob or just leave this to redis.
                _cache.Remove(certification);
            }
            return false;
        }
        public static void AddUserRoleToCache(string certification, UserRoleResult userRole)
        {
            _cache.Add(certification, new UserRoleWithTimestamp(userRole));
        }
        public static void UpdateUserRole(string certification)
        {
            _cache[certification].ExpireTime = DateTime.Now.AddHours(1);
        }
        public static void RemoveUserRoleFromCache(string certification)
        {
            _cache.Remove(certification);
        }
        public class UserRoleWithTimestamp 
        {
            public UserRoleResult Data { get; set; }
            public DateTime ExpireTime { get; set; }
            public UserRoleWithTimestamp(UserRoleResult userRole)
            {
                Data = userRole;
                ExpireTime = DateTime.Now.AddHours(1);
            }
        }

    }
}
