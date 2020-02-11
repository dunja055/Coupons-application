using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class RegistracijaController : Controller
    {
        public ActionResult Registracija()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registracija(Korisnik k, string password1)
        {

            k.Uloga = false;


            if(k.Password == password1)
            {
                KuponiEntities baza = new KuponiEntities();
                baza.Korisnik.Add(k);
                baza.SaveChanges();

            }
           
            return RedirectToAction("Login", "Login");
        }

        



       
    }
}