using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Moq;
using PhotoStorage.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoStorage.Controllers;
using PhotoStorage.Models;

namespace PhotoStorage.Tests.Controllers
{
    [TestClass]
    public class GalleryControllerTest
    {
        private Mock<IGenericRepository<Gallery>> galleryMock;
        private GalleryController galleryController;
        private List<Gallery> galleryList;

        [TestInitialize]
        public void Intialize()
        {
            galleryMock = new Mock<IGenericRepository<Gallery>>();
            galleryController = new GalleryController();
            galleryList = new List<Gallery>()
            {
                new Gallery() {Id = 1, GalleryName = "TestGallery1", Description = "This is the first test gallery"},
                new Gallery() {Id = 2, GalleryName = "TestGallery2", Description = "This is the second test gallery"},
                new Gallery() {Id = 3, GalleryName = "TestGallery3", Description = "This is the third test gallery"},
                new Gallery() {Id = 4, GalleryName = "TestGallery4", Description = "This is the fourth test gallery"}
            };
        }

        [TestMethod]
        public void Index_Returns_Four()
        {
            var data = new List<Gallery>
            {
                new Gallery() {Id = 1, GalleryName = "TestGallery1", Description = "This is the first test gallery"},
                new Gallery() {Id = 2, GalleryName = "TestGallery2", Description = "This is the second test gallery"},
                new Gallery() {Id = 3, GalleryName = "TestGallery3", Description = "This is the third test gallery"},
                new Gallery() {Id = 4, GalleryName = "TestGallery4", Description = "This is the fourth test gallery"}
            };

            var mockSet = new Mock<DbSet<Gallery>>();
           
            var mockContext = new Mock<PhotoStorageDb>();
            mockContext.Setup(c => c.Galleries).Returns(mockSet.Object);

            var result = ((galleryController.Index() as ViewResult).Model) as List<Gallery>;

            Assert.AreEqual(3, result.Count);
            //Assert.AreEqual("AAA", blogs[0].Name);
            //Assert.AreEqual("BBB", blogs[1].Name);
            //Assert.AreEqual("ZZZ", blogs[2].Name); 



            //// Arrange 
            //galleryMock.Setup(x => x.GetAll()).Returns(galleryList);

            //// Act 
            //var result = ((galleryController.Index() as ViewResult).Model) as List<Gallery>;

            //// Assert
            //Assert.AreEqual(result.Count,4);
        }
    }
}
