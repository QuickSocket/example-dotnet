using System.Collections.Generic;

namespace QuickSocketDemo
{
    // This class is used to simulate your database layer
    // in this instance it is used to store a list of connections
    // to relay new incoming messages to.
    public class Store : IStore
    {
        private List<string> _connectionIds = new List<string>();

        public List<string> GetConnectionIds()
        {
            return _connectionIds;
        }
        public void AddToConnectionIds(string connectionId)
        {
            _connectionIds.Add(connectionId);
        }
        public void RemoveFromConnectionIds(string connectionid)
        {
            _connectionIds.Remove(connectionid);
        }
    }
}
