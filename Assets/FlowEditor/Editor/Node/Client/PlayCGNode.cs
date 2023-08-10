using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{
    [NodeMenuItem("场景&表现/CG"), GameEventNode, System.Serializable]
    public class PlayCGNode : ClientNodeBase
    {
        public override string name => "CG";
        
        public override FlowNodeType Type => FlowNodeType.PlayCG;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
        
        [CustomSetting("视频资源"), AssetReference(typeof(string), "CgName")] 
        public Object CgAsset;

        public override bool CheckForExport()
        {
            if (this.CgAsset == null)
            {
                Debug.LogError($"{nameof(PlayCGNode)}, {nameof(CgAsset)}值为空！请检查节点！");
                return false;
            }
            
            return true;
        }
    }
}