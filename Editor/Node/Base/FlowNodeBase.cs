using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{
    public abstract class FlowNodeBase : BaseNode
    {
        /// <summary>
        /// 节点顺序
        /// </summary>
        [HideInInspector]
        public int NodeOrder = -1;
        
        /// <summary>
        /// 流程节点
        /// </summary>
        [HideInInspector]
        public FlowType FlowType = FlowType.None;
        
        /// <summary>
        /// 节点类型
        /// </summary>
        public virtual FlowNodeType Type { get; }

        /// <summary>
        /// 服务器节点标记
        /// </summary>
        public virtual bool ServerNode { get; }
        
        /// <summary>
        /// 是否需要上传参数
        /// </summary>
        public virtual bool TansferParameter { get; }


        public virtual void UpdateForExport(){}

        public virtual bool CheckForExport()
        {
            return true;
        }
    }
}