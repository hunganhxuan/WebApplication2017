using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;
using WebApplication7.ViewModels;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace WebApplication7.Controllers
{
    [Authorize(Roles = "admin")]
    public class ArticlesController : Controller
    {
        public readonly ApplicationDbContext _dbContext;

        public ArticlesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Articles
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            var viewModel = new ArticlesViewModel
            {
                Categories = _dbContext.Categories.ToList()
            };
            return View(viewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticlesViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }

            var article = new Article
            {
                DateTime = DateTime.Now,
                CategoryId = viewModel.CategoryId,
                Header = viewModel.Header,
                Content = viewModel.Content,
                Image = viewModel.Image
            };

            _dbContext.Articles.Add(article);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = _dbContext.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        public ActionResult Manage()
        {
            return View(_dbContext.Articles.ToList());
        }

        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article article = _dbContext.Articles.Find(id);

            var viewModel = new ArticlesViewModel
            {
                Categories = _dbContext.Categories.ToList(),
                Header = article.Header,
                Content = article.Content,
                DateTime = article.DateTime,
                CategoryId = article.CategoryId,
                Image = article.Image,
                Id = article.Id
            };

            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticlesViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }

            var article = _dbContext.Articles.Single(c => c.Id == viewModel.Id);

            article.DateTime = DateTime.Now;
            article.CategoryId = viewModel.CategoryId;
            article.Header = viewModel.Header;
            article.Content = viewModel.Content;
            article.Image = viewModel.Image;

            _dbContext.SaveChanges();

            return RedirectToAction("Manage", "Articles");
            /*if (ModelState.IsValid)
            {
                var article = new Article
                {
                    DateTime = DateTime.Now,
                    CategoryId = viewModel.CategoryId,
                    Header = viewModel.Header,
                    Content = viewModel.Content,
                    Image = viewModel.Image
                };

                _dbContext.Entry(article).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("News");
            }
            return View(viewModel);*/
        }

        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = _dbContext.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Article article = _dbContext.Articles.Find(id);
            _dbContext.Articles.Remove(article);
            _dbContext.SaveChanges();
            return RedirectToAction("News");
        }

        public ActionResult News()
        {
            ViewBag.Message = "Your application news page.";

            var upcomingNews = _dbContext.Articles
                .Include(c => c.Category)
                .Where(c => c.CategoryId == 1);

            return View(upcomingNews);
        }

        public ActionResult Sport()
        {
            ViewBag.Message = "Your sport page.";

            var upcomingNews = _dbContext.Articles
                .Include(c => c.Category)
                .Where(c => c.CategoryId == 2);

            return View(upcomingNews);
        }

        public ActionResult Weather()
        {
            ViewBag.Message = "Your application weather page.";

            var upcomingNews = _dbContext.Articles
                .Include(c => c.Category)
                .Where(c => c.CategoryId == 3);

            return View(upcomingNews);
        }

        public ActionResult Music()
        {
            ViewBag.Message = "Your application music page.";

            var upcomingNews = _dbContext.Articles
                .Include(c => c.Category)
                .Where(c => c.CategoryId == 4);

            return View(upcomingNews);
        }

        public ActionResult Search(string searchString)
        {
            ViewBag.Message = "Your application search page.";
            
            var listModel = new ArticlesController();
            var model = listModel.GetArticle(searchString);

            return View(model);
        }

        public IEnumerable<Article> GetArticle(string searchString)
        {
            IQueryable<Article> articles = _dbContext.Articles;
            if (searchString != null)
            {
                articles = articles.Where(p => p.Header.Contains(searchString));
            }
            return articles;
        }
    }
}