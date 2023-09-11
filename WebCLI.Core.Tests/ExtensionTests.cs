using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCLI.Core.Contracts;

namespace WebCLI.Core.Tests
{
    [TestClass]
    public class ExtensionTests
    {
        [TestMethod]
        [DataRow("")]
        [DataRow("WebCLI.Core.Tests, WebCLI.Core.Tests.Pipes.PlusOnePipe")]
        [DataRow("Assembly.Incorrect, ClassName.Incorrect")]
        public void Given_AssemblyName_expect_initialized_Type(string assemblyName)
        {
            IPipe command;
            try
            {
                 command = assemblyName.InstantiateObject<IPipe>();
                Assert.IsInstanceOfType(command, typeof(IPipe));
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(NotSupportedException));
            }
        }
    }
}
