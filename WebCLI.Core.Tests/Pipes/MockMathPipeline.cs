using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCLI.Core.Contracts;
using WebCLI.Core.Pipes;

namespace WebCLI.Core.Tests.Pipes
{
    public class PlusOnePipe : APipe, IPipe
    {
        public override async Task Process(IContext context)
        {
            while (this.HasNextPipe)
                await this.NextPipe.Process(context);
        }
    }

    public class MinusOnePipe : APipe, IPipe
    {
        public override async Task Process(IContext context)
        {
            while (this.HasNextPipe)
                await this.NextPipe.Process(context);
        }
    }

    public class DoubleValuePipe : APipe, IPipe
    {
        public override async Task Process(IContext context)
        {
            while (this.HasNextPipe)
                await this.NextPipe.Process(context);
        }
    }
}
