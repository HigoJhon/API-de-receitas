using Microsoft.AspNetCore.Mvc;
using recipes_api.Services;
using recipes_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace recipes_api.Controllers;

[ApiController]
[Route("recipe")]
public class RecipesController : ControllerBase
{    
    public readonly IRecipeService _service;
    
    public RecipesController(IRecipeService service)
    {
        this._service = service;        
    }

    [HttpGet]
    public IActionResult Get()
    {
        var recipes = _service.GetRecipes();
        return Ok(recipes);
    }

    [HttpGet("{name}", Name = "GetRecipe")]
    public IActionResult Get(string name)
    {                
        var recipe = _service.GetRecipe(name);
        return Ok(recipe);
    }

    [HttpPost]
    public IActionResult Create([FromBody]Recipe recipe)
    {
        _service.AddRecipe(recipe);
        return CreatedAtRoute("GetRecipe", new { 
            name = recipe.Name,
            Recipe = recipe.RecipeType,
            recipe.PreparationTime,
            recipe.Ingredients,
            recipe.Directions,
            recipe.Rating
            }, recipe);
    }

    [HttpPut("{name}")]
    public IActionResult Update(string name, [FromBody]Recipe recipe)
    {
        var isValidName = _service.RecipeExists(name);
        
        if (!isValidName)
        {
            return BadRequest("Recipe not found");
        }
        var isRecipe = _service.RecipeExists(recipe.Name);
        if (!isRecipe)
        {
            return BadRequest("Recipe not found");
        }
        _service.UpdateRecipe(recipe);
        return NoContent();
    }

    [HttpDelete("{name}")]
    public IActionResult Delete(string name)
    {
        var isValidName = _service.RecipeExists(name);
        
        if (!isValidName)
        {
            return BadRequest("Recipe not found");
        }
        _service.DeleteRecipe(name);
        return NoContent();
    }    
}
