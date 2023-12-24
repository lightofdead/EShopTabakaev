using DataBase.Repositories;

namespace Test
{
    public class Tests
    {
        private CategoriesRepository _repoCategories = new CategoriesRepository();
        private ProductsRepository _repoProducts = new ProductsRepository();
        private ProductsForBuyRepository _repoBuy = new ProductsForBuyRepository();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            var result = (await _repoCategories.GetModels()).ToList().Count;
            Assert.AreNotEqual(result, 0);
        }

        [Test]
        public async Task Test2()
        {
            var result = (await _repoProducts.GetModels()).ToList().Count;
            Assert.AreNotEqual(result, 0);
        }

        [Test]
        public async Task Test3()
        {
            var result = (await _repoBuy.GetModels()).ToList().Count;
            Assert.AreNotEqual(result, 0);
        }
    }
}