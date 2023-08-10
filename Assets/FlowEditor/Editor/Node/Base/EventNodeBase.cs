using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    public abstract class EventNodeBase : BaseNode
    {
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
        
        /// <summary>
        /// 不需要向服务器
        /// </summary>
        // public virtual bool TansferParameter { get; }
        

        public virtual void UpdateForExport(){}

        public virtual bool CheckForExport()
        {
            return true;
        }
    }
}