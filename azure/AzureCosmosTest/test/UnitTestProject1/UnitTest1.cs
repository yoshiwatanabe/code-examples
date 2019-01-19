using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using UnitTestProject1;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }

        Repository repository;

        [TestInitialize()]
        public void Initialize()
        {
            string endpoint = TestContext.Properties["endpoint"].ToString();
            string authKey = TestContext.Properties["authKey"].ToString();
            repository = new Repository(endpoint, authKey);
        }

        [TestCleanup]
        public void Cleanup()
        {
            repository.CleanAll().GetAwaiter().GetResult();
            repository.Dispose();
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            var partitionKey = Guid.NewGuid().ToString();
            await repository.Add(new Item { PartitionKey = partitionKey, Id = "101", Todo = "Eat dinner", Completed = false });
            var item = await repository.Get(partitionKey, "101");

            Assert.AreEqual("Eat dinner", item.Todo);
        }
    }
}
