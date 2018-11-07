using System.Web.Services;
using System.Web.UI;
using WhatINoted.ConnectionManagers;
using WhatINoted.Models;

namespace WhatINoted
{
    public class View : Page
    {
        protected User User;

        [WebMethod]
        public static void HandleLogin(string userID, string displayName, string email)
        {
            GoogleFirestoreConnectionManager.HandleLogin(userID, displayName, email);
        }
    }
}