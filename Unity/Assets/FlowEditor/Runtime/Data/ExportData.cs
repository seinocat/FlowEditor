using System;
using System.Collections.Generic;
using UnityEngine;

namespace FlowEditor.Runtime
{
    [Serializable, ExportData]
    public class BranchData
    {
        public List<int> Conditions;
        [HideInInspector]
        public int NextID;
    }
    
    [Serializable, ExportData]
    public class ItemData
    {
        public int ItemID;
        public int ItemCount;
    }
    
    [Serializable, ExportData]
    public class RandomBranchData
    {
        public int Weight;
        [HideInInspector]
        public int NextID;
    }
}

