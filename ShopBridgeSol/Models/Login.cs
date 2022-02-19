using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeSol.Models
{
    public class Login
    {
        public int UserID { get; set; } = 0;
        public  string UserName { get; set; } = "";
        public  string Password { get; set; } = "";
        public  string Name { get; set; } = "";
        public  string MobileNo { get; set; } = "";
        public  string mail { get; set; } = "";
        public  string jwtTokan { get; set; } = "";

        public string DateOfJoing { get; set; } = System.DateTime.Now.ToString("yyyy-MM-dd");
        
    }
}
