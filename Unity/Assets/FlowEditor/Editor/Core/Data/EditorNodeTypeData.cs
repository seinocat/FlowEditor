using System;
using SeinoCat.FlowEditor.Runtime;
using Sirenix.OdinInspector;

namespace SeinoCat.FlowEditor.Editor
{
    [Serializable]
    public class EditorNodeTypeBase
    {
        [ReadOnly]
        public string name;
        [NonSerialized]
        public FlowNodeType type;
    }
    

    [Serializable]
    public class EditorNodeTypeData : EditorNodeTypeBase
    {
        public int value;
        public string desc;
    }
    
    [Serializable]
    public class EditorNetNodeTypeData : EditorNodeTypeBase
    {
        [ReadOnly]
        public int value;
        public string desc;
    }
}