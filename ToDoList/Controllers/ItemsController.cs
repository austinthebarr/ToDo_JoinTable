using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoList.Models;
using System;
namespace ToDoList.Controllers
{
  public class ItemController : Controller
  {
    [HttpPost("/items")]
    public ActionResult CollectInfo(string newitem, string newdate)
    {
      Item newItem = new Item(newitem, newdate);
      newItem.Save();
      // List<Item> all = Item.GetAll();
      return RedirectToAction("Success", "Home");
    }

    [HttpGet("/items")]
    public ActionResult Index()
    {
      List<Item> all = Item.GetAll();
      return View(all);
    }

    [HttpGet("/items/new")]
    public ActionResult CreateForm()
    {
      return View(Category.GetAll());
    }

    [HttpGet("/items/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Item selectedItem = Item.Find(id);
      List<Category> itemCategories = selectedItem.GetCategories();
      List<Category> allCategories = Category.GetAll();
      model.Add("selectedItem", selectedItem);
      model.Add("itemCategories", itemCategories);
      model.Add("allCategories", allCategories);
      return View(model);
    }

    [HttpPost("/items/{itemId}/categories/new")]
    public ActionResult AddCategory(int itemId)
    {
      Item item = Item.Find(itemId);
      Category category = Category.Find(Int32.Parse(Request.Form["category-id"]));
      item.AddCategory(category);
      return RedirectToAction("Details", new {id = itemId});
    }

    // [HttpGet("/items/{id}/update")]
    // public ActionResult UpdateForm(int id)
    // {
    //   Item thisItem = Item.Find(id);
    //   return View(thisItem);
    // }
    //
    // [HttpPost("/items/{id}/update")]
    // public ActionResult Update(int id)
    // {
    //   Item thisItem = Item.Find(id);
    //   thisItem.Edit(Request.Form["newdescription"]);
    //   return RedirectToAction("Index");
    // }
    //
    // [HttpGet("/items/{id}/delete")]
    //   public ActionResult Delete(int id)
    // {
    //   Item thisItem = Item.Find(id);
    //   return View(thisItem);
    // }
    //
    // [HttpPost("/items/{id}/delete")]
    // public ActionResult DeleteItem(int id)
    // {
    //   Item thisItem = Item.Find(id);
    //   thisItem.Delete();
    //   return RedirectToAction("Index");
    // }
  }
}
