using CaarInsurance.Data;
using CaarInsurance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaarInsurance.Controllers
{
    public class InsureeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsureeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Insuree
        public async Task<IActionResult> Index()
        {
         
            return View(await _context.Insurees.ToListAsync());
        }

        public IActionResult Admin()
        {
            foreach (var insuree in _context.Insurees)
            {
                decimal quote = 50m;
                // Age-based adjustments
                var age = DateTime.Now.Year - insuree.DateOfBirth.Year;
                if (insuree.DateOfBirth > DateTime.Now.AddYears(-age)) age--;
                if (age < 18)
                {
                    quote += 100m;
                }
                else if (age <= 25)
                {
                    quote += 50m;
                }
                else
                {
                    quote += 25m;
                }
                // Car year adjustments
                if (insuree.CarYear < 2000 || insuree.CarYear > 2015)
                {
                    quote += 25m;
                }
                // Car make and model adjustments
                if (insuree.CarMake.ToLower() == "porsche")
                {
                    quote += 25m;
                    if (insuree.CarModel.ToLower() == "911 carrera")
                    {
                        quote += 25m;
                    }
                }
                // Speeding tickets adjustment
                quote += insuree.SpeedingTickets * 10m;
                // DUI adjustment
                if (insuree.HasDUI)
                {
                    quote += 12.5m;
                }
                // Coverage type adjustment
                if (insuree.CoverageType.ToLower() == "full")
                {
                    quote += 25m;
                }
                insuree.Quote = quote;
            }

            
            var insurees = _context.Insurees.ToList();

            return View(insurees);
          
        }

        // GET: Insuree/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuree = await _context.Insurees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuree == null)
            {
                return NotFound();
            }

            return View(insuree);
        }

        // GET: Insuree/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Insuree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,HasDUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuree);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insuree);
        }

        // GET: Insuree/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuree = await _context.Insurees.FindAsync(id);
            if (insuree == null)
            {
                return NotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,HasDUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (id != insuree.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuree);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsureeExists(insuree.Id))
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
            return View(insuree);
        }

        // GET: Insuree/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuree = await _context.Insurees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuree == null)
            {
                return NotFound();
            }

            return View(insuree);
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insuree = await _context.Insurees.FindAsync(id);
            if (insuree != null)
            {
                _context.Insurees.Remove(insuree);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsureeExists(int id)
        {
            return _context.Insurees.Any(e => e.Id == id);
        }
    }
}
