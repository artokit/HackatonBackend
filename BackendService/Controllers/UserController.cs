﻿using Database.Interfaces;
using System.Web;
using EducationService.Models;
using EducationService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationService.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController: BaseController
{
    private readonly UserService userService;

    public UserController(UserService userService)
    {
        this.userService = userService;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetInfo(int id)
    {
        var userExist = await userService.GetById(id);
        if (userExist == null)
        {
            return BadRequest();
        }

        return Ok(userExist);
    }
}
