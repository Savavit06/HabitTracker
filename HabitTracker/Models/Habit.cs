﻿namespace HabitTracker.Models
{
    public class Habit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AppUserId { get; set; } // Foreign Key
        public AppUser AppUser { get; set; } // Navigation Property
    }
}
