using FuzzySearch.Core.Models;
using FuzzySearch.Core.Models.Config;
using FuzzySearch.Service;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzySearch.Search.UnitTests
{
    [TestFixture]
    class FuzzySearchServiceTests
    {
        private Mock<IAzureDocDatabase> _mockDbContext;
        private Mock<IOptions<DocumentDbConfig>> _mockOption;
        private Mock<IFuzzySearchHelper> _mockSearchHelper;
        private FuzzySearchService _searchService;


        [SetUp]
        public void Initialize()
        {
            _mockDbContext = new Mock<IAzureDocDatabase>(MockBehavior.Default);
            _mockOption = new Mock<IOptions<DocumentDbConfig>>(MockBehavior.Default);
            _mockOption.Setup(opt => opt.Value).Returns(new DocumentDbConfig());
            _mockSearchHelper = new Mock<IFuzzySearchHelper>(MockBehavior.Default);
            _searchService = new FuzzySearchService(_mockOption.Object, _mockDbContext.Object, _mockSearchHelper.Object);
        }

        [Test]
        public void Constructor_OptionsNull_ThrowsArgumentNullException()
        {
            //arrange
            Assert.Throws<ArgumentNullException>(() => {
                new FuzzySearchService(null, _mockDbContext.Object, _mockSearchHelper.Object);
            });
        }

        [Test]
        public void SearchAccount_ValidRequest_ReturnsMatchingAccounts()
        {
            //arrange
            var requestedAccount = "Wal";
            var availableAcounts = new List<AccountModel>()
            {
                new AccountModel() { Name = "Walmart" },
                new AccountModel() { Name = "Target" },
                new AccountModel() { Name = "Wal-mart" },
                new AccountModel() { Name = "Firestone" }
            };
            var expectedResult = new List<AccountModel>()
            {
                new AccountModel() { Name = "Walmart" },
                new AccountModel() { Name = "Wal-mart" }
            };

            _mockDbContext.Setup(db => db.GetAccounts()).Returns(availableAcounts);

            _mockSearchHelper.Setup(srch => srch.FindMatchingAccounts(requestedAccount, It.IsAny<IEnumerable<AccountModel>>())).Returns(expectedResult);

            //act
            var actualResult = _searchService.SearchAccount(new SearchRequestModel() { Name = requestedAccount });

            //assert
            Assert.AreEqual(2, actualResult.MatchingAccounts.Count());
        }


    }
}
