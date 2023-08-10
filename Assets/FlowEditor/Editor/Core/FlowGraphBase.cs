using GraphProcessor;
using FlowEditor.Runtime;

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
    }
}