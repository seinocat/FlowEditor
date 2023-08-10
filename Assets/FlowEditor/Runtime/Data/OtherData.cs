using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace FlowEditor.Runtime
{
    [Serializable]
    public class CreateUnit
    {
        [LabelText("UID")]
        public int UnitUID;
        
        [LabelText("资源配置ID")]
        public int ConfigID;
        
        [LabelText("单位类型")]
        public GuideUnitType UnitType = GuideUnitType.Monster;
        
        [LabelText("坐标")]
        public float2 Position;
        
        [LabelText("旋向")]
        public float Rotation;
    }
    
    
    
    [Serializable]
    public class CreateAsset
    {
        [LabelText("资源Uid")] 
        public string Uid;
        
        [LabelText("资源名称")]
        public string AssetName;
        
        [LabelText("资源坐标")]
        public Vector3 AssetPosition;
        
        [LabelText("资源旋向")]
        public Vector3 AssetRotation;
        
        [LabelText("资源缩放")]
        public Vector3 AssetScale = Vector3.one;
    }
    
    [Serializable]
    public enum GuideUnitType
    {
        [LabelText("船")]
        Ship,
        [LabelText("怪物")]
        Monster,
        [LabelText("人类")]
        Human
    }
    
    [Serializable]
    public class LinePosCfg
    {
        [LabelText("起点坐标")]
        public Vector3 StartPosition;
        [LabelText("终点坐标")]
        public Vector3 EndPosition;
        [LabelText("宽度")]
        public float Width = 5f;
        [LabelText("曲线高度")]
        public float Height = 2.84f;
        [LabelText("频率")]
        public float Frequency = 10.84f;
        [LabelText("放大器")] 
        public float Amplifier = 40f;
        [LabelText("WarpRandom")] 
        public float WarpRandom = 2f;
        [LabelText("WalkManual")] 
        public float WalkManual = 18f;
    }
    
    [Serializable]
    public class SelectData
    {
        [LabelText("条件组"), LabelWidth(120)] 
        public List<ConditionData> Conditions;
    
        [LabelText("下一节点ID"), LabelWidth(120)] 
        public int NextNodeID;
    }
    
    [Serializable]
    public class ConditionData
    {
        [LabelText("条件ID"), LabelWidth(120)]
        public int ConditionID;
    
        [LabelText("条件说明"), LabelWidth(120)] 
        public string Desc;
        
    }
}