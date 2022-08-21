using System.Collections.Generic;

namespace QuickSocketDemo
{
    public interface IRepository
    {
        void AddToConnectionIds(string connectionId);
        List<string> GetConnectionIds();
        void RemoveFromConnectionIds(string connectionId);
    }
}
