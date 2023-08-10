using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("单位/创建单位"), GameEventNode, System.Serializable]
    public class CreateUnitNode : ServerNodeBase
    {
        public override string name => "创建单位";
        
        public override FlowNodeType Type => FlowNodeType.CreateUnit;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;

        [CustomSetting("怪物ID")]
        public int MonsterID;
    }
}