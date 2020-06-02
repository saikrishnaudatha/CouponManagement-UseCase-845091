using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_UI.Models;
using Newtonsoft.Json;

namespace MVC_UI.Controllers
{
    public class CouponController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// To view the coupons
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetCoupons()
        {
            List<CouponDetails> couponDetails = new List<CouponDetails>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:50187/api/v2/GetCoupons"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    couponDetails = JsonConvert.DeserializeObject<List<CouponDetails>>(apiResponse);
                }
            }
            return View(couponDetails);
        }
        [HttpGet]
        public async Task<IActionResult> CouponsByUserId(int id)
        {
            List<CouponDetails> couponDetails = new List<CouponDetails>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:50187/api/v2/GetAllCoupons/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    couponDetails = JsonConvert.DeserializeObject<List<CouponDetails>>(apiResponse);
                }
            }
            TempData["userid"] = id;
            Console.WriteLine(couponDetails);
            return View(couponDetails);
        }
        /// <summary>
        /// viewing the particular coupon details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetCouponById(int id)
        {
            CouponDetails coupon = new CouponDetails();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:50187/api/v2/GetCouponById/" + id))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                   coupon = JsonConvert.DeserializeObject<CouponDetails>(apiResponse);
                }
            }
            return View(coupon);
        }
        public async Task<IActionResult> CouponRegister()
        {
            return View();
        }
        /// <summary>
        /// Register the coupon details
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CouponRegister(CouponDetails coupons)
        {
            CouponDetails coupon = coupons;
            coupon.CreateDate = DateTime.Now;
            int userId = Convert.ToInt32(TempData["userid"]);
            coupon.UserId = userId;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(coupon), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:50187/api/v2/AddCoupon/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //user1 = JsonConvert.DeserializeObject<UserDetails>(apiResponse);
                }
            }
            return RedirectToAction("CouponsByUserId", "Coupon", new { id = coupon.UserId });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {

            CouponDetails coupon = new CouponDetails();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:50187/api/v2/GetCouponById/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    coupon = JsonConvert.DeserializeObject<CouponDetails>(apiResponse);
                }
            }
            TempData["userid"] = coupon.UserId;
            return View(coupon);
        }
        /// <summary>
        /// Deleting the coupon details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            CouponDetails coupon1 = new CouponDetails();
            using (var httpClient = new HttpClient())
            {


                using (var response = await httpClient.DeleteAsync("http://localhost:50187/api/v2/DeleteCoupon/"+id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //user1 = JsonConvert.DeserializeObject<UserDetails>(apiResponse);
                }
            }
            
            return RedirectToAction("CouponsByUserId", "Coupon", new { id = TempData["userid"] });
                   
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCoupon(int id)
        {
            CouponDetails coupon = new CouponDetails();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:50187/api/v2/GetCouponById/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    coupon = JsonConvert.DeserializeObject<CouponDetails>(apiResponse);
                }
            }
            return View(coupon);
        }
        /// <summary>
        /// updating the coupon details
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateCoupon(CouponDetails coupon)
        {
            coupon.UpdatedDate = DateTime.Now;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(coupon),System.Text. Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("http://localhost:50187/api/v2/UpdateCoupon/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    //coupon1 = JsonConvert.DeserializeObject<CouponDetails>(apiResponse);
                }
            }
            return RedirectToAction("CouponsByUserId", "Coupon", new { id = coupon.UserId });
        }

    }
}