﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackCogs.Data.Models;

namespace MultiPlex.Core.Data.Models
{
   public class WikiModInvitations
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Wiki Wiki { get; set; }
        [Required]
        public string Moderator { get; set; }
        [Timestamp]
        public Byte[] RowVersion { get; set; }
    }
}
