using Moq;
using Nancy.Testing;
using Juriba.ApiProject.Models;
using Juriba.ApiProject.Modules;
using Juriba.ApiProject.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace Juriba.ApiProject.Tests.Modules
{
    [TestFixture]
    internal sealed class ArticleModuleTests
    {
        private Mock<IArticleService> _articleServiceMock;

        private ArticleModule _module;
        private Browser _browser;

        [SetUp]
        public void SetUp()
        {
            _articleServiceMock = new Mock<IArticleService>();
            _module = new ArticleModule(_articleServiceMock.Object);
            _browser = new Browser(x => x.Module(_module));
        }

        #region GetArticles

        [Test]
        public void GetArticles_CallsGetSectionItemsOnce()
        {
            // Arrange
            _articleServiceMock.Setup(i => i.GetSectionItems(It.IsAny<string>()))
                .Returns(new List<ArticleView>());

            // Act
            _browser.Get("/list/business");

            // Assert
            _articleServiceMock.Verify(i => i.GetSectionItems("business"), Times.Once);
        }

        [Test]
        public void GetArticles_ShouldReturnStatusOk()
        {
            // Arrange
            _articleServiceMock.Setup(i => i.GetSectionItems(It.IsAny<string>()))
                .Returns(new List<ArticleView>());

            // Act
            BrowserResponse result = _browser.Get("/list/business").Result;

            // Assert
            Assert.AreEqual(Nancy.HttpStatusCode.OK, result.StatusCode);
        }

        #endregion

        #region GetFirstArticle

        [Test]
        public void GetFirstArticle_CallsGetFirstSectionItemOnce()
        {
            // Arrange
            _articleServiceMock.Setup(i => i.GetFirstSectionItem(It.IsAny<string>()))
                .Returns(new ArticleView());

            // Act
            _browser.Get("/list/business/first");

            // Assert
            _articleServiceMock.Verify(i => i.GetFirstSectionItem("business"), Times.Once);
        }

        [Test]
        public void GetFirstArticle_ShouldReturnStatusOk()
        {
            // Arrange
            _articleServiceMock.Setup(i => i.GetFirstSectionItem(It.IsAny<string>()))
                .Returns(new ArticleView());

            // Act
            BrowserResponse result = _browser.Get("/list/business/first").Result;

            // Assert
            Assert.AreEqual(Nancy.HttpStatusCode.OK, result.StatusCode);
        }

        #endregion

        #region GetArticlesSortedByUpdatedDate

        [Test]
        public void GetArticlesSortedByUpdatedDate_CallsGetSectionItemsSortedByUpdatedDateOnce()
        {
            // Arrange
            _articleServiceMock.Setup(i => i.GetSectionItemsSortedByUpdatedDate(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<ArticleView>());

            // Act
            _browser.Get("/list/business/2020-05-12");

            // Assert
            _articleServiceMock.Verify(i => i.GetSectionItemsSortedByUpdatedDate("business", "2020-05-12"), Times.Once);
        }

        [Test]
        public void GetArticlesSortedByUpdatedDate_ShouldReturnStatusOk()
        {
            // Arrange
            _articleServiceMock.Setup(i => i.GetSectionItems(It.IsAny<string>()))
                .Returns(new List<ArticleView>());

            // Act
            BrowserResponse result = _browser.Get("/list/business/2020-05-12").Result;

            // Assert
            Assert.AreEqual(Nancy.HttpStatusCode.OK, result.StatusCode);
        }

        #endregion

        #region GetArticleByShortUrl

        [Test]
        public void GetArticleByShortUrl_CallsGetSectionItemByShortUrlOnce()
        {
            // Arrange
            _articleServiceMock.Setup(i => i.GetSectionItemByShortUrl(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ArticleView());

            // Act
            _browser.Get("/article/39Af7IB");

            // Assert
            _articleServiceMock.Verify(i => i.GetSectionItemByShortUrl("home", "39Af7IB"), Times.Once);
        }

        [Test]
        public void GetArticleByShortUrl_ShouldReturnStatusOk()
        {
            // Arrange
            _articleServiceMock.Setup(i => i.GetSectionItemByShortUrl(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ArticleView());

            // Act
            BrowserResponse result = _browser.Get("/list/39Af7IB").Result;

            // Assert
            Assert.AreEqual(Nancy.HttpStatusCode.OK, result.StatusCode);
        }

        #endregion

        #region GetArticlesGroupedByDate

        [Test]
        public void GetArticlesGroupedByDate_CallsGetSectionItemsGroupedByDateOnce()
        {
            // Arrange
            _articleServiceMock.Setup(i => i.GetSectionItemsGroupedByDate(It.IsAny<string>()))
                .Returns(new List<ArticleGroupedByDateView>());

            // Act
            _browser.Get("/group/home");

            // Assert
            _articleServiceMock.Verify(i => i.GetSectionItemsGroupedByDate("home"), Times.Once);
        }

        [Test]
        public void GetArticlesGroupedByDate_ShouldReturnStatusOk()
        {
            // Arrange
            _articleServiceMock.Setup(i => i.GetSectionItemsGroupedByDate(It.IsAny<string>()))
                .Returns(new List<ArticleGroupedByDateView>());

            // Act
            BrowserResponse result = _browser.Get("/group/home").Result;

            // Assert
            Assert.AreEqual(Nancy.HttpStatusCode.OK, result.StatusCode);
        }

        #endregion
    }
}
