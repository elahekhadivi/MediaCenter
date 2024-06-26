using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MediaCenter.Models;

     public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? Director { get; set; }
        public string? ReleaseDate { get; set; }
        public string? Rating { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public Uri? IMDBLink { get; set; }
        public string? FileAddress { get; set; }
    }
