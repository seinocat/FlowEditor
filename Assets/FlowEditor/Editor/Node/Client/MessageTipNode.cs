using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{
    [NodeMenuItem("交互/消息提示"), GameEventNode, System.Serializable]
    public class MessageTipNode : ClientNodeBase
    {
        public override string name => "消息提示";
        
        public override FlowNodeType Type => FlowNodeType.MessageTip;
        
        [Input("In", true)]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;

        [CustomSetting("消息内容"), TextArea] 
        public string Content;
        
        
    }
}