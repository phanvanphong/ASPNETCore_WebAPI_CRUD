using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestAPIvsJWT.Controllers.API;
using TestAPIvsJWT.Models;
using TestAPIvsJWT.ViewModel;

namespace TestAPIvsJWT.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        public ProductController(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products = new List<Product>();
            // Sử dụng HttpClient lấy request từ người dùng
            using(var httpClient = new HttpClient())
            {
                // Thực hiện truy vấn GET 
                var response = await httpClient.GetAsync("https://localhost:44397/api/product");
                // ReadAsStringAsync đọc nội dung (content) HTTP trả về chuỗi (có encoding) JSon
                string apiResponse = await response.Content.ReadAsStringAsync();
                // Chuyển Json về thành đối tượng products
                products = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
            }

            var productsViewModel = _mapper.Map<List<ProductViewModel>>(products);
            return View(productsViewModel);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            // C1: Sử dụng HttpRequestMessage
            using( var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://localhost:44397/api/product"),
                    Method = new HttpMethod("Post"),
                    Content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json")
                };

                await httpClient.SendAsync(request);


            // C2: Sử dụng PostAsync
            //// Chuyển đối tượng product về Json (StringContent chứa 3 tham số)
            //StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            //// Thực hiện truy vấn post và truyền content
            //var response = await httpClient.PostAsync("https://localhost:44397/api/product", content);
            }
            return RedirectToAction("Index", "Product");
        }


        public async Task<IActionResult> Edit(int id)
        {
            Product product = new Product();
            using (var httpClient = new HttpClient())
            {   
                // Thực hiện truy vấn Get với url + id
                var response = await httpClient.GetAsync("https://localhost:44397/api/product/" + id);
                // Đọc nội dung Content HTTP và trả về chuỗi (có encoding) Json
                string apiResponse = await response.Content.ReadAsStringAsync();
                // Chuyển Json về thành đối tượng
                product = JsonConvert.DeserializeObject<Product>(apiResponse);
            }

            var productViewModel = _mapper.Map<ProductViewModel>(product);
            return View(productViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id,Product product)
        {
            using (var httpClient = new HttpClient())
            {
                // C1: Sử dụng HttpRequestMessage
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://localhost:44397/api/product/" + id),
                    Method = new HttpMethod("Put"),
                    Content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json")
                };

                await httpClient.SendAsync(request);


                //// C2: sử dụng PutAsync
                //// Chuyển đối tượng product về Json (StringContent chứa 3 tham số)
                //StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                //// Thực hiện truy vấn post và truyền content
                //var response = await httpClient.PutAsync("https://localhost:44397/api/product/" + id, content);
            }
            return RedirectToAction("Index", "Product");
        }



        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {   
                // Thực hiện truy vấn Delete với url + id
                var response = await httpClient.DeleteAsync("https://localhost:44397/api/product/" + id);
            }
            return RedirectToAction("Index", "Product");
        }


    }
}
