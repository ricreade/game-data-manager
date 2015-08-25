using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeDataImpl
{
    /// <summary>
    /// Class for managing all game states.
    /// </summary>
    public sealed class PrototypeGameStates
    {
        private static ConcurrentDictionary<string, PrototypeDataLayer> _states;
        private static volatile PrototypeGameStates _inst;
        private static object _synclock = new Object();

        private PrototypeGameStates()
        {
            _states = new ConcurrentDictionary<string, PrototypeDataLayer>();
        }

        /// <summary>
        /// Retrieves the singleton instance of the game state manager,
        /// or creates a new instance if it does not exist.
        /// </summary>
        public static PrototypeGameStates Instance
        {
            get
            {
                if (_inst == null)
                {
                    lock (_synclock)
                    {
                        if (_inst == null)
                            _inst = new PrototypeGameStates();
                    }
                }
                return _inst;
            }
        }

        /// <summary>
        /// Retrieves a data layer from the registry using the specified id.
        /// If a layer cannot be found that references the specified id, this method
        /// returns null.
        /// </summary>
        /// <param name="ID">The string ID of the layer to retrieve.</param>
        /// <returns>The requested PrototypeDataLayer.</returns>
        public PrototypeDataLayer GetLayer(string ID)
        {
            PrototypeDataLayer layer;
            if (ID == null || ID.Length == 0)
                return null;
            if (_states.TryGetValue(ID, out layer))
            {
                return layer;
            }
            return null;
        }

        /// <summary>
        /// Adds a data layer to the game state registry.  If the layer ID already
        /// exists, or the layer argument is null, or the layer ID is empty or null
        /// the registration fails.
        /// </summary>
        /// <param name="Layer">The data layer to register.</param>
        /// <returns>True if the registration was successful.</returns>
        public bool RegisterLayer(PrototypeDataLayer Layer){
            if (Layer != null || Layer.ID == null || Layer.ID.Length > 0)
            {
                if (!_states.ContainsKey(Layer.ID))
                {
                    lock (_synclock)
                    {
                        if (!_states.ContainsKey(Layer.ID))
                        {
                            return _states.TryAdd(Layer.ID, Layer);
                        }
                    }
                }
            }
            return false;
        }
    }
}
