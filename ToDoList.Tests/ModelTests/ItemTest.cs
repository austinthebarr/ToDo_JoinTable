using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoList.Models;
using System;

namespace ToDoList.Tests
{
  [TestClass]
  public class ItemTests : IDisposable
  {
    public void Dispose()
    {
      Item.DeleteAll();
      Category.DeleteAll();
    }
    public ItemTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=todo_test;";
    }
    [TestMethod]
    public void GetAll_DbStartsEmpty_0()
    {
      //Arrange
      //Act
      int result = Item.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
    {
      //Arrange, Action
      Item firstItem = new Item("Mow the lawn", "Now");
      Item secondItem = new Item("Mow the lawn", "Now");

      //Assert
      Assert.AreEqual(firstItem, secondItem);
    }

    [TestMethod]
    public void Save_SavesToDatabase_ItemList()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn", "Now");

      //Act
      testItem.Save();
      List<Item> result = Item.GetAll();
      List<Item> testList = new List<Item>{testItem};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn", "Now");

      //Act
      testItem.Save();
      Item savedItem = Item.GetAll()[0];

      int result = savedItem.GetId();
      int testId = testItem.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Find_FindsItemInDatabase_Item()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn", "now");
      testItem.Save();

      //Act
      Item foundItem = Item.Find(testItem.GetId());

      //Assert
      Assert.AreEqual(testItem, foundItem);
    }
    [TestMethod]
    public void Edit_UpdatesItemInDatabase_String()
    {
      //Arrange
      string firstDescription = "Walk the Dog";
      string dueDate = "now";
      Item testItem = new Item(firstDescription, dueDate);
      testItem.Save();
      string secondDescription = "Mow the lawn";

      //Act
      testItem.Edit(secondDescription);

      string result = Item.Find(testItem.GetId()).GetDescription();

      //Assert
      Assert.AreEqual(secondDescription, result);
    }
    [TestMethod]
    public void Delete_A_Specific_ITEM()
    {
      //Arrange
      Item newItem1 = new Item("Eat dinner", "today");
      newItem1.Save();
      Item newItem2 = new Item("Walk the dog", "tomorrow");
      newItem2.Save();
      Assert.IsTrue(Item.GetAll().Count == 2);

      //Act
      newItem1.Delete();
      List<Item> expectedList = new List<Item> {newItem2};
      // Console.WriteLine(newItem1.GetDescription());

      //Assert
      List<Item> outputList = Item.GetAll();
      Assert.IsTrue(outputList.Count == 1);
      CollectionAssert.AreEqual(expectedList, outputList);
    }

    [TestMethod]
    public void AddCategory_addsCategoryToItem_CategoryList()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn", "today");
      testItem.Save();

      Category testCategory = new Category("Home stuff");
      testCategory.Save();

      //Act
      testItem.AddCategory(testCategory);

      List<Category> result = testItem.GetCategories();
      List<Category> testList = new List<Category>{testCategory};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetCategories_ReturnsAllItemsCategories_CategoryList()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn", "today");
      testItem.Save();

      Category testCategory1 = new Category("Home Stuff");
      testCategory1.Save();

      Category testCategory2 = new Category("Work stuff");
      testCategory2.Save();

      //Act
      testItem.AddCategory(testCategory1);
      List<Category> result = testItem.GetCategories();
      List<Category> testList = new List<Category> {testCategory1};

      //Assert

      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Delete_DeletesItemAssociationsFromDatabase_ItemList()
    {
      //Arrange
      Category testCategory = new Category("Home stuff");
      testCategory.Save();

      string testDescription = "Mow the lawn";
      string testDueDate = "today";
      Item testItem = new Item(testDescription, testDueDate);
      testItem.Save();

      //Act
      testItem.AddCategory(testCategory);
      testItem.Delete();

      List<Item> resultCategoryItems = testCategory.GetItems();
      List<Item> testCategoryItems = new List<Item> {};

      //Assert
      CollectionAssert.AreEqual(testCategoryItems, resultCategoryItems);
    }

    [TestMethod]
    public void Done_ChangesValueOfCheckOffandChangesBitValueinTABLE()
    {
      //Arrange
      Item testItem1 = new Item("hisato walks", "now");
      testItem1.Save();

      //Act
      testItem1.Done();

      //Assert
    Assert.AreEqual(true, testItem1.GetCheckOff());
    }
  }
}
