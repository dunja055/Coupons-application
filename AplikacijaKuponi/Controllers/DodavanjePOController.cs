using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplikacijaKuponi.Controllers
{
    public class DodavanjePOController : Controller
    {
        public ActionResult DodavanjePO()
        {
            return View();
        }

        [HttpPost]

        public ActionResult DodavanjePO(PoslovniObjekti po, HttpPostedFileBase file)
        {
            
            if (file != null)
            {
                string slika = System.IO.Path.GetFileName(file.FileName);
                string putanja = System.IO.Path.Combine(Server.MapPath("~/Content/assets/Slike poslovni objekti/"), slika);
                file.SaveAs(putanja);
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] niz = ms.GetBuffer();

                    KuponiEntities baza = new KuponiEntities();
                    baza.PoslovniObjekti.Add(po);
                    SlikePoslovnihObjekata slikePO = new SlikePoslovnihObjekata();
                    var novaPutanja = MakeRelative(putanja, @"C:\Users\Dunja\Desktop\Kuponi\AplikacijaKuponi\AplikacijaKuponi\");
                    novaPutanja = "/" + novaPutanja;
                    slikePO.Putanja = novaPutanja;
                    slikePO.PoslovniObjekti = po;
                    slikePO.IDPoslovniObjekti = po.IDPoslovniObjekti;
                    baza.SlikePoslovnihObjekata.Add(slikePO);
                    baza.SaveChanges();
                }

            }
            return RedirectToAction("PoslovniObjekti", "PoslovniObjekti");
        }

        public string MakeRelative(string filePath, string referencePath)
        {
            var fileUri = new Uri(filePath);
            var referenceUri = new Uri(referencePath);
            return referenceUri.MakeRelativeUri(fileUri).ToString();
        }
        }

    }
