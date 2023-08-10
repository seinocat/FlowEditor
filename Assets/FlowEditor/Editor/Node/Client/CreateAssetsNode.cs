using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{
    [NodeMenuItem("场景&表现/创建资源(特效，Timeline)"), GameEventNode, System.Serializable]
    public class CreateAssetsNode : ClientNodeBase, ICreateNodeFrom<GameObject>
    {
        public override string name => "创建资源";
        
        public override FlowNodeType Type => FlowNodeType.CreateAssets;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
        
        [CustomSetting("资源"), AssetReference(typeof(string), "AssetName"), SerializeReference]
        public GameObject Asset;
        
        [CustomSetting("坐标")] 
        public Vector3 Position;
        
        [CustomSetting("旋转")] 
        public Vector3 Rotate;
        
        [CustomSetting("缩放")] 
        public Vector3 Scale = Vector3.one;
        
        public bool InitializeNodeFromObject(GameObject value)
        {
            this.Asset = value;
            this.Position = value.transform.position;
            this.Rotate = value.transform.rotation.eulerAngles;
            return true;
        }

        public override bool CheckForExport()
        {
            if (this.Asset == null)
            {
                Debug.LogError($"{nameof(CreateAssetsNode)}, {nameof(Asset)}值为空！请检查节点！");
                return false;
            }
            
            return true;
        }
    }
}