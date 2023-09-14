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

            string[] split = null; 
            //data[0]["commandPipeGroups"]["1"].ToString().Split(',')[1]
            if (data != null)
            {
                if (StringExtensions.Has(data[0], "commandPipeGroups"))
                {
                    if (StringExtensions.Has(data[0]["commandPipeGroups"], "1"))
                    {
                        string items = data[0]["commandPipeGroups"]["1"].ToString();
                        items = items.Replace("\n", string.Empty);
                        items = items.Replace("\r",string.Empty);
                        items = items.Replace("\\", string.Empty);
                        items = items.Replace("]", string.Empty);
                        items = items.Replace("[", string.Empty);
                        items = items.Replace(" ", string.Empty);
                        items = items.Replace("\"", string.Empty);
                        split = items.Split(',');
                    }
                }
            }

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
