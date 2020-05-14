using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Moq;
using Juriba.ApiProject.Data;
using Juriba.ApiProject.Models;
using Juriba.ApiProject.Services;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Juriba.ApiProject.Tests.Services
{
    [TestFixture]
    internal sealed class ArticleServiceTests
    {
        private Mock<IDataAccessory> _dataAccessoryMock;

        private IArticleService _articleService;

        [SetUp]
        public void SetUp()
        {
            _dataAccessoryMock = new Mock<IDataAccessory>();
            _articleService = new ArticleService(_dataAccessoryMock.Object);
        }

        #region GetSectionItems

        [Test]
        public void GetSectionItems_CallsGetDataOnce()
        {
            // Arrange
            _dataAccessoryMock.Setup(i => i.GetData(It.IsAny<string>()))
                .Returns(new JToken[0]);

            // Act
            _articleService.GetSectionItems(It.IsAny<string>());

            // Assert
            _dataAccessoryMock.Verify(i => i.GetData(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetSectionItems_CallsGetDataWithCorrectParametersOnce()
        {
            // Arrange
            _dataAccessoryMock.Setup(i => i.GetData(It.IsAny<string>()))
                .Returns(new JToken[0]);

            // Act
            _articleService.GetSectionItems("home");

            // Assert
            _dataAccessoryMock.Verify(i => i.GetData("home"), Times.Once);
        }

        [Test]
        public void GetSectionItems_ReturnsCorrectResult()
        {
            // Arrange
            JToken[] allArticles = GetAllArticles();

            _dataAccessoryMock.Setup(i => i.GetData(It.IsAny<string>()))
                .Returns(allArticles);

            // Act
            IEnumerable<ArticleView> result = _articleService.GetSectionItems(It.IsAny<string>());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(allArticles.Count(), result.Count());
        }

        #endregion

        #region GetFirstSectionItem

        [Test]
        public void GetFirstSectionItem_CallsGetDataOnce()
        {
            // Arrange
            _dataAccessoryMock.Setup(i => i.GetData(It.IsAny<string>()))
                .Returns(new JToken[0]);

            // Act
            _articleService.GetFirstSectionItem(It.IsAny<string>());

            // Assert
            _dataAccessoryMock.Verify(i => i.GetData(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetFirstSectionItem_CallsGetDataWithCorrectParametersOnce()
        {
            // Arrange
            _dataAccessoryMock.Setup(i => i.GetData(It.IsAny<string>()))
                .Returns(new JToken[0]);

            // Act
            _articleService.GetFirstSectionItem("business");

            // Assert
            _dataAccessoryMock.Verify(i => i.GetData("business"), Times.Once);
        }

        [Test]
        public void GetFirstSectionItem_ReturnsCorrectData()
        {
            // Arrange
            ArticleView expectedResult = new ArticleView
            {
                Heading = "Coronavirus Live Updates: Americans Are on the Move, Even as New Warnings Sound",
                Updated = DateTime.Parse("2020-05-13T07:56:56-04:00"),
                Link = "https://www.nytimes.com/2020/05/13/us/coronavirus-updates.html"
            };

            _dataAccessoryMock.Setup(i => i.GetData(It.IsAny<string>()))
                .Returns(GetAllArticles());

            // Act
            ArticleView result = _articleService.GetFirstSectionItem(It.IsAny<string>());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult.Heading, result.Heading);
            Assert.AreEqual(expectedResult.Updated, result.Updated);
            Assert.AreEqual(expectedResult.Link, result.Link);
        }

        #endregion

        #region GetSectionItemsSortedByUpdatedDate

        [Test]
        public void GetSectionItemsSortedByUpdatedDate_CallsGetDataOnce()
        {
            // Arrange
            _dataAccessoryMock.Setup(i => i.GetData(It.IsAny<string>()))
                .Returns(new JToken[0]);

            // Act
            _articleService.GetSectionItemsSortedByUpdatedDate(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            _dataAccessoryMock.Verify(i => i.GetData(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetSectionItemsSortedByUpdatedDate_CallsGetDataWithCorrectParametersOnce()
        {
            // Arrange
            _dataAccessoryMock.Setup(i => i.GetData(It.IsAny<string>()))
                .Returns(new JToken[0]);

            // Act
            _articleService.GetSectionItemsSortedByUpdatedDate("home", It.IsAny<string>());

            // Assert
            _dataAccessoryMock.Verify(i => i.GetData("home"), Times.Once);
        }

        [Test]
        public void GetSectionItemsSortedByUpdatedDate_ReturnsCorrectDataSortedByDate()
        {
            // Arrange
            _dataAccessoryMock.Setup(i => i.GetData(It.IsAny<string>()))
                .Returns(GetAllArticles());

            // Act
            IEnumerable<ArticleView> result = _articleService.GetSectionItemsSortedByUpdatedDate(It.IsAny<string>(), new DateTime(2020, 05, 13).ToString("yyyy-MM-dd"));

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(52, result.Count());
        }

        #endregion

        #region GetSectionItemByShortUrl

        [Test]
        public void GetSectionItemByShortUrl_CallsGetDataOnce()
        {
            // Arrange
            _dataAccessoryMock.Setup(i => i.GetData(It.IsAny<string>()))
                .Returns(new JToken[0]);

            // Act
            _articleService.GetSectionItemByShortUrl(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            _dataAccessoryMock.Verify(i => i.GetData(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetSectionItemByShortUrl_CallsGetDataWithCorrectParametersOnce()
        {
            // Arrange
            _dataAccessoryMock.Setup(i => i.GetData(It.IsAny<string>()))
                .Returns(new JToken[0]);

            // Act
            _articleService.GetSectionItemByShortUrl("home", It.IsAny<string>());

            // Assert
            _dataAccessoryMock.Verify(i => i.GetData("home"), Times.Once);
        }

        [Test]
        public void GetSectionItemByShortUrl_ReturnsCorrectDataSortedByShortUrl()
        {
            // Arrange
            ArticleView expectedResult = new ArticleView
            {
                Heading = "Coronavirus Live Updates: Americans Are on the Move, Even as New Warnings Sound",
                Updated = DateTime.Parse("2020-05-13T07:56:56-04:00"),
                Link = "https://www.nytimes.com/2020/05/13/us/coronavirus-updates.html"
            };

            _dataAccessoryMock.Setup(i => i.GetData(It.IsAny<string>()))
                .Returns(GetAllArticles());

            // Act
            ArticleView result = _articleService.GetSectionItemByShortUrl("world", "2WthSYo");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult.Heading, result.Heading);
            Assert.AreEqual(expectedResult.Updated, result.Updated);
            Assert.AreEqual(expectedResult.Link, result.Link);
        }

        #endregion

        #region GetSectionItemByShortUrl

        [Test]
        public void GetGroupedByDate_CallsGetDataOnce()
        {
            // Arrange
            _dataAccessoryMock.Setup(i => i.GetData(It.IsAny<string>()))
                .Returns(new JToken[0]);

            // Act
            _articleService.GetSectionItemsGroupedByDate(It.IsAny<string>());

            // Assert
            _dataAccessoryMock.Verify(i => i.GetData(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetGroupedByDate_CallsGetDataWithCorrectParametersOnce()
        {
            // Arrange
            _dataAccessoryMock.Setup(i => i.GetData(It.IsAny<string>()))
                .Returns(new JToken[0]);

            // Act
            _articleService.GetSectionItemsGroupedByDate("home");

            // Assert
            _dataAccessoryMock.Verify(i => i.GetData("home"), Times.Once);
        }

        [Test]
        public void GetGroupedByDate_ReturnsCorrectData()
        {
            // Arrange
            _dataAccessoryMock.Setup(i => i.GetData(It.IsAny<string>()))
                .Returns(GetAllArticles());

            // Act
            IEnumerable<ArticleGroupedByDateView> result = _articleService.GetSectionItemsGroupedByDate(It.IsAny<string>());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(9, result.Count());
        }

        #endregion

        #region Accessory methods

        private JToken[] GetAllArticles()
        {
            return JObject.Parse(File
                .ReadAllText(Path
                .Combine(Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Fake/Data.json"), Encoding.UTF8))
                .GetValue("results")
                .ToObject<JToken[]>();
        }

        #endregion
    }
}
