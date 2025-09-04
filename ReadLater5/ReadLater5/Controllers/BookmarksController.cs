using Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ReadLater5.Controllers
{
    [Authorize(AuthenticationSchemes = "Auth0" + "," + "Identity.Application")]
    public class BookmarksController : Controller
    {
        private readonly IBookmarksService _bookmarksService;
        private readonly ICategoryService _categoryService;
        public BookmarksController(IBookmarksService bookmarksService, ICategoryService categoryService)
        {
            _bookmarksService = bookmarksService;
            _categoryService = categoryService;
        }
        // GET: Bookmarks
        public async Task<IActionResult> Index()
        {
            List<Bookmark> model = await _bookmarksService.GetBookmarks();
            return View(model);
        }

        // GET: Bookmark/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark category = await _bookmarksService.GetBookmark((int)id);
            if (category == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(category);

        }

        // GET: Bookmarks/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(
                await _categoryService.GetCategories(), "ID", "Name");

            var model = new Bookmark();
            model.Category = new Category();
            return View(model);
        }

        // POST: Bookmarks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                await _bookmarksService.CreateBookmark(bookmark);
                return RedirectToAction("Index");
            }

            return View(bookmark);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = await _bookmarksService.GetBookmark((int)id);
            if (bookmark == null)
            {
               
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            ViewData["CategoryList"] = new SelectList(
                           await _categoryService.GetCategories(), "ID", "Name", bookmark.Category);
            return View(bookmark);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                var existingBookmark = await _bookmarksService.GetBookmark(bookmark.ID);
                if (existingBookmark == null)
                {
                    return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
                }
                existingBookmark.ShortDescription = bookmark.ShortDescription;
                existingBookmark.URL = bookmark.URL;
                existingBookmark.CategoryId = bookmark.CategoryId;

                await _bookmarksService.UpdateBookmark(existingBookmark);
                return RedirectToAction("Index");
            }

            ViewData["CategoryList"] = new SelectList(
            await _categoryService.GetCategories(), "ID", "Name", bookmark.Category);   

            return View(bookmark);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark category = await _bookmarksService.GetBookmark((int)id);
            if (category == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Bookmark bookmark = await _bookmarksService.GetBookmark(id);
            await _bookmarksService.DeleteBookmark(bookmark);
            return RedirectToAction("Index");
        }
    }
}
