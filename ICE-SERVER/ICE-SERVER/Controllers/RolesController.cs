using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ICE_Server.DAL;
using ICE_Server.Models;
using Microsoft.AspNet.Identity;

namespace ICE_Server.Controllers
{
    public class RoleController : BaseController
    {
        public RoleController()
            : this(Startup.RoleManagerFactory())
        {
        }

        public RoleController(RoleManager<Role, int> roleManager)
        {
            RoleManager = roleManager;
        }

        public RoleManager<Role, int> RoleManager { get; private set; }


        // GET: api/Role
        public IQueryable<Role> GetRoles()
        {
            return RoleManager.Roles;
        }

        // GET: api/Role/5
        [ResponseType(typeof(Role))]
        public IHttpActionResult GetRole(int id)
        {
            var role = RoleManager.FindById(id);
            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // PUT: api/Role/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRole(Role model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id > 0)
            {
                return BadRequest();
            }

            // Check if the model name is empty
            if (model.Name == null || model.Name == "")
            {
                ModelState.AddModelError("Name", "Role name is required");
                return BadRequest(ModelState);
            }

            // Get the role with the provided id from the database
            var role = RoleManager.FindById(model.Id);

            // Update the role database object from the binding model and the existing object
            role.Name = model.Name;

            await RoleManager.UpdateAsync(role);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Role
        [ResponseType(typeof(Role))]
        public async Task<IHttpActionResult> PostRole(Role model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the model name is empty
            if (model.Name == null || model.Name == "")
            {
                ModelState.AddModelError("Name", "Role name is required");
                return BadRequest(ModelState);
            }

            // Get the role with the provided id from the database
            var roles = RoleManager.Roles.Where(r => r.Name.ToLower() == model.Name.ToLower());
            Role role = null;
            if (roles.Count() > 0)
            {
                role = roles.First();
            }

            // If is different null, so the name already exists
            if (role != null)
            {
                ModelState.AddModelError("Name", "Role name already exists");
                return BadRequest(ModelState);
            }

            // Create the role database object from the binding model and the existing object
            var dboRole = new Role
            {
                Name = model.Name
            };

            await RoleManager.CreateAsync(dboRole);

            return CreatedAtRoute("DefaultApi", new { id = dboRole.Id }, dboRole);
        }

        // DELETE: api/Role/5
        [ResponseType(typeof(Role))]
        public async Task<IHttpActionResult> DeleteRole(int id)
        {
            var role = RoleManager.FindById(id);
            if (role == null)
            {
                return NotFound();
            }
            await RoleManager.DeleteAsync(role);
            return Ok(role);
        }

        private bool RoleExists(int id)
        {
            return RoleManager.FindById(id) == null ? false : true;
        }
    }
}