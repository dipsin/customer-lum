using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomeInfo;
using System.IO;

namespace CustomeInfo.Controllers
{
    public class CustomerInfoController : Controller
    {
        private CustomerInfoAzureEntities db = new CustomerInfoAzureEntities();

        // GET: CustomerInfo
        public async Task<ActionResult> Index()
        {
            return View(await db.Customers.ToListAsync());
        }

        // GET: CustomerInfo/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: CustomerInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HttpPostedFileBase file,[Bind(Include = "CustomerName,CustomerAge,CustomerEmail,CustomerImage,imagePath")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var path = "";
                if(file!=null)
                {
                    if (file.ContentLength > 0)
                    {
                        if(Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".png" || Path.GetExtension(file.FileName).ToLower() == ".gif" || Path.GetExtension(file.FileName).ToLower() == ".jpeg")
                        {
                            path = Path.Combine(Server.MapPath("~/Content/Upload"), file.FileName);
                            file.SaveAs(path);

                            customer.CustomerImage = ImageToBinary(path);          
                                         
                        }
                    }

                }
                //if (customer.imagePath != null)
                //{
                //       //var uImagePath = @"C:\Dipti\CLASSES\MVC PROJECT\Image-Fan\" + customer.imagePath;
                //       var uImagePath = Path.Combine(Server.MapPath("~/Content/Upload"), customer.imagePath.ToString());
                    
                //    //mRegister.file.SaveAs(path);
                //    //@"C:\Dipti\CLASSES\MVC PROJECT\Image-Fan\User1.jpg";
                //    //var uImagePath = "C:\\Dipti\\CLASSES\\MVC PROJECT\\Image-Fan\\User1.jpg";
                //    customer.CustomerImage = ImageToBinary(uImagePath);
                // }

                db.Customers.Add(customer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }


            return View(customer);
        }

        public static byte[] ImageToBinary(string _path)
        {
            FileStream fS = new FileStream(_path, FileMode.Open, FileAccess.Read);
            
            byte[] b = new byte[fS.Length];
            fS.Read(b, 0, (int)fS.Length);
            fS.Close();
            return b;
        }

       

        // GET: CustomerInfo/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: CustomerInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CustomerId,CustomerName,CustomerAge,CustomerEmail,CustomerImage")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: CustomerInfo/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: CustomerInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Customer customer = await db.Customers.FindAsync(id);
            db.Customers.Remove(customer);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
