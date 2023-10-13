using GraphProcessor;
using FlowEditor.Runtime;
using UnityEngine;

namespace FlowEditor.Editor
{
    public class FlowGraphBase : BaseGraph
    {
        public long Timestamp;
        public int OpenCount;
        public FlowGraphWindow m_Window;
        
        public FlowGraphBase()
        {
            this.Timestamp = TimeHelper.GetTimestamp();
        }
        
        public bool ComputeGraphOrder()
        {
            foreach (var node in nodes)
            {
                (node as FlowNodeBase).NodeOrder = -1;
                (node as FlowNodeBase).FlowType = FlowType.None;
            }
            
            foreach (var node in nodes)
            {
                if (node is EventFlagNode eventFlag)
                {
                    if (eventFlag.GetOutputNodeList().Count > 0)
                    {
                        var startNode = eventFlag.GetOutputNodeList()[0];
                        if (startNode is StartNode initNode)
                        {
                            int order = 1;
                            initNode.NodeOrder = order;
                            initNode.FlowType = FlowType.Event;
                            if (!ComputeNodeOrder(initNode, ref order, FlowType.Event))
                            {
                                return false;
                            }
                        }
                    }
                   
                }
                else if (node is InteractItemNode)
                {
                    foreach (var output in node.GetOutputNodeList())
                    {
                        var outputNode = output as FlowNodeBase;
                        if (outputNode is not InteractItemNode)
                        {
                            int order = 1;
                            outputNode.NodeOrder = order;
                            outputNode.FlowType = FlowType.Interact;
                            if (!ComputeNodeOrder(outputNode, ref order, FlowType.Interact))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
        
            return true;
        }
        
        private bool ComputeNodeOrder(FlowNodeBase nodeBase, ref int order, FlowType type)
        {
            foreach (var output in nodeBase.GetOutputNodeList())
            {
                var outputNode = output as FlowNodeBase;
                outputNode.NodeOrder = ++order;
                if (outputNode.FlowType != FlowType.None && outputNode.FlowType != type)
                {
                    Debug.LogError($"事件配置{name}含有流程交叉，请检查配置！");
                    return false;
                }
                outputNode.FlowType = type;
                if (outputNode.GetOutputNodeList().Count > 0)
                {
                    ComputeNodeOrder(outputNode, ref order, type);
                }
            }
        
            return true;
        }
    }
}