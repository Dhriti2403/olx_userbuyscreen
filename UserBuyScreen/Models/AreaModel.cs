﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserBuyScreen.Models
{
    public class AreaModel : CityModel
    {
        public int areaId { get; set; }
        public int cityId { get; set; }
        public string areaName { get; set; }
    }
}