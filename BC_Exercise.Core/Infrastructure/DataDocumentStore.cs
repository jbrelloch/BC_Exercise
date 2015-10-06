using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;

namespace BC_Exercise.Core.Infrastructure
{
    public class DataDocumentStore
    {
        private static IDocumentStore _Instance;

        public static IDocumentStore Instance
        {
            get
            {
                if (_Instance == null)
                {
                    throw new InvalidOperationException("IDocumentStore has not been initialized");
                }
                return _Instance;
            }
            set
            {
                if (_Instance != null) return; // prevent misuse

                _Instance = value;
            }
        }

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <returns>The session</returns>
        public IDocumentSession GetSession()
        {
            return _Instance.OpenSession();
        }
        /// <summary>
        /// Gets the asynchronous session.
        /// </summary>
        /// <returns>The async session.</returns>
        public IAsyncDocumentSession GetAsyncSession()
        {
            return _Instance.OpenAsyncSession();
        }
    }
}
