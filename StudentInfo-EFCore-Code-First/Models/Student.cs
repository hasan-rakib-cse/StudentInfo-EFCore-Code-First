﻿using System.ComponentModel.DataAnnotations;

namespace StudentInfo_EFCore_Code_First.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int Roll { get; set; }

        [Required, StringLength(50)]
        public string Email { get; set; }

        [Required, StringLength(50)]
        public string Gender { get; set; }

        [Required]
        public int Age { get; set; }
    }
}
