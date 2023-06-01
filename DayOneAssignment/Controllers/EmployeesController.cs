using DayOneAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace DayOneAssignment.Controllers
{

    public class EmployeesController : Controller
    {

        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View(_context.Employees.Include(a => a.Company).ToList());
        //    //var employee = await _context.Employees.ToListAsync();
        //    //return View(employee);
        //}
        public async Task<IActionResult> Index(string sortOrder, string currentFilter,string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            var employees = from s in _context.Employees
                           select s;


            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.EmployeeFirstName.Contains(searchString)
                                       || s.EmployeeLastName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(s => s.EmployeeFirstName);
                    break;
                case "Date":
                    employees = employees.OrderBy(s => s.EmployeeLastName);
                    break;
                //case "date_desc":
                //    students = students.OrderByDescending(s => s.EnrollmentDate);
                //    break;
                default:
                    employees = employees.OrderBy(s => s.EmployeeFirstName);
                    break;
            }
            _context.Employees.Include(a => a.Company).ToList();
            int pageSize = 5;
            return View(await PaginatedList<Employee>.CreateAsync(employees.AsNoTracking(), pageNumber ?? 1, pageSize));

            //return View(await employees.AsNoTracking().ToListAsync());
        }

        ////AddOrEdit Get Method
        //public async Task<IActionResult> AddOrEdit(int? employeeId)
        //{
        //    ViewBag.PageName = employeeId == null ? "Create Employee" : "Edit Employee";
        //    ViewBag.IsEdit = employeeId == null ? false : true;
        //    if (employeeId == null)
        //    {
        //        ViewBag.Companies = new SelectList(_context.Companys.ToList(), "CompanyId", "CompanyName");
        //        return View();
        //    }
        //    else
        //    {
        //        var employee = await _context.Employees.FindAsync(employeeId);

        //        if (employee == null)
        //        {
        //            return NotFound();
        //        }
        //        ViewBag.Companies = new SelectList(_context.Companys.ToList(), "CompanyId", "CompanyName");
        //        return View(employee);
        //    }
        //}

        ////AddOrEdit Post Method
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddOrEdit(int employeeId,Employee employeeData)
        //{
        //    bool IsEmployeeExist = false;

        //    Employee employee = await _context.Employees.FindAsync(employeeId);

        //    if (employee != null)
        //    {
        //        IsEmployeeExist = true;
        //    }
        //    else
        //    {
        //        employee = new Employee();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            employee.EmployeeFirstName = employeeData.EmployeeFirstName;
        //            employee.EmployeeLastName = employeeData.EmployeeLastName;
        //            employee.BirthDate = employeeData.BirthDate;
        //            employee.Company= employeeData.Company;


        //            if (IsEmployeeExist)
        //            {
        //                _context.Update(employee);
        //            }
        //            else
        //            {
        //                _context.Add(employee);
        //            }
        //            await _context.SaveChangesAsync();

        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            throw;
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(employeeData);
        //}

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Companies = new SelectList(_context.Companys.ToList(), "CompanyId", "CompanyName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {

            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                //return View("Index", _context.Employees.ToList());
                return RedirectToAction(nameof(Index) , _context.Employees.ToList());
            }

            return View();
        }


        [HttpGet]
        public IActionResult Edit(int EmployeeId)
        {
            ViewBag.Companies = new SelectList(_context.Companys.ToList(), "CompanyId", "CompanyName");
            return View(_context.Employees.FirstOrDefault(a => a.EmployeeId == EmployeeId));
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }
        // GET: Employees/Delete/1
        public async Task<IActionResult> Delete(int? EmployeeId)
        {
            if (EmployeeId == null)
            {
                return NotFound();
            }
            var company = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == EmployeeId);

            return View(company);
        }

        // POST: Employees/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Employee employee)
        {
            var emp1 = await _context.Employees.FindAsync(employee.EmployeeId);
            if (emp1 != null)
            {
                _context.Employees.Remove(emp1);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return NotFound();

        }
        public async Task<IActionResult> Details(int? EmployeeId)
        {
            //var company = _context.Companys.FirstOrDefaultAsync(x=>x.CompanyId== compId);
            //  return View(company);


            if (EmployeeId == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == EmployeeId);
            if (EmployeeId == null)
            {
                return NotFound();
            }
            return View(employee);
        }


    }
}
