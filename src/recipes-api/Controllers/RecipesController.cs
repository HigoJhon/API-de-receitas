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

    // 1 - Sua aplicação deve ter o endpoint GET /recipe
    //Read
    [HttpGet]
    public IActionResult Get()
    {
        var recipes = _service.GetRecipes();
        return Ok(recipes);
    }

    // 2 - Sua aplicação deve ter o endpoint GET /recipe/:name
    //Read
    [HttpGet("{name}", Name = "GetRecipe")]
    public IActionResult Get(string name)
    {                
        var recipe = _service.GetRecipe(name);
        return Ok(recipe);
    }

    // 3 - Sua aplicação deve ter o endpoint POST /recipe
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

    // 4 - Sua aplicação deve ter o endpoint PUT /recipe
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

    // 5 - Sua aplicação deve ter o endpoint DEL /recipe
    [HttpDelete("{name}")]
    public IActionResult Delete(string name)
    {
        throw new NotImplementedException();
    }    
}
