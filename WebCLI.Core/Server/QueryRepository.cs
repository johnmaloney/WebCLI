using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCLI.Core.Server.Interfaces;

namespace WebCLI.Core.Server
{
    public class QueryRepository
    {
        #region Fields

        #endregion

        #region Properties

        public ConcurrentDictionary<string, Func<IQueryCriteria, IQueryResult>> queryActions;

        #endregion

        #region Methods

        public QueryRepository()
        {
            queryActions = new ConcurrentDictionary<string, Func<IQueryCriteria, IQueryResult>>();
        }

        public void AddQueryDelegate(string queryName, Func<IQueryCriteria, IQueryResult> actionDelegate)
        {
            queryActions.AddOrUpdate(queryName, actionDelegate, (cmdName, action) => action = actionDelegate);
        }

        #endregion
    }
}
