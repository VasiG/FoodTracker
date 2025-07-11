using FoodTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index()
        {
            return View(await _context.Measures.OrderByDescending(measure => measure.MeasureDate).ToListAsync());
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measure = await _context.Measures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (measure == null)
            {
                return NotFound();
            }

            return View(measure);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MeasureDate,SugarLevelBefore,SugarLevelAfter,MealType,Notes")] Measure measure)
        {
            if (ModelState.IsValid)
            {
                measure.MealType = Enum.Parse<MealType>(measure.MealType).ToString();
                _context.Add(measure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(measure);
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measure = await _context.Measures.FindAsync(id);
            if (measure == null)
            {
                return NotFound();
            }
            return View(measure);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MeasureDate,SugarLevelBefore,SugarLevelAfter,MealType,Notes")] Measure measure)
        {
            if (id != measure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    measure.MealType = Enum.Parse<MealType>(measure.MealType).ToString();
                    _context.Update(measure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeasureExists(measure.Id))
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
            return View(measure);
        }

        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measure = await _context.Measures
                .FirstOrDefaultAsync(m => m.Id == id);

            if (measure == null)
            {
                return NotFound();
            }

            return View(measure);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var measure = await _context.Measures.FindAsync(id);
            if (measure != null)
            {
                _context.Measures.Remove(measure);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeasureExists(int id)
        {
            return _context.Measures.Any(e => e.Id == id);
        }
    }
}
