using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Latihan2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Latihan2.Pages
{
    public class LoginModel : PageModel
    {
        private readonly LoginService LoginMan;

        public LoginModel(LoginService loginService)
        {
            this.LoginMan = loginService;
        }

        public class FormModel
        {
            [Required]
            public string Username { set; get; }

            [Required]
            public string Password { set; get; }

        }

        [BindProperty]
        public FormModel Form { set; get; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid == false)
            {
                return Page();
            }

            var login = await LoginMan.Login(Form.Username, Form.Password);

            if(login == null)
            {
                ModelState.AddModelError("Form.Password", "Username / Password Salah");
                return Page();
            }

            await HttpContext.Authentication.SignInAsync(LoginService.CookieAuthenticationScheme, login);
            
            return Redirect("~/");
        }

    }
}