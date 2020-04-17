﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DBSystem.ENTITIES
{
    [Table("Guardian")]
    public class Entity01
    {
        [Key]
        public int GuardianID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmergencyPhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        //public byte[] Picture { get; set; }
        //public string PictureMimeType { get; set; }
        public string FullName
        {
            get
            {
                return LastName + "," + FirstName;
            }
        }
    }
}
