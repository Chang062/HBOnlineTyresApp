﻿using HBOnlineTyresApp.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace HBOnlineTyresApp.Models
{
    public class Message: IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }

        public DateTime DateReceived { get; set; }
        public bool? Viewed { get; set; }


    }
}