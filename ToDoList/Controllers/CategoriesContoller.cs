using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Controllers
{
  public class CategoryController : Controller
  {
    [HttpGet("/categories/new")]
    public ActionResult CategoryForm()
    {
      return View(Category.GetAll());
    }

    [HttpPost("/categories")]
    public ActionResult CollectCategory()
    {
      Category newCategory = new Category(Request.Form["new-category"]);
      newCategory.Save();
      List<Category> allCategories = Category.GetAll();
      return RedirectToAction("Index");
    }

    [HttpGet("/categories")]
    public ActionResult Index()
    {
      return View(Category.GetAll());
    }

    // [HttpGet("/categories/{id}/items")]
    // public ActionResult List(int id)
    // {
    //   Category thisCategory = Category.Find(id);
    //
    //   List<Item> allItems = thisCategory.GetItems();
    //   return View(thisCategory);
    // }

  }
}
