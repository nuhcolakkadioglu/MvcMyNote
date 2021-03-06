﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.Enties
{
    [Table("NoteUsers")]
    public class NoteUser:MyEntityBase
    {
        [StringLength(25)]
        public string Name { get; set; }
        [StringLength(25)]
        public string Surname { get; set; }
        [Required,StringLength(25)]
        public string Username { get; set; }
        [Required, StringLength(100)]
        public string Email { get; set; }
        [Required, StringLength(100)]
        public string Password { get; set; }
        [Required, ScaffoldColumn(false)]
        public Guid ActivateGuid { get; set; }

        [ScaffoldColumn(false)]
        public string ProfileImageFileName { get; set; } 

        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
       
    

        public virtual List<Note> Notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }

    }
}
