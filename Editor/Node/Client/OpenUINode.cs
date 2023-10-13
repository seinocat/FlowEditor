using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem((int)FlowNodeType.OpenUI), GameEventNode, System.Serializable]
    public class OpenUINode : ClientNodeBase
    {
        public override string name => NodeGroupHelper.GetName(Type.GetHashCode());
        
        public override FlowNodeType Type => FlowNodeType.OpenUI;
        
        [Input("In")]
        public FlowNodePort Input;
        
        [Output("Out", false)]
        public FlowNodePort Output;
        
        [CustomSetting("UI类型")]
        public OpenUIType UIType;

        [CustomSetting("UI名称")] 
        public string UIName;

        [CustomSetting("参数")] 
        public string UIParams;
        
        [CustomSetting("打开UI立即完成")] 
        public bool ImmediatelyComplete;
    }
}