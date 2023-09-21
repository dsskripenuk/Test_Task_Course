using Course_Test_Task.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;

namespace Course_Test_Task.Controllers
{
    public class CatalogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CatalogController(AppDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var catalogs = _context.Catalogs
                .Include(c => c.SubCatalogs)
                .ToList();

            return View(catalogs.ToList());
        }

        [HttpGet]
        public IActionResult AddCatalog()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCatalog(Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                _context.Catalogs.Add(catalog);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(catalog);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    var fileContent = await reader.ReadToEndAsync();

                    var dataModel = new Catalog
                    {
                        Name = fileName
                    };

                    _context.Catalogs.Add(dataModel);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }

            return View();
        }

    }
}
