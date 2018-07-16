using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using ToDoList.Models;

namespace ToDoList.Tests
{
  [TestClass]
  public class CategoryTests : IDisposable
  {
    public CategoryTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=todo_test;";
    }

    [TestMethod]
    public void GetAll_CategoriesEmptyAtFisrt_0()
    {
      //Arrange, Act
      int result = Category.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Category()
    {
      //Arrange, Act
      Category firstCategory = new Category("Household chores");
      Category secondCategory = new Category("Household chores");

      //Assert
      Assert.AreEqual(firstCategory, secondCategory);
    }
    [TestMethod]
    public void Save_SavesToDatabase_CategoryList()
    {
      //Arrange
      Category testCategory = new Category("Household chores");
      testCategory.Save();

      //Act
      List<Category> result = Category.GetAll();
      List<Category> testList = new List<Category>{testCategory};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToCategory_Id()
    {
      //Arrange
      Category testCategory = new Category("Household chores");
      testCategory.Save();

      //Act
      Category savedCategory = Category.GetAll()[0];

      int result = savedCategory.GetId();
      int testId = testCategory.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Find_FindsCategoryInDatabase_Category()
    {
      //Arrange
      Category testCategory = new Category("Household chores");
      testCategory.Save();

      //Act
      Category foundCategory = Category.Find(testCategory.GetId());

      //Assert
      Assert.AreEqual(testCategory, foundCategory);
    }
    [TestMethod]
    public void Delete_DeleteCategoryAssociationsFromDatabase_CategoryList()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn", "right meow");

      testItem.Save();

      string testName = "Home stuff";
      Category testCategory = new Category(testName);
      testCategory.Save();

      //Act
      testCategory.AddItem(testItem);
      testCategory.Delete();

      List<Category> resultItemCategories = testItem.GetCategories();
      List<Category> testItemCategories = new List<Category> {};

      //Assert
      CollectionAssert.AreEqual(testItemCategories, resultItemCategories);
    }
    [TestMethod]
    public void Test_AddItem_AddsItemToCategory()
    {
      //Arrange
      Category testCategory = new Category("Household chores");
      testCategory.Save();

      Item testItem = new Item("Mow the lawn", "meow");
      testItem.Save();

      Item testItem2 = new Item("Water the garden", "meow");
      testItem2.Save();

      //Act
      testCategory.AddItem(testItem);
      testCategory.AddItem(testItem2);

      List<Item> result = testCategory.GetItems();
      List<Item> testList = new List<Item>{testItem, testItem2};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void GetItems_ReturnAllCategoryItems_ItemList()
    {
      //Arrange
      Category testCategory = new Category("Household chores");
      testCategory.Save();

      Item testItem1 = new Item("Mow the lawn", "meow");
      testItem1.Save();

      //Act
      testCategory.AddItem(testItem1);
      List<Item> savedItems = testCategory.GetItems();
      List<Item> testList = new List<Item> {testItem1};

      //Assert
      CollectionAssert.AreEqual(testList, savedItems);
    }
    [TestMethod]
    public void GetItems_RetrievesAllItemsWithCategory_ItemList()
    {
      Category testCategory = new Category("Household chores");
      testCategory.Save();

      Item firstItem = new Item("Mow the lawn", "now");
      firstItem.Save();
      testCategory.AddItem(firstItem);
      Item secondItem = new Item("Do the dishes", "dude");
      secondItem.Save();
      testCategory.AddItem(secondItem);

      List<Item> testItemList = new List<Item> {firstItem, secondItem};
      List<Item> resultItemList = testCategory.GetItems();

      CollectionAssert.AreEqual(testItemList, resultItemList);
    }

    public void Dispose()
    {
      Item.DeleteAll();
      Category.DeleteAll();
    }
  }
}
