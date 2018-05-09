﻿using System.ComponentModel.DataAnnotations;

namespace HotelApi
{
    public class HotelRegion
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}