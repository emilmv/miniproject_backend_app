﻿using Juan_PB301EmilMusayev.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Juan_PB301EmilMusayev.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly ILayoutService _layoutService;

        public ContactUsController(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public IActionResult Index()
        {
            return View(_layoutService.GetSettings());
        }
    }
}
