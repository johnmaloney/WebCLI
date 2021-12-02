using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCLI.Core.Contracts
{
    public interface ICommandRepository
    {
        ICommandResult this[ICommandCriteria criteria] { get; }
        void AddCommandDelegate(string commandName, Func<ICommandCriteria, ICommandResult> actionDelegate);
    }
}
