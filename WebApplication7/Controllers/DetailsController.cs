using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Dbopeartions;

namespace WebApplication7.Controllers
{
    public class DetailsController : Controller
    {
        EmployeeEntities db = new EmployeeEntities();

        // GET: Details
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        { 
                List<EmpFileDetails> empList = db.EmpFileDetails.ToList<EmpFileDetails>();
                return Json(new { data = empList },
                JsonRequestBehavior.AllowGet);   
        }
        [HttpGet]
        public ActionResult StoreOrEdit()
        {

            return View(new EmpFileDetails());
        }
        [HttpPost]
        public ActionResult StoreOrEdit(HttpPostedFileBase file, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, EmpFileDetails empdata)
        {
            Directory.CreateDirectory(Server.MapPath("~/Files"));
            Directory.CreateDirectory(Server.MapPath("~/Filesen"));
            Directory.CreateDirectory(Server.MapPath("~/Filesdec"));
            string path1 = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(file.FileName));
            string path2 = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(file1.FileName));
            string path3 = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(file2.FileName));
            string path4 = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(file3.FileName));
            file.SaveAs(path1);
            file.SaveAs(path2);
            file.SaveAs(path3);
            file.SaveAs(path4);
            string enpath1 = path1.Replace("Files", "Filesen");
            string enpath2 = path2.Replace("Files", "Filesen");
            string enpath3 = path3.Replace("Files", "Filesen");
            string enpath4 = path4.Replace("Files", "Filesen");
            string filename1 = Path.GetFileName(file.FileName);
            string filename2 = Path.GetFileName(file1.FileName);
            string filename3 = Path.GetFileName(file2.FileName);
            string filename4 = Path.GetFileName(file3.FileName);
            EncryptFile(path1, enpath1);
            EncryptFile(path2, enpath2);
            EncryptFile(path3, enpath3);
            EncryptFile(path4, enpath4);

            System.IO.File.Delete(path1);
            System.IO.File.Delete(path2);
            System.IO.File.Delete(path3);
            System.IO.File.Delete(path4);

            EmpFileDetails emp = new EmpFileDetails();
            emp.EmpId = empdata.EmpId;
            emp.FilepathEmirates = enpath1;
            emp.FilepathPassport = enpath2;
            emp.FilepathCertificate = enpath3; ;
            emp.FilepathResume = enpath4;
            emp.emirates = filename1;
            emp.passport = filename2;
            emp.certificate = filename3;
            emp.resume = filename4;

            db.EmpFileDetails.Add(emp);
            db.SaveChanges();
            ModelState.Clear();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult StoreOrEdit1(int? id)
        {
            

                return View(db.EmpFileDetails.Where(x => x.EmpId == id).FirstOrDefault<EmpFileDetails>());

        }

        [HttpPost]
        public ActionResult StoreOrEdit1(HttpPostedFileBase file, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, EmpFileDetails empdata)
        {
            Directory.CreateDirectory(Server.MapPath("~/Files"));
            Directory.CreateDirectory(Server.MapPath("~/Filesen"));
            Directory.CreateDirectory(Server.MapPath("~/Filesdec"));
            string path1 = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(file.FileName));
            string path2 = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(file1.FileName));
            string path3 = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(file2.FileName));
            string path4 = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(file3.FileName));
            file.SaveAs(path1);
            file.SaveAs(path2);
            file.SaveAs(path3);
            file.SaveAs(path4);
            string enpath1 = path1.Replace("Files", "Filesen");
            string enpath2 = path2.Replace("Files", "Filesen");
            string enpath3 = path3.Replace("Files", "Filesen");
            string enpath4 = path4.Replace("Files", "Filesen");
            string filename1 = Path.GetFileName(file.FileName);
            string filename2 = Path.GetFileName(file1.FileName);
            string filename3 = Path.GetFileName(file2.FileName);
            string filename4 = Path.GetFileName(file3.FileName);
            EncryptFile(path1, enpath1);
            EncryptFile(path2, enpath2);
            EncryptFile(path3, enpath3);
            EncryptFile(path4, enpath4);

            System.IO.File.Delete(path1);
            System.IO.File.Delete(path2);
            System.IO.File.Delete(path3);
            System.IO.File.Delete(path4);
            var emp = db.EmpFileDetails.Where(x => x.EmpId == empdata.EmpId).FirstOrDefault();  
            emp.FilepathEmirates = enpath1;
            emp.FilepathPassport = enpath2;
            emp.FilepathCertificate = enpath3; ;
            emp.FilepathResume = enpath4;
            emp.emirates = filename1;
            emp.passport = filename2;
            emp.certificate = filename3;
            emp.resume = filename4;
            db.SaveChanges();
            ModelState.Clear();

            return RedirectToAction("Index");
        }

       
        public FileResult Downloadfileemirates(int? Id)
        {
            var obj = db.EmpFileDetails.Where(x => x.EmpId == Id).First();
            string path1 = obj.FilepathEmirates.ToString();
            string decpath1 = path1.Replace("Filesen", "Filesdec");
            DecryptFile(path1, decpath1);
            return File(decpath1, "application", decpath1);
        }
       
        public FileResult Downloadfilepassport(int? Id)
        {
            var obj = db.EmpFileDetails.Where(x => x.EmpId == Id).First();
            string path1 = obj.FilepathPassport.ToString();
            string decpath1 = path1.Replace("Filesen", "Filesdec");
            DecryptFile(path1, decpath1);
            return File(decpath1, "application", decpath1);
        }

       
        public FileResult Downloadfilecertificate(int? Id)
        {
            var obj = db.EmpFileDetails.Where(x => x.EmpId == Id).First();
            string path1 = obj.FilepathCertificate.ToString();
            string decpath1 = path1.Replace("Filesen", "Filesdec");
            DecryptFile(path1, decpath1);
            return File(decpath1, "application", decpath1);
        }
        
        public FileResult Downloadfileresume(int? Id)
        {
            var obj = db.EmpFileDetails.Where(x => x.EmpId == Id).First();
            string path1 = obj.FilepathResume.ToString();
            string decpath1 = path1.Replace("Filesen", "Filesdec");
            DecryptFile(path1, decpath1);
            return File(decpath1, "application", decpath1);
        }


        private void EncryptFile(string inputFile, string outputFile)
        {

            try
            {
                string password = @"myKey123"; 
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);


                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch
            {
              
            }
        }




        private void DecryptFile(string inputFile, string outputFile)
        {

            {
                string password = @"myKey123"; 

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                fsOut.Close();
                cs.Close();
                fsCrypt.Close();

            }
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {        
                EmpFileDetails emp = db.EmpFileDetails.Where(x => x.EmpId == id).FirstOrDefault<EmpFileDetails>();
                db.EmpFileDetails.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });        
        }

    }

}
