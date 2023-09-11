using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebCLI.Core.Contracts;
using WebCLI.Core.Pipes;
using WebCLI.Core.Repositories;
using WebCLI.Core.Tests.Pipes;

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

            var context = new GeneralContext(null, null) { Identifier = "mock" };

            Assert.IsNull(repo[context]);
        }

        [TestMethod]
        public void Using_real_command_expect_action_from_repo()
        {
            var repo = new CommandRepository();
            repo.AddCommandDelegate("mock",
                (criteria) =>
                {
                    return new PlusOnePipe();
                });

            var context = new GeneralContext(null, null) { Identifier = "mock" };

            Assert.IsNotNull(repo[context]);
        }
    }

    internal class MockCommandCriteria : ICriteria
    {
        public string Identifier => "Mock";

        public string CommandDirective { get; set; }
        public string[] Arguments { get; set; }
        public Dictionary<string, object> Options { get; set; }

        public IPipe GetPipeline(IPipe parentPipeline)
        {
            return null;
        }
    }

    internal class MockCommandResult : ICommandResult
    {

    }
}
