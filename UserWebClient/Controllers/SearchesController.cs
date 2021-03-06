﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserWebClient.Models;

namespace UserWebClient.Controllers
{
    public class SearchesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Searches
        public async Task<ActionResult> Index()
        {
            return View(await db.Searches.ToListAsync());
        }

        // GET: Searches/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Search search = await db.Searches.FindAsync(id);
            if (search == null)
            {
                return HttpNotFound();
            }
            return View(search);
        }

        // GET: Searches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Searches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "name,address,zipcode,phoneNo")] Search search)
        {
            if (ModelState.IsValid)
            {
                db.Searches.Add(search);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(search);
        }

        // GET: Searches/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Search search = await db.Searches.FindAsync(id);
            if (search == null)
            {
                return HttpNotFound();
            }
            return View(search);
        }

        // POST: Searches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "name,address,zipcode,phoneNo")] Search search)
        {
            if (ModelState.IsValid)
            {
                db.Entry(search).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(search);
        }

        // GET: Searches/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Search search = await db.Searches.FindAsync(id);
            if (search == null)
            {
                return HttpNotFound();
            }
            return View(search);
        }

        // POST: Searches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Search search = await db.Searches.FindAsync(id);
            db.Searches.Remove(search);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
