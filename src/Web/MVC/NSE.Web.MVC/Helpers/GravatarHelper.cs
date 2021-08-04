using NSE.Web.MVC.Extensions;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NSE.Web.MVC.Helpers
{
    public static class GravatarHelper
    {
        public static string GetAvatar(this IUser user)
        {
            var hash = MD5.Create().ComputeHash(Encoding.Default.GetBytes(user.Email));
            var builder = new StringBuilder();
            hash.ToList().ForEach(x => builder.Append(x.ToString("x2")));
            return $"https://s.gravatar.com/avatar/{builder}?d=mm&s=40";
        }
    }
}
