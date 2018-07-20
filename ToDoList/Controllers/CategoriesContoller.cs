using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Controllers
{
  public class CategoryController : Controller
  {
    [HttpGet("/categories/new")]
    public ActionResult CreateForm()
    {
      return View(Category.GetAll());
    }

    [HttpPost("/categories")]
    public ActionResult Create()
    {
      Category newCategory = new Category(Request.Form["new-category"]);
      newCategory.Save();
      List<Category> allCategories = Category.GetAll();
      return RedirectToAction("Success", "Home");
    }

    [HttpGet("/categories")]
    public ActionResult Index()
    {
      return View(Category.GetAll());
    }

    [HttpGet("/categories/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Category selectedCategory = Category.Find(id);
      List<Item> categoryItems = selectedCategory.GetItems();
      List<Item> allItems = Item.GetAll();
      model.Add("selectedCategory", selectedCategory);
      model.Add("categoryItems", categoryItems);
      model.Add("allItems", allItems);
      return View(model);
    }

    [HttpPost("/categories/{categoryId}/items/new")]
    public ActionResult AddItem(int categoryId)
    {
      Category category = Category.Find(categoryId);
      Item item = Item.Find(int32.Parse(Request.Form["item-id"]));
      category.AddItem(item);
      return RedirectToAction("Details", new {id = categoryId});
    }  

  }
}
