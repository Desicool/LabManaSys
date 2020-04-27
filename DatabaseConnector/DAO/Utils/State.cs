using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DatabaseConnector.Utils
{
    public class StateUtil
    {
        // Key值为当前状态
        public IDictionary<string, Pair> StateRoute { get; set; }
        public class Pair
        {
            //需要通知的角色名称，暂定为一种角色审批
            public string RoleName { get; set; }
            // 下一个状态，默认0项为失败跳转，1项为成功跳转
            public List<string> Next { get; set; }
        }
        public StateUtil(IConfiguration config)
        {
            StateRoute = config.GetSection("stateRoute").Get<Dictionary<string,Pair>>();
        }
        
    }
    public enum FormState { None = 0, InProcess, Approved, Rejected, Returned }
}
