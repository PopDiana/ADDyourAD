using ADDyourAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADDyourAD
{
    public class Authentication
    {

        private bool logged;
        private User currentUser;
        private bool admin;
        
        

        private static Authentication _instance = null;
        public static Authentication Instance
        {
            get
            {
                if (_instance == null) _instance = new Authentication();
                return _instance;
            }
        }

        public void AccountLogin(User user)
        {
            currentUser = user;
            logged = true;
            admin = false;
        }

        public void AdminLogin(User user)
        {
            logged = true;
            admin = true;
            currentUser = null;
        }

        public void AccountLogout()
        {
            logged = false;
            admin = false;
        }

        public bool isLoggedIn() { return logged; }

        public bool isAdmin() { return admin; }

        public User getCurrentUser() { return currentUser; }
        
    }
}
