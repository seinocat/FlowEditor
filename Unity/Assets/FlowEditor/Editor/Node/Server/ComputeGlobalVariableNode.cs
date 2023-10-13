using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem((int)FlowNodeType.ComputeGlobalVariable), GameEventNode, System.Serializable]
    public class ComputeGlobalVariableNode : ServerNodeBase
    {
        public override string name => NodeGroupHelper.GetName(Type.GetHashCode());
        
        public override FlowNodeType Type => FlowNodeType.ComputeGlobalVariable;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;

        [CustomSetting("全局变量名")] 
        public string GlobalKey;
        
        [CustomSetting("值")] 
        public int GlobalValue;
        
        [CustomSetting("运算类型"), Reference(typeof(int), nameof(ComputeType))] 
        public ComputeType ComputeType;
    }
}