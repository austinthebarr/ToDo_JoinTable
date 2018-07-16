using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoList.Models;

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
      return RedirectToAction("Index");
    }

    [HttpGet("/items")]
    public ActionResult Index()
    {
      // List<Item> all = Item.GetAll();
      return View(Item.GetAll());
    }

    [HttpGet("/items/new")]
    public ActionResult CreateForm()
    {
      return View(Category.GetAll());
    }

    [HttpGet("/items/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
      Item thisItem = Item.Find(id);
      return View(thisItem);
    }

    [HttpPost("/items/{id}/update")]
    public ActionResult Update(int id)
    {
      Item thisItem = Item.Find(id);
      thisItem.Edit(Request.Form["newdescription"]);
      return RedirectToAction("Index");
    }

    [HttpGet("/items/{id}/delete")]
      public ActionResult Delete(int id)
    {
      Item thisItem = Item.Find(id);
      return View(thisItem);
    }

    [HttpPost("/items/{id}/delete")]
    public ActionResult DeleteItem(int id)
    {
      Item thisItem = Item.Find(id);
      thisItem.Delete();
      return RedirectToAction("Index");
    }


      // [HttpPost("/items/delete")]
    // public ActionResult DeleteAll()
    // {ß
    //   Item.ClearAll();
    //   return View();
    // }
  }
}
