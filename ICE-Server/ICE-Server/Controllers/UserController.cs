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
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ICE_Server.Models.Views;
using ICE_Server.Models.Views.Authentication;
using System.Web;

namespace ICE_Server.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {

        public CustomClaimsPrincipal CurrentUser
        {
            get { return new CustomClaimsPrincipal((ClaimsPrincipal)ControllerContext.RequestContext.Principal); }
        }


        public UserController()
            : this(Startup.UserManagerFactory(), Startup.RoleManagerFactory(), Startup.OAuthOptions.AccessTokenFormat)
        {
        }

        public UserController(UserManager<User, int> userManager, RoleManager<Role, int> roleManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public UserManager<User, int> UserManager { get; private set; }
        public RoleManager<Role, int> RoleManager { get; private set; }
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        private ICEContext db = new ICEContext();
        private const string LocalLoginProvider = "Local";


        [Route("Login")]
        [AllowAnonymous]
        [ResponseType(typeof(OAuthResponse))]
        public async Task<IHttpActionResult> Login(LoginViewModel model)
        {
            // Check if a user can be found with the specified credentials
            //var dboUser = await UserManager.FindAsync(user.UserName, user.Password);
            var user = await UserManager.FindByEmailAsync(model.Email);

            var dboUser = await UserManager.FindAsync(user.UserName, model.Password);

            if (dboUser != null)
            {
                // Create a claims identity using the username and id
                var identity = await dboUser.GenerateUserIdentityAsync(UserManager);

                // Create an access token using a ticket
                var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
                ticket.Properties.IssuedUtc = DateTime.UtcNow;
                ticket.Properties.ExpiresUtc = DateTime.UtcNow.Add(TimeSpan.FromMinutes(30));

                //var accessToken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);
                var accessToken = AccessTokenFormat.Protect(ticket);

                return Ok(new OAuthResponse
                {
                    AccessToken = accessToken,
                    Expires = (DateTimeOffset)ticket.Properties.ExpiresUtc,
                    ExpiresIn = ticket.Properties.ExpiresUtc.Value.Ticks - ticket.Properties.IssuedUtc.Value.Ticks,
                    Issued = (DateTimeOffset)ticket.Properties.IssuedUtc,
                    User = dboUser
                });
            }
            ModelState.AddModelError("Password", "Incorrect login credentials, please try again.");
            //if (dboUser == null)
            //{
            //    // ModelState.AddModelError("Password", Resources.Global.InvalidPassword);
            //    ModelState.AddModelError("Password", "Invalid password");
            //}
            //else
            //{
            //    ModelState.AddModelError("Password", "Email not confirmed");
            //}
            return BadRequest(ModelState);
        }


        // POST api/User/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Create a user database object from the binding model
            User user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                RoleId = 2, // Registered user by default
                Ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]
            };

            // Check if the username already exists
            var userNameExists = await UserManager.FindByNameAsync(model.UserName);

            if (userNameExists != null)
            {
                //ModelState.AddModelError("UserName", Resources.Global.UserNameAlreadyExists);
                ModelState.AddModelError("UserName", "User name already exists");
                return BadRequest(ModelState);
            }

            // Check if the email already exists
            var emailExists = await UserManager.FindByEmailAsync(model.Email);

            if (emailExists != null)
            {
                ModelState.AddModelError("Email", "Email already is taken");
                return BadRequest(ModelState);
            }

            // Add the user to the database
            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            IHttpActionResult errorResult = GetErrorResult(result);

            // If an error occurred, let the user know about it
            if (errorResult != null)
            {
                return errorResult;
            }

            // Add the user's role to the database
            result = await UserManager.AddToRoleAsync(user.Id, "User");
            errorResult = GetErrorResult(result);

            // If an error occurred, let the user know about it
            if (errorResult != null)
            {
                return errorResult;
            }

            ///// Store in the database the request to change the password
            //string baseUrl = "http://localhost:9797/User/ConfirmEmail";
            //DateTime validity = DateTime.UtcNow.AddDays(7);
            //string urlConfirmation = await saveForgotPassword(user.Id, validity, baseUrl);

            ////Send the email to the user
            //string from = "justactivities@outlook.com";
            //string fromPass = "Justpass12";
            //string to = model.Email;
            //string subject = "[JustActivities] Confirm your email (7 days from now)";
            //string body = "Hi :),<br><br>check the following link to confirm your email: " + urlConfirmation + "<br><br>Thank you,<br>JustActivities, always with you.";

            //try
            //{
            //    sendEmail(from, fromPass, to, subject, body);
            //}
            //catch (Exception)
            //{

            //}

            return Ok(user);
        }




        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }






        // Automatically created
        // GET: api/User
        public IQueryable<User> GetUsers()
        {
            return UserManager.Users;
        }

        // GET: api/User/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/User/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/User
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}