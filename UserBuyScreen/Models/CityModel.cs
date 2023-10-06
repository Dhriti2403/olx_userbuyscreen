using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserBuyScreen.Models
{
    public class CityModel : StateModel
    {
        public int cityId { get; set; }
        public int stateId { get; set; }
        public string cityName { get; set; }
    }
}