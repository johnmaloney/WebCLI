using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCLI.Core.Contracts;
using WebCLI.Core.Repositories;

namespace WebCLI.Core.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void Add_commands_to_repository_expect_list()
        {
            var repo = new CommandRepository();
            repo.AddCommandDelegate("mock",
                (criteria) =>
                {
                    return null;
                });

            var criteria = new MockCommandCriteria();

            Assert.IsNull(repo[criteria]);
        }

        [TestMethod]
        public void Using_real_command_expect_action_from_repo()
        {
            var repo = new CommandRepository();
            repo.AddCommandDelegate("mock",
                (criteria) =>
                {
                    return new MockCommandResult();
                });

            var criteria = new MockCommandCriteria();

            Assert.IsNotNull(repo[criteria]);
        }
    }

    internal class MockCommandCriteria : ICommandCriteria
    {
        public string Name => "Mock";
    }

    internal class MockCommandResult : ICommandResult
    {

    }
}
