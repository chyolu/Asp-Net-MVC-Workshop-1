using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using PagedList;

namespace WebApplication1.Controllers
{
    public class homeController : Controller
    {
        Database1Entities2 db = new Database1Entities2();
        // GET: home

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(BOOK_DATA table)
        {
            db.BOOK_DATA.Add(table);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        int pageSize = 5;
        public ActionResult Index(int page=1)
        {
            int cpage = page < 1 ? 1 : page;
            var c = db.Table.OrderBy(n => n.CREATE_DATE).ToList();
            var r = c.ToPagedList(cpage, pageSize);
            return View(r);
            //var c = db.Table.OrderBy(n => n.BOOK_CLASS_ID).ToList();
            //return View(c);
        }
        public ActionResult search(string searching)
        {
            var c = db.BOOK_DATA.Where(n => n.BOOK_NAME.Contains(searching) || searching == null).ToList();
            return View(c);
        }
        public ActionResult Delete(int aid)
        {
            var c= db.Table.Where(n => n.BOOK_CLASS_ID==aid).FirstOrDefault();
            db.Table.Remove(c);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult Edit(int aid)
        {
            var c = db.Table.Where(n => n.BOOK_CLASS_ID == aid).FirstOrDefault();
            return View(c);
        }
        [HttpPost]
        public ActionResult Edit(Table table)
        {
            db.Table.Add(table);
            int aid = table.BOOK_CLASS_ID;
            var c = db.Table.Where(n => n.BOOK_CLASS_ID == aid).FirstOrDefault();
            c.BOOK_CLASS_NAME = table.BOOK_CLASS_NAME;
            c.CREATE_DATE = table.CREATE_DATE;
            c.CREATE_USER = table.CREATE_USER;
            c.MODIFY_DATE = table.MODIFY_DATE;
            c.MODIFY_USER = table.MODIFY_USER;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}