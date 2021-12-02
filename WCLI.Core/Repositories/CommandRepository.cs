using System;
using System.Collections.Concurrent;
using WebCLI.Core.Contracts;

namespace WebCLI.Core.Repositories
{
    public class CommandRepository : ICommandRepository
    {
        #region Fields

        private readonly ConcurrentDictionary<string, Func<ICommandCriteria, ICommandResult>> commandActions;

        #endregion

        #region Properties

        public ICommandResult this[ICommandCriteria criteria]
        {
            get
            {
                return this.commandActions[criteria.Name.ToLowerInvariant()](criteria);
            }
        }

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
