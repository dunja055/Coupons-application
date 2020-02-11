using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class RezervacijeAdministratorController : Controller
    {
        public ActionResult RezervacijeAdministrator()
        {
            KuponiEntities baza = new KuponiEntities();
            List<Rezervacija> rezervacije = baza.Rezervacija.ToList();
            List<Rezervacija> cekanjeRez = new List<Rezervacija>();

            foreach (Rezervacija rez in rezervacije)
            {

                if (rez.Uplaceno == false)
                {
                    cekanjeRez.Add(rez);
                }

            }
            Session["rezervacijeCekanje"] = cekanjeRez;
            return View();
        }

        public ActionResult OdobravanjeRezervacija(string odobreno)
        {
            KuponiEntities baza = new KuponiEntities();
            List<Rezervacija> neodobrenaRez = Session["rezervacijeCekanje"] as List<Rezervacija>;


            foreach (Rezervacija r in neodobrenaRez)
            {
                if (r.IDRezervacija == Int32.Parse(odobreno))
                {
                    baza.Rezervacija.FirstOrDefault(x => x.IDRezervacija == r.IDRezervacija).Uplaceno = true;
                    break;
                }
            }
            baza.SaveChanges();
            return RedirectToAction("RezervacijeAdministrator", "RezervacijeAdministrator");
        }


    }
}