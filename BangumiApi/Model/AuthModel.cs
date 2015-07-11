using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpPostUtility;

namespace BangumiApi.Model
{
    class AuthModel
    {
        [PostProperty("username")]
        public string Username { get; set; }

        [PostProperty("password")]
        public string Password { get; set; }

        [PostProperty("source")]
        public string Source { get; set; }
    }
}
