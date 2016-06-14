using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ICE_Server.DAL;
using ICE_Server.Models;
using ICE_Webserver.Authorization;
using System.Net.Http;
using ICE_Server.Models.Views.Authentication;

namespace ICE_Webserver.Controllers
{
    [ICEAuthorize]
    public class UsersController : BaseController
    {

        // GET: Users
        public async Task<ActionResult> Index()
        {
            return View((await HandleObjectFromRequest<List<User>> (HttpMethod.Get, "api/User", null)).Item);
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = (await HandleObjectFromRequest<User>(HttpMethod.Get, "api/User", (int)id)).Item;
            if (user == null || user.Id != (int) id || user.Email == string.Empty || user.UserName == string.Empty)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        [ICEAuthorize (Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            var request = await HandleObjectFromRequest<List<Role>>(HttpMethod.Get, "api/role", null);
            if (!request.IsSuccessStatusCode)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(request.Item, "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ICEAuthorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "UserName,Email,RoleId")] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var request = await HandleObjectFromRequest<User>(HttpMethod.Post, "api/user/Register", model);
                if (request.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    await DisplayModelStateErrors(request.Response);    
                }
            }
            
            return View(model);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var requestUser = await HandleObjectFromRequest<EditViewModel>(HttpMethod.Get, "api/User", (int)id);
            if (!requestUser.IsSuccessStatusCode)
            {
                return HttpNotFound();
            }
            EditViewModel user = requestUser.Item;
            if (user == null || user.Id != (int)id || user.Email == string.Empty || user.UserName == string.Empty)
            {
                return HttpNotFound();
            }
            var requestRoles = await HandleObjectFromRequest<List<Role>>(HttpMethod.Get, "api/role", null);
            if (!requestRoles.IsSuccessStatusCode)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(requestRoles.Item, "Id", "Name");

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,UserName,Email,RoleId,Role,OldPassword,NewPassword,ConfirmNewPassword")] EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var request = await HandleObjectFromRequest<User>(HttpMethod.Put, "api/user", model);
                if (request.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    await DisplayModelStateErrors(request.Response);
                }
            }

            var requestRole = await HandleObjectFromRequest<List<Role>>(HttpMethod.Get, "api/role", null);
            if (!requestRole.IsSuccessStatusCode)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(requestRole.Item, "Id", "Name");
            if (model.Role == null)
            {
                model.Role = requestRole.Item.Where(r => r.Id == model.RoleId).First();
            }
            return View(model);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var request = await HandleObjectFromRequest<User>(HttpMethod.Get, "api/user", (int) id);
            if (!request.IsSuccessStatusCode)
            {
                return HttpNotFound();
            }
            return View(request.Item);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var request = await HandleObjectFromRequest<User>(HttpMethod.Delete, "api/user", (int)id);
            if (!request.IsSuccessStatusCode)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }

        //GET: Users/Profile
        public async Task<ActionResult> MyProfile()
        {
            User user = null;
            if (((ICE_Server.Models.Views.Authentication.UserViewModel)Session["User"]) != null &&
            ((ICE_Server.Models.Views.Authentication.UserViewModel)Session["User"]).Role.Name == "User")
            {
                user = (await HandleObjectFromRequest<User>(HttpMethod.Get, "api/User", (int)((ICE_Server.Models.Views.Authentication.UserViewModel)Session["User"]).Id)).Item;

                if (user == null ||  user.Email == string.Empty || user.UserName == string.Empty)
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(user);
        }
    }
}
