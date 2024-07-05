using System.Collections.Generic;
using GraphProcessor;
using SeinoCat.FlowEditor.Runtime;
using UnityEngine;

namespace SeinoCat.FlowEditor.Editor
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
        
        public List<T> GetNode<T>() where T : FlowNodeBase
        {
            List<T> list = new List<T>();
            foreach (var node in this.nodes)
            {
                if (node is T @base)
                {
                    list.Add(@base);
                }
            }

            return list;
        }
        
        public bool ComputeGraphOrder()
        {
            ResetNode();
            int flowId = 0;
            
            foreach (var node in nodes)
            {
                if (node is EventFlagNode eventFlag)
                {
                    eventFlag.FlowType = FlowType.Event;
                    if (eventFlag.GetOutputNodeList().Count > 0)
                    {
                        var startNode = eventFlag.GetOutputNodeList()[0];
                        if (startNode is StartNode initNode)
                        {
                            int order = 1;
                            eventFlag.FlowId = ++flowId;
                            initNode.FlowId = flowId;
                            initNode.NodeOrder = order;
                            initNode.FlowType = FlowType.Event;
                            if (!ComputeNodeOrder(initNode, ref order, FlowType.Event, flowId))
                            {
                                return false;
                            }
                        }
                    }
                   
                }
                else if (node is InteractItemNode interactItemNode)
                {
                    interactItemNode.FlowType = FlowType.Interact;
                    foreach (var output in node.GetOutputNodeList())
                    {
                        var outputNode = output as FlowNodeBase;
                        if (outputNode is not InteractItemNode)
                        {
                            int order = 1;
                            interactItemNode.FlowId = ++flowId;
                            outputNode.NodeOrder = order;
                            outputNode.FlowId = flowId;
                            outputNode.FlowType = FlowType.Interact;
                            if (!ComputeNodeOrder(outputNode, ref order, FlowType.Interact, flowId))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
        
            return true;
        }

        private bool ComputeNodeOrder(FlowNodeBase nodeBase, ref int order, FlowType type, int flowId)
        {
            foreach (var output in nodeBase.GetOutputNodeList())
            {
                if (!nodes.Contains(output))
                    continue;
                
                var outputNode = output as FlowNodeBase;
                if (outputNode is EndNode)
                {
                    outputNode.NodeOrder = 9999;
                    outputNode.FlowId = -1;
                }
                else
                {
                    outputNode.NodeOrder = ++order;
                    outputNode.FlowId = flowId;
                }
                
                if (outputNode.FlowType != FlowType.None && outputNode.FlowType != type)
                {
                    Debug.LogError($"事件配置{name}，含有流程交叉，请检查配置！");
                    return false;
                }

                if (type == FlowType.Interact && outputNode is ServerNodeBase)
                {
                    Debug.LogError($"事件配置{name}，交互流程中含有服务器节点，请检查配置！");
                    return false;
                }
                
                outputNode.FlowType = type;
                if (outputNode.GetOutputNodeList().Count > 0)
                {
                    ComputeNodeOrder(outputNode, ref order, type, flowId);
                }
            }
        
            return true;
        }
        
        private void ResetNode()
        {
            foreach (var node in nodes)
            {
                var nodebase = node as FlowNodeBase;
                nodebase.FlowId = -1;
                nodebase.FlowType = FlowType.None;
            }
        }
        
        private bool CheckNode()
        {
            foreach (var node in nodes)
            {
                var nodebase = node as FlowNodeBase;
                if (nodebase.FlowType == FlowType.None)
                {
                    Debug.LogError($"事件配置{name}，连线错误或含有孤立节点，请检查配置！");
                    return false;
                }
            }

            return true;
        }
    }
}