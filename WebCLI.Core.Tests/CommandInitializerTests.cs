using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCLI.Application;
using WebCLI.Core.Models;
using WebCLI.Core.Pipes;
using WebCLI.Core.Repositories;
using WebCLI.Core.Tests.Base;

namespace WebCLI.Core.Tests
{
    [TestClass]
    public class CommandInitializerTests
    {
        [TestMethod]
        public void Given_folder_find_location_expect_deserialized_object()
        {
            var deserializer = new DeserializerStrategy("\\Data\\");
            var data = deserializer.Into<dynamic>("scenario1");

            var split = ((string)data["commandPipeType"]).Split(',');
            var plus = Activator.CreateInstance(split[0], split[1]);
            var instance = plus.Unwrap();
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public async Task Given_JSON_Command_Expect_Objects()
        {
            // instantiate a command loader, and the repository, which //
            // will parse all the JSON files and build out the data //
            var commands = new CommandRepository();

            var location = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var initializer = new CommandQueryLoader(new List<string> { $"{location}\\Data\\" }, commands, new QueryRepository());
            await initializer.BuildOutCommandRepository();

            var pipe = commands[new GeneralContext(null, null) { Identifier = "addone" }];

            Assert.IsNotNull(pipe);
        }

        [TestMethod]
        public async Task Given_multiple_pipes_in_one_expect_pipeline_build_out()
        {
            // instantiate a command loader, and the repository, which //
            // will parse all the JSON files and build out the data //
            var commands = new CommandRepository();

            var location = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location);

            var initializer = new CommandQueryLoader(
                new List<string> { $"{location}\\Data\\" }, commands, new QueryRepository());

            await initializer.BuildOutCommandRepository();

            var pipe = commands[new GeneralContext(null, null) { Identifier = "addonedouble" }];
        }
    }
}
