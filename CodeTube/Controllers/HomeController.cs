﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CodeTube.Models;
using CodeTube.Interfaces;


namespace CodeTube.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IVideoRepository _VideoRepository;

    public HomeController(ILogger<HomeController> logger, IVideoRepository VideoRepository)
    {
        _logger = logger;
        _VideoRepository = VideoRepository;
    }

    public IActionResult Index()
    {
        var Videos = _VideoRepository.ReadAllDetailed();
        return View(Videos);
    }

    public IActionResult Video(int id)
    {
        var Video  = _VideoRepository.ReadByIdDetailed(id);
        return View(Video);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        _logger.LogError("Ocorreu um erro");
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}