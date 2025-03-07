using Microsoft.AspNetCore.Identity;

namespace HabitTracker.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Habit> Habits { get; set; }
    }
}
