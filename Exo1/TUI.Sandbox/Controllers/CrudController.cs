using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TUI.Data.Access.Source.Unit;
using TUI.Model.Shared.Source;

namespace TUI.Sandbox.Controllers
{
    public abstract class CrudController<T>:BasicController
        where T : class, IIdContainer
    {
        protected readonly IUnit<T> Unit;

        protected CrudController(IUnit<T> unit)
        {
            this.Unit = unit;
        }

        protected abstract void SetViewBagDependencies();
        protected abstract void SetViewBagDependencies(T item);

        public ActionResult Index()
        {
            using (var session = this.Unit.GetSession())
            {
                var items = session.GetRepository();
                return View(items.GetAll());
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.GetInvalidParameterNotification();
            }
            using (var session = this.Unit.GetSession())
            {
                var notNullId = id ?? -1;
                var element = session.GetRepository().Get(notNullId);
                if (element == null)
                {
                    return GetNotFound<T>(notNullId);
                }
                return View(element);
            }
        }

        [Authorize]
        public ActionResult Create()
        {
            this.SetViewBagDependencies();
            return View();
        }

        [Authorize]
        public virtual ActionResult Create(T item)
        {
            return this.Creating(item);
        }

        [Authorize]
        private ActionResult Creating(T item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var session = this.Unit.GetSession())
                    {
                        var repo = session.GetRepository();
                        repo.Add(item);
                        session.Complete();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception e)
                {
                    return GetProvokedErrorBy(e.Message);
                }
            }
            return this.GetProvokedErrorBy(GetModelStateErrors());
        }

        private String GetModelStateErrors()
        {
            var errors = this.ModelState.Values.SelectMany(m => m.Errors);
            return String.Join("", errors.Select(e => e.ErrorMessage));
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.GetInvalidParameterNotification();
            }
            var notNullId = id ?? -1;

            using (var session = this.Unit.GetSession())
            {
                T item = session.GetRepository().Get(notNullId);
                if (item == null)
                {
                    return GetNotFound<T>(notNullId);
                }

                this.SetViewBagDependencies(item);
                return View(item);
            }
        }

        [Authorize]
        public virtual ActionResult Edit(T item)
        {
            return Editing(item);
        }

        [Authorize]
        private ActionResult Editing(T item)
        {
            try
            {
                using (var session = this.Unit.GetSession())
                {
                    var repo = session.GetRepository();
                    if (ModelState.IsValid)
                    {
                        repo.SetModified(item);
                        session.Complete();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception e)
            {
                return GetProvokedErrorBy(e.Message);
            }

            return this.GetProvokedErrorBy(GetModelStateErrors());
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return this.GetInvalidParameterNotification();
            }

            var notNullId = id ?? -1;

            using (var container = this.Unit.GetSession())
            {
                T item = container.GetRepository().Get(notNullId);
                if (item == null)
                {
                    return GetNotFound<T>(notNullId);
                }
                return View(item);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                using (var session = this.Unit.GetSession())
                {
                    var repo = session.GetRepository();
                    T item = repo.Get(id);
                    repo.Remove(item);
                    session.Complete();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                return GetProvokedErrorBy(e.Message);
            }
        }
    }
}