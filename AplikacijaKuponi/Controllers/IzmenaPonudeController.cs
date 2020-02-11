using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class IzmenaPonudeController : Controller
    {
        public ActionResult IzmenaPonude(string id)
        {
            KuponiEntities baza = new KuponiEntities();
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
            Session["izmenaPonuda"] = nadjen;

            return View();
        }
        [HttpPost]
        public ActionResult IzmenaPonudeBaza(Ponuda p,string kat, HttpPostedFileBase file)
        {
            Ponuda pIzmena = Session["izmenaPonuda"] as Ponuda;
            KuponiEntities baza = new KuponiEntities();
            List<Ponuda> ponude = baza.Ponuda.ToList();

            Ponuda nadjen = ponude.SingleOrDefault(a => a.IDPonuda == pIzmena.IDPonuda);
            if (file != null)
            {
                string slika = System.IO.Path.GetFileName(file.FileName);
                string putanja = System.IO.Path.Combine(Server.MapPath("~/Content/assets/Slike ponuda/"), slika);
                file.SaveAs(putanja);
                SlikePonude sp = baza.SlikePonude.FirstOrDefault(x => x.IDPonuda == nadjen.IDPonuda);
                baza.SlikePonude.Remove(sp);
                baza.SaveChanges();
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] niz = ms.GetBuffer();
                    SlikePonude slikePonuda = new SlikePonude();
                    var novaPutanja = MakeRelative(putanja, @"C:\Users\Dunja\Desktop\Kuponi\AplikacijaKuponi\AplikacijaKuponi\");
                    novaPutanja = "/" + novaPutanja;
                    slikePonuda.Putanja = novaPutanja;
                    slikePonuda.Ponuda = nadjen;
                    slikePonuda.IDPonuda = nadjen.IDPonuda;
                    baza.SlikePonude.Add(slikePonuda);
                    baza.SaveChanges();
                
                }

            }
            nadjen.NazivPonude = p.NazivPonude;
            nadjen.OpisPonude = p.OpisPonude;
            nadjen.Datum = p.Datum;
            nadjen.Kolicina = p.Kolicina;
            nadjen.Cena = p.Cena;
            nadjen.IDKategorija = Int32.Parse(kat);
            nadjen.Kategorija = baza.Kategorija.FirstOrDefault(x => x.IDKategorija.ToString() == kat);
            baza.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public string MakeRelative(string filePath, string referencePath)
        {
            var fileUri = new Uri(filePath);
            var referenceUri = new Uri(referencePath);
            return referenceUri.MakeRelativeUri(fileUri).ToString();
        }
    }
}