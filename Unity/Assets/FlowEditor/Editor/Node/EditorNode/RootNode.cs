using System;
using GraphProcessor;
using SeinoCat.FlowEditor.Runtime;

namespace SeinoCat.FlowEditor.Editor
{
    [NodeMenuItem("配置/根节点"), FlowNode, Serializable]
    public class RootNode : EditorNodeBase
    {
        public override string name => "根节点";
        
        [Output("Root")]
        public EditorPort Output;
    }
}