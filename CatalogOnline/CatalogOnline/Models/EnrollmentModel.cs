﻿namespace CatalogOnline.Models
{
    public class EnrollmentModel
    {
        public int enrollment_id { get; set; }
        public int user_id { get; set; }
        public int course_id { get; set; }
        public DateTime joined_since { get; set; }
    }
}   
