using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADDyourAD.Models;

namespace ADDyourAD
{
    public class AdvertisementUtils
    {

        private DateTime tempAddDate;
        private DateTime tempExpDate;
        private int idAdvertisement;
        private int tempUser;
        public bool includeExpired;
        

        private static AdvertisementUtils _instance = null;
        public static AdvertisementUtils Instance
        {
            get
            {
                if (_instance == null) _instance = new AdvertisementUtils();
                return _instance;
            }
        }

        public DateTime getAddDate() { return tempAddDate; }

        public void setAddDate(DateTime date) { tempAddDate = date; }

        public DateTime getExpDate() { return tempExpDate; }

        public void setExpDate(DateTime date) { tempExpDate = date; }

        public bool isNotExpired(DateTime date) { return date > DateTime.Now; }

        public void setIdAd(int id) { idAdvertisement = id; }

        public int getIdAd() { return idAdvertisement; }

        public void setUser(int user) { tempUser = user; }

        public int getUser() { return tempUser; }

        
    }
}
