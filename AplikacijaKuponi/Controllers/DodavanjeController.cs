using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class DodavanjeController : Controller
    {
        public ActionResult Dodavanje()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Dodavanje(Ponuda p, HttpPostedFileBase file,string kategorija, string pObjekti)
        {

            if (file != null)
            {
                string slika = System.IO.Path.GetFileName(file.FileName);
                string putanja = System.IO.Path.Combine(Server.MapPath("~/Content/assets/Slike ponuda/"), slika);
                file.SaveAs(putanja);
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] niz = ms.GetBuffer();
                    KuponiEntities baza = new KuponiEntities();
                    Kategorija kat = baza.Kategorija.FirstOrDefault(x => x.IDKategorija.ToString() == kategorija);
                    p.Kategorija = kat;
                    p.IDKategorija = kat.IDKategorija;
                    baza.Ponuda.Add(p);
                    SlikePonude slikePonuda = new SlikePonude();
                    var novaPutanja = MakeRelative(putanja, @"C:\Users\Dunja\Desktop\Kuponi\AplikacijaKuponi\AplikacijaKuponi\");
                    novaPutanja = "/" + novaPutanja;
                    slikePonuda.Putanja = novaPutanja;
                    slikePonuda.Ponuda = p;
                    slikePonuda.IDPonuda = p.IDPonuda;
                    baza.SlikePonude.Add(slikePonuda);
                    baza.SaveChanges();

                    List<Kategorija> kategorije = baza.Kategorija.ToList();
                    Kategorija nadjen = null;

                    foreach(var k in kategorije)
                    {
                        if (k.IDKategorija == p.IDKategorija)
                        {
                            nadjen = k;
                            break;

                        }
                    }
                    PoslovniObjekti posl = baza.PoslovniObjekti.First(x => x.IDPoslovniObjekti.ToString() == pObjekti);
                    PonudaiPoslovniObjekti ppo = new PonudaiPoslovniObjekti();
                    ppo.IDPonudaiPoslovniObjekti = posl.IDPoslovniObjekti;
                    ppo.IDPonuda = p.IDPonuda;
                    baza.PonudaiPoslovniObjekti.Add(ppo);
                    nadjen.Kolicina = nadjen.Kolicina +1;
                    baza.SaveChanges();
                }

            }
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