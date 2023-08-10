using System;
using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{

    [NodeMenuItem("逻辑/完成回执"), GameEventNode, Serializable]
    public class CompleteReceiptNode : ClientNodeBase
    {
        public override string name => "完成回执";

        public override FlowNodeType Type => FlowNodeType.CompleteReceipt;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("True")]
        public EventNodePort Output;
        
        [Output("False", false)] 
        public EventNodePort Output2;
        
        [CustomSetting("成功节点ID"), HideInInspector]
        public int SuccessID;
        [CustomSetting("失败节点ID"), HideInInspector]
        public int FailID;
        
        public CompleteReceiptNode()
        {
            onAfterEdgeConnected += OnEdgeChange;
            onAfterEdgeDisconnected += OnEdgeChange;
        }
        
        private void OnEdgeChange(SerializableEdge edge)
        {
            this.graph.UpdateComputeOrder();
            foreach (var port in this.outputPorts)
            {
                switch (port.fieldName)
                {
                    case "Output":
                        this.SetNextNode(true, port);
                        break;
                    case "Output2":
                        this.SetNextNode(false, port);
                        break;
                }
            }
        }

        private void SetNextNode(bool isSuccess, NodePort port)
        {
            if (port.GetEdges().Count > 0)
            {
                if (isSuccess)
                {
                    this.SuccessID = port.GetEdges()[0].inputNode.computeOrder;
                }
                else
                {
                    this.FailID = port.GetEdges()[0].inputNode.computeOrder;
                }
                 
            }
            else
            {
                if (isSuccess)
                {
                    this.SuccessID = -1;
                }
                else
                {
                    this.FailID = -1;
                }
            }
        }
    }
}