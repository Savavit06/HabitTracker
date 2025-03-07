using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HabitTracker.Data;
using HabitTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HabitTracker.Controllers
{
    [Authorize]
    public class HabitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<AppUser> _userManager;

        public HabitsController(ApplicationDbContext context, UserManager<AppUser> manager)
        {
            _context = context;
            _userManager = manager;
        }

        // GET: Habits

        public async Task<IActionResult> Index()
        {
            var userID = _userManager.GetUserId(User);
            var habits = await _context.Habit.Where(h => h.AppUserId == userID).ToListAsync();
            return View(habits);
        }

        // GET: Habits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habit = await _context.Habit
                .FirstOrDefaultAsync(m => m.Id == id);
            if (habit == null)
            {
                return NotFound();
            }

            return View(habit);
        }

        // GET: Habits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Habits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,AppUserId")] Habit habit)
        {
            
            if (ModelState.IsValid)
            {
                var id = _userManager.GetUserId(User);
                habit.AppUserId = id;
                _context.Add(habit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(habit);
        }

        // GET: Habits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habit = await _context.Habit.FindAsync(id);
            if (habit == null)
            {
                return NotFound();
            }
            return View(habit);
        }

        // POST: Habits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Habit habit)
        {
            if (id != habit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(habit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabitExists(habit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(habit);
        }


        // GET: Habits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habit = await _context.Habit
                .FirstOrDefaultAsync(m => m.Id == id);
            if (habit == null)
            {
                return NotFound();
            }

            return View(habit);
        }

        // POST: Habits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var habit = await _context.Habit.FindAsync(id);
            if (habit != null)
            {
                _context.Habit.Remove(habit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HabitExists(int id)
        {
            return _context.Habit.Any(e => e.Id == id);
        }
    }
}
