using System.Collections.Generic;

namespace QuickSocketDemo
{
    // Normally we'd use some form of persistence to track these connectionIds, but
    // for simplicity sake, we do this in-memory for this example.
    public class Repository : IRepository
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
