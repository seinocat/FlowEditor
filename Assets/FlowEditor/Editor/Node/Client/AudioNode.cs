using System;
using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{
    [NodeMenuItem("场景&表现/音效"), GameEventNode, Serializable]
    public class AudioNode : ClientNodeBase, ICreateNodeFrom<AudioClip>
    {
        public override string name => "音效";
        
        public override FlowNodeType Type => FlowNodeType.Audio;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
        
        [CustomSetting("音效资源"), AssetReference(typeof(string), "AudioName"), SerializeReference]
        public AudioClip AudioClip;
        
        [CustomSetting("音量")]
        public float AudioVolume = 1f;
        
        public bool InitializeNodeFromObject(AudioClip value)
        {
            this.AudioClip = value;
            return true;
        }
        
        public override bool CheckForExport()
        {
            if (this.AudioClip == null)
            {
                Debug.LogError($"{nameof(AudioNode)}, {nameof(AudioClip)}值为空！请检查节点！");
                return false;
            }
            
            return true;
        }
    }
}