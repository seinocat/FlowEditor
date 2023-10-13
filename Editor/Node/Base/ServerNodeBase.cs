using UnityEngine;

namespace FlowEditor.Editor
{
    public abstract class ServerNodeBase : FlowNodeBase
    {
        public override Color color => new Color(0f, 0.99f, 1f);

        public override bool ServerNode => true;
    }
}