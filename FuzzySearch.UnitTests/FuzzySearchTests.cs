using FuzzySearch.Service;
using NUnit.Framework;
using Moq;
using FuzzySearch.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace FuzzySearch.UnitTests
{
    [TestFixture]
    public class FuzzySearchHelperTests
    {
        [Test]
        public void AccountNameExactMatch_ReturnsMatchingAccounts()
        {
            //arrange
            var accounts = new List<AccountModel>()
            {
                new AccountModel() { Name = "Walmart" },
                new AccountModel() { Name = "Target" },
                new AccountModel() { Name = "Wal-mart" },
                new AccountModel() { Name = "Firestone" }
            };


            //act
            var results = new FuzzySearchHelper().FindMatchingAccounts("Wal", accounts);

            //assert
            Assert.AreEqual(2, results.Count());            
        }
    }
}
