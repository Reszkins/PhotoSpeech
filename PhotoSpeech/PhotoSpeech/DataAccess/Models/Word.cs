﻿namespace PhotoSpeech.DataAccess.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}
