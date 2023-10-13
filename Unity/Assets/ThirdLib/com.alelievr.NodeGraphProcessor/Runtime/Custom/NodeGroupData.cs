using System;
using System.Collections.Generic;

namespace GraphProcessor
{
    [Serializable]
    public class NodeGroupData
    {
        public int ID;
        public string Group;
        public string Name;
    }

    [Serializable]
    public class NodeGroup
    {
        public List<NodeGroupData> NodeConfig = new ();
    }
}