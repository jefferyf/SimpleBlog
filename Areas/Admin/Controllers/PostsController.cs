using NHibernate.Linq;
using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTabAttribute("posts")]
    public class PostsController : Controller
    {
        private const int PostsPerPage = 2;
        private const int PageRange = 3;

        public ActionResult Index(int page = 1)
        {
            var totalPostCount = Database.Session.Query<Post>().Count();
            var currentPostPage = Database.Session.Query<Post>()
                .OrderByDescending(x => x.CreatedAt)
                .Skip((page - 1) * PostsPerPage)
                .Take(PostsPerPage)
                .ToList();

            return View(new PostsIndex {
                Posts = new PagedData<Post>(currentPostPage, totalPostCount, page, PostsPerPage, PageRange)
            });
        }

        public ActionResult New()
        {
            return View("Form", new PostsForm
            {
                IsNew = true
            });
        }

        public ActionResult Edit(int id)
        {
            var post = Database.Session.Load<Post>(id);
            return View("Form", new PostsForm
            {
                IsNew = false,
                PostId = id,
                Content = post.Content,
                Title = post.Title,
                Slug = post.Slug
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Form(PostsForm form)
        {
            form.IsNew = form.PostId == 0;

            if (!ModelState.IsValid)
                return View(form);

            Post post;
            if(form.IsNew)
            {
                post = new Post
                {
                    CreatedAt = DateTime.UtcNow,
                    User = Auth.User
                };
            } else
            {
                post = Database.Session.Load<Post>(form.PostId);
                if (post == null)
                    return HttpNotFound();

                post.UpdatedAt = DateTime.UtcNow;
            }

            post.Title = form.Title;
            post.Slug = form.Slug;
            post.Content = form.Content;

            Database.Session.SaveOrUpdate(post);

            return RedirectToAction("Index");
        }
    }
}