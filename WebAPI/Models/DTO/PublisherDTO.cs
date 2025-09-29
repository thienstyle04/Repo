﻿using System.ComponentModel.DataAnnotations;
namespace WebAPI.Models.DTO
{
    public class PublisherDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PublisherNoIdDTO
    {
        public string Name { get; set; }
    }

    public class AddPublisherRequestDTO
    {
        public string Name { get; set; }
    }
}
