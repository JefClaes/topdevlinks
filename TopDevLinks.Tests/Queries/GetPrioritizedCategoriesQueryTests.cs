using System.Linq;
using NUnit.Framework;
using TopDevLinks.Models.Entities;
using TopDevLinks.Queries;

namespace TopDevLinks.Tests.Queries
{
    public class GetPrioritizedCategoriesQueryTests : QueryTestFixture
    {
        private Category _category1 = new Category {Priority = 2};
        private Category _category2 = new Category {Priority = 1};
        private Category _category3 = new Category {Priority = 3};

        [SetUp] 
        public void SetUp()
        {
            MongoContext.GetCollection<Category>().Drop();
            EntityStore.Save(_category1);
            EntityStore.Save(_category2);
            EntityStore.Save(_category3);
        }

        [Test]
        public void Returned_Categories_Are_Prioritized()
        {
            var categories = Execute(new GetPrioritizedCategoriesQuery());
            Assert.AreEqual(_category3.Id, categories.ElementAt(0).Id);
            Assert.AreEqual(_category1.Id, categories.ElementAt(1).Id);
            Assert.AreEqual(_category2.Id, categories.ElementAt(2).Id);
        }
    }
}