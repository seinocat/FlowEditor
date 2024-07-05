using UnityEngine;

namespace SeinoCat.FlowEditor.Editor
{
    public class EditorNodeBase : FlowNodeBase
    {
        public override bool ServerNode => false;
        
        public override Color color => new Color(1f, 0f, 0.02f);
    }
}