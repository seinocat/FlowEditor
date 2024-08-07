﻿using GraphProcessor;
using SeinoCat.FlowEditor.Runtime;

namespace SeinoCat.FlowEditor.Editor
{
    [NodeMenuItem((int)FlowNodeType.SetGlobalVariable), FlowNode, System.Serializable]
    public class SetGlobalVariableNode : ServerNodeBase
    {
        public override string name => NodeGroupHelper.GetName(Type.GetHashCode());
        
        public override FlowNodeType Type => FlowNodeType.SetGlobalVariable;
        
        [Input("In")]
        public FlowNodePort Input;
        
        [Output("Out", false)]
        public FlowNodePort Output;

        [CustomSetting("全局变量名")] 
        public string GlobalKey;
        
        [CustomSetting("全局变量值")] 
        public int GlobalValue;
    }
}