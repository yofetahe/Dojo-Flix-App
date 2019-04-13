using System;

namespace DotnetFlix.Models
{
    public abstract class CreateUpdate
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
    }
}