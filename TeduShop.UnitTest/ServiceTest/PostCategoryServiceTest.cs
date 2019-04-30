using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using TeduShop.Service;

namespace TeduShop.UnitTest.ServiceTest
{
    [TestClass]
    public class PostCategoryServiceTest
    {
        private Mock<IPostCategoryRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IPostCategoryService _categoryService;
        private List<PostCategory> _listCatetory;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IPostCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _categoryService = new PostCategoryService(_mockRepository.Object, _mockUnitOfWork.Object);
            _listCatetory = new List<PostCategory>() {
                new PostCategory() { ID = 1, Name = "DM1", Alias = "dm1", Status = true, CreatedBy = "TestMode", CreatedDate = DateTime.Now },
                new PostCategory() { ID = 2, Name = "DM2", Alias = "dm2", Status = true, CreatedBy = "TestMode", CreatedDate = DateTime.Now },
                new PostCategory() { ID = 3, Name = "DM3", Alias = "dm3", Status = true, CreatedBy = "TestMode", CreatedDate = DateTime.Now }
            };
        }

        [TestMethod]
        public void PostCategory_Service_GetAll()
        {
            //step 1: setup method
            _mockRepository.Setup(m => m.GetAll(null)).Returns(_listCatetory);

            //step 2: call action
            var result = _categoryService.GetAll() as List<PostCategory>;

            //step 3: compare
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void PostCategory_Service_Create()
        {
            int id = 1;
            PostCategory postCategory = new PostCategory();
            postCategory.Name = "Test";
            postCategory.Alias = "test";
            postCategory.Status = true;
            postCategory.CreatedBy = "TestMode";
            postCategory.CreatedDate = DateTime.Now;

            _mockRepository.Setup(m => m.Add(postCategory)).Returns((PostCategory p) =>
            {
                p.ID = 1;
                return postCategory;
            });

            var result = _categoryService.Add(postCategory);
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.ID);
        }
    }
}
