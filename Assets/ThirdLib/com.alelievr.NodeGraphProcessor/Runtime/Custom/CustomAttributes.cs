using System;

namespace GraphProcessor
{
    public partial class NodeMenuItemAttribute
    {
        public NodeMenuItemAttribute(int id)
        {
            this.menuTitle = NodeGroupHelper.GetGroup(id);
        }
    }
}