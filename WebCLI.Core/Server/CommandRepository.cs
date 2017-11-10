using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCLI.Core.Server.Interfaces;

namespace WebCLI.Core.Server
{
    public class CommandRepository
    {
        #region Fields
       
        #endregion

        #region Properties

        public ConcurrentDictionary<string, Func<ICommandCriteria, ICommandResult>> commandActions;

        #endregion

        #region Methods

        public CommandRepository()
        {
            commandActions = new ConcurrentDictionary<string, Func<ICommandCriteria, ICommandResult>>();
        }

        public void AddCommandDelegate(string commandName, Func<ICommandCriteria, ICommandResult> actionDelegate)
        {
            commandActions.AddOrUpdate(commandName, actionDelegate, (cmdName, action) => action = actionDelegate);
        }

        #endregion
    }
}
