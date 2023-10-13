using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem((int)FlowNodeType.SetGlobalVariable), GameEventNode, System.Serializable]
    public class SetGlobalVariableNode : ServerNodeBase
    {
        public override string name => NodeGroupHelper.GetName(Type.GetHashCode());
        
        public override FlowNodeType Type => FlowNodeType.SetGlobalVariable;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;

        [CustomSetting("全局变量名")] 
        public string GlobalKey;
        
        [CustomSetting("全局变量值")] 
        public int GlobalValue;
    }
}