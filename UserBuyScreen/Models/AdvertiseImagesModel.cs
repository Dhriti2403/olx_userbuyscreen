using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserBuyScreen.Models
{
    public class AdvertiseImagesModel : MyAdvertiseModel
    {
        public int advertiseImageId { get; set; }
        public int advertiseId { get; set; }    

        public byte[] imageData { get; set; }

        public DateTime createdOn { get; set; }
        public DateTime updatedOn { get; set; }
    }
}