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
    public class RegisterModel : PageModel
    {
        private readonly LoginService LoginMan;

        public RegisterModel(LoginService loginService)
        {
            this.LoginMan = loginService;
        }

        public class FormModel
        {
            [Required]
            [StringLength(maximumLength:100,MinimumLength = 5)]
            public string Username { set; get; }
            [Required]
            [StringLength(maximumLength:100,MinimumLength = 6)]
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
            await LoginMan.CreateUser(Form.Username, Form.Password);

           

            return Redirect("~/");
        }

    }
}