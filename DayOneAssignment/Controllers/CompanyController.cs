using DayOneAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DayOneAssignment.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompanyController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult CompanyList()
        {
            var cam = new List<Company>
            {
                new Company()
                {
               //id = new Guid(),
                CompanyId =1,
                CompanyName = "abc",
                CompanyAddress = "addis",



                },
                 new Company()
                {
               //id = new Guid(),
                CompanyId =2,
                CompanyName = "abc",
                CompanyAddress = "addis",



                },
                  new Company()
                {
               //id = new Guid(),
                CompanyId =3,
                CompanyName = "abc",
                CompanyAddress = "addis",



                }

            };
            return View(cam);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
              _context.Companys.Add(company);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

             return View(company);
        }



        [HttpGet]
        public IActionResult Edit(int CompanyId)
        {
            return View(_context.Companys.FirstOrDefault(a=>a.CompanyId==CompanyId));
        }
        [HttpPost]
        public IActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Companys.Update(company);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company);
        }
        public async Task<IActionResult> Index()
        {
            var company= await _context.Companys.ToListAsync(); 
            return View(company);
        }

        public async Task<IActionResult> Details(int? compId)
        {
          //var company = _context.Companys.FirstOrDefaultAsync(x=>x.CompanyId== compId);
          //  return View(company);


            if(compId == null)
            {
                return NotFound();
            }
            var company = await _context.Companys.FirstOrDefaultAsync(x=> x.CompanyId == compId);
            if (compId == null)
            {
                return NotFound();
            }
            return View(company);
        }
        
        // GET: Employees/Delete/1
        public async Task<IActionResult> Delete(int? CompId)
        {
            if (CompId == null)
            {
                return NotFound();
            }
            var company = await _context.Companys.FirstOrDefaultAsync(x => x.CompanyId == CompId);

            return View(company);
        }

        // POST: Employees/Delete/1
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Company company)
        {
            var company1 = await _context.Companys.FindAsync(company.CompanyId);
            if(company1 != null) {
                _context.Companys.Remove(company1);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return NotFound();
           
        }
        
    }
}
