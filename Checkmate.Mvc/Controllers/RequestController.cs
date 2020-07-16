using Checkmate.Business;
using Checkmate.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Checkmate.Mvc.Controllers
{
    public class RequestController : Controller
    {
        private readonly IRequestService requestService;
        public RequestController(IRequestService requestService)
        {
            this.requestService = requestService;
        }

        [HttpGet("request/get")]
        public IActionResult Get()
        {
            var items = this.requestService.GetRequests();
            return new JsonResult(items);
        }

        public IActionResult Requests()
        {           
            return View();
        }

        [HttpPost("request/update/{typeid:int}/{ids?}")]
        public IActionResult UpdateRequests(int typeid, string ids)
        {
            List<int> items = new List<int>();
            items = ids.Split(" ").Select(int.Parse).ToList();
            return new JsonResult(this.requestService.UpdateRequest(items, typeid));
        }

    }
}
