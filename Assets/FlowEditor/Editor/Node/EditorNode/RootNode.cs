using System;
using FlowEditor.Runtime;
using GraphProcessor;
namespace FlowEditor.Editor
{
    [NodeMenuItem("配置/根节点"), GameEventNode, Serializable]
    public class RootNode : EditorNodeBase
    {
        public override string name => "根节点";
        
        [Output("Root")]
        public EditorPort Output;
    }
}