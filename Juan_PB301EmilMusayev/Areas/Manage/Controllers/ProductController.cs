using Juan_PB301EmilMusayev.Data;
using Juan_PB301EmilMusayev.Extensions;
using Juan_PB301EmilMusayev.Models;
using Juan_PB301EmilMusayev.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Juan_PB301EmilMusayev.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        private readonly JuanDbContext _context;

        public ProductController(JuanDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page=1)
        {
            var query = _context.Products
                .AsNoTracking()
                .Include(p => p.ProductColors)
                .Include(p => p.Category)
                .Where(p => !p.IsDeleted);
            return View(PaginationVM<Product>.Create(query,page,3));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.categories = await _context.Categories
                .AsNoTracking()
                .Where(c => !c.IsDeleted && c.IsMainCategory)
                .ToListAsync();
            ViewBag.sizes = await _context.Sizes
                .AsNoTracking()
                .Where(s => !s.IsDeleted)
                .ToListAsync();
            ViewBag.colors = await _context.Colors
                .AsNoTracking()
                .Where(c => !c.IsDeleted)
                .ToListAsync();
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ViewBag.categories = await _context.Categories
                .AsNoTracking()
                .Where(c => !c.IsDeleted && c.IsMainCategory)
                .ToListAsync();
            ViewBag.sizes = await _context.Sizes
                .AsNoTracking()
                .Where(s => !s.IsDeleted)
                .ToListAsync();
            ViewBag.colors = await _context.Colors
                .AsNoTracking()
                .Where(c => !c.IsDeleted)
                .ToListAsync();
            if (!ModelState.IsValid) return View(product);
            Product newProduct = new();
            var mainPhoto = product.MainPhoto;
            var photos = product.Photos;
            if (mainPhoto is null || photos is null)
            {
                ModelState.AddModelError("", "Photos Required");
                return View(product);
            }
            if (!mainPhoto.IsImage())
            {
                ModelState.AddModelError("MainPhoto", "Invalid file format");
                return View(product);
            }
            if (mainPhoto.IsCorrectSize(100))
            {
                ModelState.AddModelError("MainPhoto", "File size is too large");
                return View(product);
            }
            newProduct.DisplayImage = await mainPhoto.SaveFile();
            List<ProductImage> list = new();
            foreach (var photo in photos)
            {
                if (!photo.IsImage())
                {
                    ModelState.AddModelError("Photos", "Invalid file format");
                    return View(product);
                }
                if (photo.IsCorrectSize(100))
                {
                    ModelState.AddModelError("Photos", "File size is too large");
                    return View(product);
                }
                ProductImage productImage = new();
                productImage.ProductId = newProduct.Id;
                productImage.CreateDate = DateTime.Now;
                productImage.Image = await photo.SaveFile();
                list.Add(productImage);
            }
            newProduct.Name = product.Name.Trim();
            newProduct.IsNewArrival = product.IsNewArrival;
            newProduct.Description = product.Description.Trim();
            newProduct.CategoryId = product.CategoryId;
            newProduct.Count = product.Count;
            newProduct.CreateDate = DateTime.Now;
            newProduct.DiscountPrice = product.DiscountPrice;
            newProduct.SalePrice = product.SalePrice;
            newProduct.ProductImages = list;
            newProduct.ProductReviews = product.ProductReviews;
            newProduct.ProductSizes = product.ProductSizes;
            newProduct.ProductColors = product.ProductColors;
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return View();
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var product = await _context.Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product is null) return NotFound();
            return View(product);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int? id, Product product)
        {
            ViewBag.categories = await _context.Categories
                .AsNoTracking()
                .Where(c => !c.IsDeleted && c.IsMainCategory)
                .ToListAsync();
            ViewBag.sizes = await _context.Sizes
                .AsNoTracking()
                .Where(s => !s.IsDeleted)
                .ToListAsync();
            ViewBag.colors = await _context.Colors
                .AsNoTracking()
                .Where(c => !c.IsDeleted)
                .ToListAsync();
            if (id is null) return BadRequest();
            var existProduct = await _context.Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (existProduct is null) return NotFound();
            if (!ModelState.IsValid) return View(product);
            if (!await _context.Categories.Where(c => !c.IsDeleted).AnyAsync(c => c.Id == product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Category Id is not correct");
                return View(product);
            }
            List<ProductImage> list = new();
            var mainFile = product.MainPhoto;
            var Files = product.Photos;
            if (mainFile != null)
            {
                if (!mainFile.IsImage())
                {
                    ModelState.AddModelError("MainPhoto", "Invalid file format");
                    return View(product);
                }
                if (mainFile.IsCorrectSize(100))
                {
                    ModelState.AddModelError("MainPhoto", "File size is too large");
                    return View(product);
                }
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "product", existProduct.DisplayImage);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                existProduct.DisplayImage = await mainFile.SaveFile();
            }
            if (Files != null)
            {
                foreach (var photo in Files)
                {
                    if (!photo.IsImage())
                    {
                        ModelState.AddModelError("Photos", "invalid file format");
                        return View(product);
                    }
                    if (photo.IsCorrectSize(100))
                    {
                        ModelState.AddModelError("MainPhoto", "File size is too large");
                        return View(product);
                    }
                    ProductImage productImage = new();
                    productImage.ProductId = existProduct.Id;
                    productImage.CreateDate = DateTime.Now;
                    productImage.Image = await photo.SaveFile();
                    list.Add(productImage);
                }
            }
            existProduct.Name = product.Name.Trim();
            existProduct.IsNewArrival = product.IsNewArrival;
            existProduct.Description = product.Description.Trim();
            existProduct.CategoryId = product.CategoryId;
            existProduct.Count = product.Count;
            existProduct.CreateDate = DateTime.Now;
            existProduct.DiscountPrice = product.DiscountPrice;
            existProduct.SalePrice = product.SalePrice;
            existProduct.ProductImages = list;
            existProduct.ProductReviews = product.ProductReviews;
            existProduct.ProductSizes = product.ProductSizes;
            existProduct.ProductColors = product.ProductColors;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null) return NotFound();
            product.IsDeleted = true;
            product.DeleteDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
