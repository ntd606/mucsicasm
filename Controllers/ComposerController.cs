using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mucsic.Data;
using mucsic.Interfaces;
using mucsic.Models;

namespace mucsic.Controllers
{
    public class ComposerController : Controller
    {
        private readonly AppDbContext _context;
        
        private readonly IPhotoService _singerPhotoService;

        public ComposerController(AppDbContext context, IPhotoService singerPhotoService)
        {
            _context = context;
            _singerPhotoService = singerPhotoService;
        }

        // GET: Composer
        public async Task<IActionResult> Index()
        {
            return View(await _context.Composers.ToListAsync());
        }

        // GET: Composer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var composer = await _context.Composers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (composer == null)
            {
                return NotFound();
            }

            return View(composer);
        }

        // GET: Composer/Create
        public async Task<IActionResult> Create()
        {
            var composer = await _context.Composers.ToListAsync();
            ViewBag.Composer = new SelectList(composer, "Id", "Name", "Biography", "ImageUrl");
            return View();
        }

        // POST: Composer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Composer(Composer obj)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", obj);
            }
            if (obj.ImageFile != null)
            {
                var result = await _singerPhotoService.AddPhotoAsync(obj.ImageFile);
                if (result.Error != null)
                {
                    ModelState.AddModelError("ImageFile", "Tải ảnh thất bại." + result.Error);
                    return View("Create", obj);
                }
                obj.ImageUrl = result.SecureUrl.AbsoluteUri;
            }
            _context.Add(obj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }

        // GET: Composer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var composer = await _context.Composers.FindAsync(id);
            if (composer == null)
            {
                return NotFound();
            }
            return View(composer);
        }

        // POST: Composer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Biography,ImageUrl")] Composer composer)
        {
            if (id != composer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(composer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComposerExists(composer.Id))
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
            return View(composer);
        }

        // GET: Composer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var composer = await _context.Composers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (composer == null)
            {
                return NotFound();
            }

            return View(composer);
        }

        // POST: Composer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var composer = await _context.Composers.FindAsync(id);
            if (composer != null)
            {
                _context.Composers.Remove(composer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComposerExists(int id)
        {
            return _context.Composers.Any(e => e.Id == id);
        }
    }
}
