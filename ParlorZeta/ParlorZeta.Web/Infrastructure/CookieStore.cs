using System;
using System.Web;
using ParlorZeta.Azure.Contracts;

namespace ParlorZeta.Web.Infrastructure
{
    public class CookieStore : IUserSettings
    {
        private readonly HttpContext _context;

        public CookieStore(HttpContext context)
        {
            _context = context;
        }

        public string GetSelectedSubscriptionId()
        {
            return Get(SubscriptionId);
        }

        public void SetSelectedSubscriptionId(string value)
        {
            Set(SubscriptionId, value);
        }

        private void Set(string key, string value)
        {
            var cookie = new HttpCookie(key, value) {Expires = DateTime.Now.AddYears(2), HttpOnly = true};
            _context.Response.Cookies.Remove(key);
            _context.Response.Cookies.Set(cookie);
        }

        private string Get(string key)
        {
            var cookie = _context.Request.Cookies[key];
            if (cookie != null)
            {
                return cookie.Value;
            }
            return null;
        }

        private const string SubscriptionId = "_CurrentSub";
    }
}