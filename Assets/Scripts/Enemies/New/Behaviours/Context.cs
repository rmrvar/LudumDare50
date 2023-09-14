using System.Collections.Generic;

namespace Ai
{
    public class Context
    {
        private static int _idCounter = 0;
        public int Id { get; private set; }

        private List<LeafNode> _nodesToProcess;

        private Dictionary<object, object> _blackboard;

        public Context()
        {
            _nodesToProcess = new List<LeafNode>();
            _blackboard = new Dictionary<object, object>();

            Id = ++_idCounter;
        }

        public bool HasStarted { get; set; }

        #region --- Tree data section ---
        public bool Has(string key)
        {
            return _blackboard.ContainsKey(key);
        }

        public T Get<T>(string key)
        {
            return (T)_blackboard[key];
        }

        public void Set<T>(string key, T value)
        {
            _blackboard[key] = value;
        }

        public void Unset(string key)
        {
            _blackboard.Remove(key);
        }
        #endregion

        #region --- Node data section ---
        public bool HasNodeValue(Node node, string key)
        {
            if (!_blackboard.ContainsKey(node))
            {
                return false;
            }

            var nodeData = (Dictionary<string, object>)_blackboard[node];

            return nodeData.ContainsKey(key);
        }

        public T GetNodeValue<T>(Node node, string key)
        {
            var nodeData = (Dictionary<string, object>)_blackboard[node];
            return (T)nodeData[key];
        }

        public void SetNodeValue<T>(Node node, string key, T value)
        {
            Dictionary<string, object> nodeData;

            if (!_blackboard.ContainsKey(node))
            {
                nodeData = new Dictionary<string, object>();
                _blackboard[node] = nodeData;
            }
            else
            {
                nodeData = (Dictionary<string, object>)_blackboard[node];
            }

            nodeData[key] = value;
        }

        public void UnsetNodeValue(Node node, string key)
        {
            if (_blackboard.TryGetValue(node, out var dictObject))
            {
                var dict = (Dictionary<string, object>)dictObject;
                dict.Remove(key);
            }
        }
        #endregion

        public void StartProcessingNode(LeafNode node)
        {
            _nodesToProcess.Add(node);
        }

        public void StopProcessingNode(LeafNode node)
        {
            _nodesToProcess.Remove(node);
        }

        public IEnumerable<LeafNode> NodesToProcess()
        {
            return _nodesToProcess;
        }
    }
}
