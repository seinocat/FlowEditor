using FlowEditor.Editor;
using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{
    [NodeMenuItem("场景&表现/销毁资源(特效，Timeline)"), GameEventNode, System.Serializable]
    public class DestroyAssetsNode : ClientNodeBase, ICreateNodeFrom<GameObject>
    {
        public override FlowNodeType Type => FlowNodeType.DestroyAssets;

        public override string name => "销毁资源";
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
        
        [CustomSetting("资源名称")] 
        public string AssetName;

        public bool InitializeNodeFromObject(GameObject value)
        {
            this.AssetName = value.name;
            return true;
        }
    }
}