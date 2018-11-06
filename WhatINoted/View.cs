using System.Web.Services;
using System.Web.UI;
using WhatINoted.ConnectionManagers;

namespace WhatINoted
{
    public class View : Page
    {
        [WebMethod]
        public static void HandleLogin(string userID, string displayName, string email)
        {
            GoogleFirestoreConnectionManager.HandleLogin(userID, displayName, email);
        }
    }
}