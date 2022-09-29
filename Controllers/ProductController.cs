using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADO_Example_PRO.DAL;
using ADO_Example_PRO.Models;

namespace ADO_Example_PRO.Controllers
{
    public class ProductController : Controller
    {
        Product_DAL _productDAL = new Product_DAL();
        // GET: Product
        public ActionResult Index()
        {
            var productList = _productDAL.GetAllProducts();
            
            if(productList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently Products not Available in the Database";
            }
            
            return View(productList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var product = _productDAL.GetProductById(id).FirstOrDefault();
                if (product == null)
                {
                    TempData["InfoMessage"] = "Currently Products not Available in the Database";
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _productDAL.InsertProduct(product);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Product Details saved Successfully";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to save the Product details";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
    }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var products = _productDAL.GetProductById(id).FirstOrDefault();
            if (products == null)
            {
                TempData["InfoMessage"] = "Product Not availabel with Id " + id.ToString();
                return RedirectToAction("Index");
            }

            return View(products);
        }

        // POST: Product/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _productDAL.UpdateProduct(product);
                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Product Details Updated  successfully!!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to Update the Product details";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var product = _productDAL.GetProductById(id).FirstOrDefault();
                if (product == null)
                {
                    TempData["InfoMessage"] = "Product Not available with id " + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Product/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {

                string result = _productDAL.DeleteProduct(id);

                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
