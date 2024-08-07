﻿using GraphProcessor;
using SeinoCat.FlowEditor.Runtime;

namespace SeinoCat.FlowEditor.Editor
{
    [NodeMenuItem((int)FlowNodeType.GetGlobalVariable), FlowNode, System.Serializable]
    public class GetGlobalVariableNode : ClientNodeBase
    {
        public override string name => NodeGroupHelper.GetName(Type.GetHashCode());

        public override FlowNodeType Type => FlowNodeType.GetGlobalVariable;
        
        [Input("In")]
        public FlowNodePort Input;
        
        [Output("Out", false)]
        public FlowNodePort Output;
        
        [CustomSetting("全局变量名")] 
        public string GlobalKey;
    }
}