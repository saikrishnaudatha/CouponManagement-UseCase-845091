using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_UI.Models;
using Newtonsoft.Json;


namespace MVC_UI.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// To view the users
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetUsers()
        {
            List<UserDetails> userDetails = new List<UserDetails>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:50443/api/v1/GetAllUsers"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    userDetails = JsonConvert.DeserializeObject<List<UserDetails>>(apiResponse);
                }
            }
            return View(userDetails);
        }
        /// <summary>
        /// To view the profile of particular user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserById(int id)
        {
            UserDetails user = new UserDetails();
            int userId = Convert.ToInt32(TempData["userid"]);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:50443/api/v1/GetUser/" + userId))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<UserDetails>(apiResponse);
                }
            }
            return View(user);
        }

        public async Task<IActionResult> UserRegister()
        {
            return View();
        }
        /// <summary>
        /// To Register the userdetails
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UserRegister(UserDetails user)
        {
            UserDetails user1 = new UserDetails();
            user.CreateDate = DateTime.Now;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                 
                using (var response = await httpClient.PostAsync("http://localhost:50443/api/v1/UserRegister/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //user1 = JsonConvert.DeserializeObject<UserDetails>(apiResponse);
                }
            }
            return RedirectToAction("UserLogin");
        }
        public async Task<IActionResult> UserLogin()
        {
            return View();
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UserLogin(UserDetails userDetails)
        {
            string apiResponse;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(userDetails), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:50443/api/v1/UserLogin/", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    //userDetails1 = JsonConvert.DeserializeObject<UserDetails>(apiResponse);
                   
                }
            }
            if(apiResponse== "successfully logged in")
            {
                int userId;
               using (var httpClient=new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:50443/api/v1/GetIdByName/" + userDetails.UserName)) 
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                        userId = JsonConvert.DeserializeObject<Int32>(apiResponse);
                    }
                    TempData["userid"] = userId;
                    return RedirectToAction("CouponsByUserId", "Coupon", new { id = userId });
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid Credentails";
                return View();
            }
           
        }
        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            UserDetails user = new UserDetails();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:50443/api/v1/GetUser/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<UserDetails>(apiResponse);
                }
            }
            return View(user);
        }
        /// <summary>
        /// To update the Profile
        /// </summary>
        /// <param name="userdetails"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserDetails userdetails)
        {
            userdetails.UpdatedDate=DateTime.Now;
            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(userdetails),System.Text.Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("http://localhost:50443/api/v1/UpdateUser/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    //user = JsonConvert.DeserializeObject<UserDetails>(apiResponse);
                }
            }
            return RedirectToAction("CouponsByUserId", "Coupon", new { id = userdetails.UserId });
        }



    }
}