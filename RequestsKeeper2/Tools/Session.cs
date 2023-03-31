using RequestsKeeper2.Models;

namespace RequestsKeeper2.Tools
{
    public class Session
    {
        static Dictionary<string, Visitor> _sessions = new Dictionary<string, Visitor>();

        public static string CreateSession(Visitor visitor)
        {
            var guid = Guid.NewGuid().ToString();
            _sessions[guid] = visitor;
            return guid;
        }

        public static Visitor GetVisitor(string guid)
        {
            if (string.IsNullOrEmpty(guid) || !_sessions.ContainsKey(guid))
            {
                return new Visitor();
            }
            else
            {
                return _sessions[guid];
            }
                
            
        }
    }
}
