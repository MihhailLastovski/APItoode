﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using ToodeAPI.Models;
using APItoode.Data;

namespace APItoode.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;

        public PaymentController(HttpClient httpClient, ApplicationDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        [HttpGet("{sum}/{id}")]
        public async Task<IActionResult> MakePayment(string sum, int id)
        {
            Toode toode = new Toode();
            foreach (Toode item in _context.Tooted)
            {
                if (item.Id == id)
                {
                    toode = item;
                }
            }

            var paymentData = new
            {
                api_username = "e36eb40f5ec87fa2",
                account_name = "EUR3D1",
                amount = sum,
                order_reference = Math.Ceiling(new Random().NextDouble() * 999999),
                nonce = $"a9b7f7e7as{DateTime.Now}{new Random().NextDouble() * 999999}",
                timestamp = DateTime.Now,
                customer_url = "https://maksmine.web.app/makse"
            };

            var json = JsonSerializer.Serialize(paymentData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "ZTM2ZWI0MGY1ZWM4N2ZhMjo3YjkxYTNiOWUxYjc0NTI0YzJlOWZjMjgyZjhhYzhjZA==");

            var response = await client.PostAsync("https://igw-demo.every-pay.com/api/v4/payments/oneoff", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(responseContent);
                var paymentLink = jsonDoc.RootElement.GetProperty("payment_link");

                toode.Kogus -= 1;
                if (toode.Kogus == 0)
                {
                    toode.IsActive = false;
                }
                _context.Tooted.Update(toode);
                _context.SaveChanges();
                _context.Tooted.ToList();
                return Ok(paymentLink);
            }
            else
            {
                return BadRequest("Payment failed.");
            }
        }
    }
}
