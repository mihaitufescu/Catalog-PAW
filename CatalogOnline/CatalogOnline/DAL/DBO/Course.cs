﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace CatalogOnline.DAL.DBO
{
    public class Course
    {
        public Course()
        {
          
        }

        [Key]
        public int course_id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]  
        public string subject { get; set; }

        [Required]
        public bool mandatory { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int credits_number { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int year { get; set; }
    }
}