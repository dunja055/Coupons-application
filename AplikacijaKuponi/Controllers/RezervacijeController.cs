using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class RezervacijeController : Controller
    {
        public ActionResult Rezervacije(string id = "")
        {
            KuponiEntities baza = new KuponiEntities();
            Korisnik ulogovani = Session["ulogovani"] as Korisnik;
            List<Rezervacija> rezervacije = rezervacije = new List<Rezervacija>();
            List<Rezervacija> rezervacijeKupljene = new List<Rezervacija>();
            if (string.IsNullOrEmpty(id))
            {
                foreach (Rezervacija rez in baza.Rezervacija.ToList())
                {
                    if (rez.IDKorisnik == ulogovani.IDKorisnik  && rez.Uplaceno == true)
                    {
                        rezervacijeKupljene.Add(rez);
                    }
                    if (rez.IDKorisnik == ulogovani.IDKorisnik && rez.Uplaceno == false)
                    {
                        rezervacije.Add(rez);
                    }

                }
                Session["korisnikoveRezervacije"] = rezervacije;
                Session["rezervacijeKupljene"] = rezervacijeKupljene;
            }
            else
            {
                List<Ponuda> ponude = baza.Ponuda.ToList();
                Ponuda nadjen = null;

                foreach (var p in ponude)
                {
                    if (p.IDPonuda == Int32.Parse(id))
                    {
                        nadjen = p;
                        break;
                    }

                }
                Session["rezPonuda"] = nadjen;
                Rezervacija rezervacija = new Rezervacija();
                rezervacija.Cena = nadjen.Cena;
                rezervacija.Datum = DateTime.Now;
                rezervacija.Korisnik = (baza.Korisnik.FirstOrDefault(x => x.IDKorisnik == ulogovani.IDKorisnik));
                rezervacija.PonudaiPoslovniObjekti = nadjen.PonudaiPoslovniObjekti.First();
                rezervacija.Uplaceno = false;
                baza.Rezervacija.Add(rezervacija);
                baza.SaveChanges();
                foreach (Rezervacija rez in baza.Rezervacija.ToList())
                {
                    if (rez.IDKorisnik == ulogovani.IDKorisnik && rez.Uplaceno == true)
                    {
                        rezervacijeKupljene.Add(rez);
                    }
                    if (rez.IDKorisnik == ulogovani.IDKorisnik && rez.Uplaceno == false)
                    {
                        rezervacije.Add(rez);
                    }

                }
                Session["korisnikoveRezervacije"] = rezervacije;
                Session["rezervacijeKupljene"] = rezervacijeKupljene;
            }
            return View();
        }

    }
}