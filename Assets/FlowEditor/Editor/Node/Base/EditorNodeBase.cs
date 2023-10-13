using UnityEngine;

namespace FlowEditor.Editor
{
    public class EditorNodeBase : EventNodeBase
    {
        public override bool ServerNode => false;
        
        public override Color color => new Color(1f, 0f, 0.02f);
    }
}