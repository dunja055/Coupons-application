using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AplikacijaKuponi.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Korisnik ulKorisnik)
        {
            string username = ulKorisnik.Username;
            string password = ulKorisnik.Password;

            KuponiEntities baza = new KuponiEntities();
            List<Korisnik> lista = baza.Korisnik.ToList();

            bool flag = false;
            Korisnik nadjen = null;
            foreach (var l in lista)
            {
                if(ulKorisnik.Username == l.Username && ulKorisnik.Password == l.Password)
                {
                    flag = true;
                    nadjen = l;
                    break;
                }
            
               
            }
          
            if (flag == true)
            {
                Session["ulogovani"] = nadjen;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Error404", "Error404");
            }
           
            
        }
    }
}