using System.Collections.Generic;

namespace QuickSocketDemo
{
    public interface IStore
    {
        void AddToConnectionIds(string connectionId);
        List<string> GetConnectionIds();
        void RemoveFromConnectionIds(string connectionId);
    }
}
