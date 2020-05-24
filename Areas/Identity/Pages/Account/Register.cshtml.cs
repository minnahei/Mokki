using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Mokki.Data;
using Mokki.Models;

namespace Mokki.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
             ApplicationDbContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email*")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña*")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirma contraseña*")]
            [Compare("Password", ErrorMessage = "La contraseña y la confirmación no coinciden.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [MaxLength(20)]
            [Display(Name = "Nombre*")]
            public string Nombre { get; set; }

            [MaxLength(50)]
            public string Apellidos { get; set; }
            public string Provincia { get; set; }
            public string Telefono { get; set; }

            [Required]
            public string TipoUser { get; set; }
            public string Pueblo { get; set; }
            public string Ciudad { get; set; }
            public string Foto { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            AppUser user = new AppUser();
            if (ModelState.IsValid)
            {
                //var user = new AppUser { UserName = Input.Email, Email = Input.Email };

                user = new AppUser
                {

                    UserName = Input.Email,
                    Email = Input.Email,
                    Nombre = Input.Nombre,
                    Apellidos = Input.Apellidos,
                    Provincia = Input.Provincia,
                    Telefono = Input.Telefono,
                    Foto = Input.Foto

                };

            }


            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                if (Input.TipoUser == "anfitrion")
                {
                    Anfitrion anfitrion = new Anfitrion
                    {
                        User = user,
                        Pueblo = Input.Pueblo

                    };
                    _context.Add(anfitrion);
                    await _context.SaveChangesAsync();
                    await _userManager.AddToRoleAsync(user, "Anfitrion");

                }
                else
                {
                    Huesped huesped = new Huesped
                    {
                        User = user,
                        Ciudad = Input.Ciudad

                    };
                    _context.Add(huesped);
                    await _context.SaveChangesAsync();
                    await _userManager.AddToRoleAsync(user, "Huesped");

                }

                _logger.LogInformation("User created a new account with password.");

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { userId = user.Id, code = code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl);

                
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
