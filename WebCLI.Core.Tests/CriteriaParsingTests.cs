using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCLI.Application;
using WebCLI.Core.Pipes;
using WebCLI.Core.Repositories;

namespace WebCLI.Core.Tests
{
    [TestClass]
    public class CriteriaParsingTests
    {
        [TestMethod]
        public async Task parse_criteria_object_of_type_single()
        {
            // instantiate a command loader, and the repository, which //
            // will parse all the JSON files and build out the data //
            var commands = new CommandRepository();

            var location = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location);

            var initializer = new CommandQueryLoader(
                new List<string> { $"{location}\\Data\\scenario1.json" }, commands, new QueryRepository());

            await initializer.BuildOutCommandRepository();

            var pipe = commands[new GeneralContext(null, null) { Identifier = "addone" }];
        }
    }
}
