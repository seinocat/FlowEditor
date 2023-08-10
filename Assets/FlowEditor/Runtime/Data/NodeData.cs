using System;
using System.Collections.Generic;
using UnityEngine;

namespace FlowEditor.Runtime
{
    [Serializable]
    public class NodeTypeData
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName;
        /// <summary>
        /// 节点类型ID
        /// </summary>
        public int NodeID;
        /// <summary>
        /// 是否需要外部传参数
        /// </summary>
        public bool TansferParameter;
        /// <summary>
        /// 节点类型
        /// </summary>
        public List<Parameter> Parameters  = new List<Parameter>();
    }
    
    [Serializable]
    public class Parameter
    {
        /// <summary>
        /// 参数数据类型名称
        /// </summary>
        public string Type;
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName;
        /// <summary>
        /// List泛型类型
        /// </summary>
        public string ListType;
    }
    
    [Serializable]
    public class FlowGraphData
    {
        public List<FlowData> AgentsList = new List<FlowData>();
    }
    
    [Serializable]
    public class FlowData
    {
        /// <summary>
        /// 事件列表
        /// </summary>
        public List<GameEventData> EventList = new List<GameEventData>();
        
        /// <summary>
        /// 交互列表
        /// </summary>
        public List<GameInteractData> InteractList = new List<GameInteractData>();

        /// <summary>
        /// 对话列表
        /// </summary>
        public List<int> DialogList = new List<int>();

        /// <summary>
        /// 交互条件
        /// </summary>
        public List<int> InteractConditions = new List<int>();
    }

    [Serializable]
    public class GameInteractData
    {
        /// <summary>
        /// 触发状态
        /// </summary>
        public AgentState State;
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority;
        /// <summary>
        /// Icon
        /// </summary>
        public string Icon;
        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text;
        /// <summary>
        /// 条件组-条件表ID
        /// </summary>
        public List<int> Conditions;
        /// <summary>
        /// 子交互选项
        /// </summary>
        public List<GameInteractData> NextInteractList = new List<GameInteractData>();
        /// <summary>
        /// 节点列表
        /// </summary>
        public List<NodeData> NodeList = new List<NodeData>();
    }
    
    [Serializable]
    public class GameEventData
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public string EventName;
        /// <summary>
        /// 事件ID
        /// </summary>
        public int EventID;
        /// <summary>
        /// 节点开始ID
        /// </summary>
        public int StartID;
        /// <summary>
        /// 事件优先级
        /// </summary>
        public int Priority;
        /// <summary>
        /// 事件触发类型 0-自动触发 1-手动触发 2-进入触发器触发
        /// </summary>
        public int TriggerType = 1;
        /// <summary>
        /// 可触发状态
        /// </summary>
        public int AgentState;
        /// <summary>
        /// Icon
        /// </summary>
        public string Icon;
        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text;
        /// <summary>
        /// 条件组-条件表ID
        /// </summary>
        public List<int> Conditions;
        /// <summary>
        /// 节点列表
        /// </summary>
        public List<NodeData> NodeList = new List<NodeData>();
        [HideInInspector]
        public string GraphName;
    }
    
    [Serializable]
    public class NodeData
    {
        /// <summary>
        /// 步骤ID
        /// </summary>
        public int ID;
        /// <summary>
        /// 上一步骤ID
        /// </summary>
        public List<int> LastID = new List<int>();
        /// <summary>
        /// 下一步骤ID
        /// </summary>
        public List<int> NextID = new List<int>();
        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName;
        /// <summary>
        /// 节点类型ID(服务器用)
        /// </summary>
        public int NodeID;
        /// <summary>
        /// 节点类型(客户端用)
        /// </summary>
        public FlowNodeType NodeType;
        /// <summary>
        /// 节点参数
        /// </summary>
        public JsonNodeData Parameters;
    }
}