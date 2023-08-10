using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace FlowEditor.Runtime
{
    /**自动生成代码，请勿手动修改[Tools/GameEvent/GenerateJsonNode]**/
    [Serializable]
    public class AudioNodeJsonData : JsonNodeData
    {
        [LabelText("音效资源")]
        public string AudioName;
        [LabelText("音量")]
        public float AudioVolume;

        public void SetArgs(string audioName, float audioVolume)
        {
            this.AudioName = audioName;
            this.AudioVolume = audioVolume;
        }
    }

    [Serializable]
    public class ChangeActionNodeJsonData : JsonNodeData
    {
        [LabelText("行为ID")]
        public int ActionID;

        public void SetArgs(int actionID)
        {
            this.ActionID = actionID;
        }
    }

    [Serializable]
    public class ClientConditionNodeJsonData : JsonNodeData
    {
        [LabelText("条件组")]
        [ListReference(typeof(int), nameof(Conditions))]
        public List<int> Conditions = new List<int>();
        [LabelText("成功节点ID")]
        public int SuccessID;
        [LabelText("失败节点ID")]
        public int FailID;

        public void SetArgs(List<int> conditions, int successID, int failID)
        {
            this.Conditions = conditions;
            this.SuccessID = successID;
            this.FailID = failID;
        }
    }

    [Serializable]
    public class ClueNodeJsonData : JsonNodeData
    {
    }

    [Serializable]
    public class CollectNodeJsonData : JsonNodeData
    {
    }

    [Serializable]
    public class CompleteReceiptNodeJsonData : JsonNodeData
    {
        [LabelText("成功节点ID")]
        public int SuccessID;
        [LabelText("失败节点ID")]
        public int FailID;

        public void SetArgs(int successID, int failID)
        {
            this.SuccessID = successID;
            this.FailID = failID;
        }
    }

    [Serializable]
    public class CreateAssetsNodeJsonData : JsonNodeData
    {
        [LabelText("资源")]
        public string AssetName;
        [LabelText("坐标")]
        public Vector3 Position;
        [LabelText("旋转")]
        public Vector3 Rotate;
        [LabelText("缩放")]
        public Vector3 Scale;

        public void SetArgs(string assetName, Vector3 position, Vector3 rotate, Vector3 scale)
        {
            this.AssetName = assetName;
            this.Position = position;
            this.Rotate = rotate;
            this.Scale = scale;
        }
    }

    [Serializable]
    public class DeliveryTaskNodeJsonData : JsonNodeData
    {
        [LabelText("任务ID")]
        public int TaskID;

        public void SetArgs(int taskID)
        {
            this.TaskID = taskID;
        }
    }

    [Serializable]
    public class DestroyAssetsNodeJsonData : JsonNodeData
    {
        [LabelText("资源名称")]
        public string AssetName;

        public void SetArgs(string assetName)
        {
            this.AssetName = assetName;
        }
    }

    [Serializable]
    public class DismantleNodeJsonData : JsonNodeData
    {
    }

    [Serializable]
    public class DragMonsterNodeJsonData : JsonNodeData
    {
    }

    [Serializable]
    public class DropSelfClientNodeJsonData : JsonNodeData
    {
        [LabelText("掉落类型")]
        public DropType DropType;
        [LabelText("延迟时间")]
        public float DelayTime;

        public void SetArgs(DropType dropType, float delayTime)
        {
            this.DropType = dropType;
            this.DelayTime = delayTime;
        }
    }

    [Serializable]
    public class EndNodeJsonData : JsonNodeData
    {
    }

    [Serializable]
    public class ExecuteEventNodeJsonData : JsonNodeData
    {
        [LabelText("事件ID")]
        public int EventID;

        public void SetArgs(int eventID)
        {
            this.EventID = eventID;
        }
    }

    [Serializable]
    public class FlowAnimaNodeJsonData : JsonNodeData
    {
        [LabelText("配置名称")]
        public string FlowName;

        public void SetArgs(string flowName)
        {
            this.FlowName = flowName;
        }
    }

    [Serializable]
    public class GetGlobalVariableNodeJsonData : JsonNodeData
    {
        [LabelText("全局变量名")]
        public string GlobalKey;

        public void SetArgs(string globalKey)
        {
            this.GlobalKey = globalKey;
        }
    }

    [Serializable]
    public class InteractSwitchNodeJsonData : JsonNodeData
    {
        [LabelText("开启交互")]
        public bool Open;

        public void SetArgs(bool open)
        {
            this.Open = open;
        }
    }

    [Serializable]
    public class LockCameraNodeJsonData : JsonNodeData
    {
    }

    [Serializable]
    public class MessageTipNodeJsonData : JsonNodeData
    {
        [LabelText("消息内容")]
        public string Content;

        public void SetArgs(string content)
        {
            this.Content = content;
        }
    }

    [Serializable]
    public class OpenTreasureBoxNodeJsonData : JsonNodeData
    {
    }

    [Serializable]
    public class OpenUINodeJsonData : JsonNodeData
    {
        [LabelText("UI类型")]
        public OpenUIType UIType;
        [LabelText("UI名称")]
        public string UIName;
        [LabelText("参数")]
        public string UIParams;
        [LabelText("打开UI立即完成")]
        public bool ImmediatelyComplete;

        public void SetArgs(OpenUIType uIType, string uIName, string uIParams, bool immediatelyComplete)
        {
            this.UIType = uIType;
            this.UIName = uIName;
            this.UIParams = uIParams;
            this.ImmediatelyComplete = immediatelyComplete;
        }
    }

    [Serializable]
    public class PlayCGNodeJsonData : JsonNodeData
    {
        [LabelText("视频资源")]
        public string CgName;

        public void SetArgs(string cgName)
        {
            this.CgName = cgName;
        }
    }

    [Serializable]
    public class PlayNpcActionNodeJsonData : JsonNodeData
    {
        [LabelText("动作名称")]
        public string ActionName;

        public void SetArgs(string actionName)
        {
            this.ActionName = actionName;
        }
    }

    [Serializable]
    public class ReceiveTaskNodeJsonData : JsonNodeData
    {
        [LabelText("任务ID")]
        public int TaskID;

        public void SetArgs(int taskID)
        {
            this.TaskID = taskID;
        }
    }

    [Serializable]
    public class SetNpcRotationNodeJsonData : JsonNodeData
    {
        [LabelText("朝向")]
        public float RatateY;

        public void SetArgs(float ratateY)
        {
            this.RatateY = ratateY;
        }
    }

    [Serializable]
    public class SetWaterNodeJsonData : JsonNodeData
    {
        [LabelText("海水配置索引")]
        public int FftWaterIndex;

        public void SetArgs(int fftWaterIndex)
        {
            this.FftWaterIndex = fftWaterIndex;
        }
    }

    [Serializable]
    public class ShakeScreenNodeJsonData : JsonNodeData
    {
        [LabelText("震动配置ID")]
        public int ShakeId;
        [LabelText("延迟时间")]
        public float DelayTime;

        public void SetArgs(int shakeId, float delayTime)
        {
            this.ShakeId = shakeId;
            this.DelayTime = delayTime;
        }
    }

    [Serializable]
    public class ShowDialogNodeJsonData : JsonNodeData
    {
        [LabelText("对话组ID")]
        [ListReference(typeof(int), nameof(GroupIds))]
        public List<int> GroupIds = new List<int>();

        public void SetArgs(List<int> groupIds)
        {
            this.GroupIds = groupIds;
        }
    }

    [Serializable]
    public class StartNodeJsonData : JsonNodeData
    {
    }

    [Serializable]
    public class StopShakeScreenNodeJsonData : JsonNodeData
    {
    }

    [Serializable]
    public class UnlockCameraNodeJsonData : JsonNodeData
    {
    }

    [Serializable]
    public class WaitTimeNodeJsonData : JsonNodeData
    {
        [LabelText("时间(秒)")]
        public float Time;

        public void SetArgs(float time)
        {
            this.Time = time;
        }
    }

    [Serializable]
    [ServerJsonNode]
    public class AutoFollowNodeJsonData : JsonNodeData
    {
        [LabelText("路径点")]
        public float MoveSpeed;
        [LabelText("路径点")]
        [ListReference(typeof(float3), nameof(PathList))]
        public List<float3> PathList = new List<float3>();

        public void SetArgs(float moveSpeed, List<float3> pathList)
        {
            this.MoveSpeed = moveSpeed;
            this.PathList = pathList;
        }
    }

    [Serializable]
    [ServerJsonNode]
    public class CommitItemNodeJsonData : JsonNodeData
    {
    }

    [Serializable]
    [ServerJsonNode]
    public class CompleteFlagNodeJsonData : JsonNodeData
    {
    }

    [Serializable]
    [ServerJsonNode]
    public class ComputeGlobalVariableNodeJsonData : JsonNodeData
    {
        [LabelText("全局变量名")]
        public string GlobalKey;
        [LabelText("值")]
        public int GlobalValue;
        [LabelText("运算类型")]
        public int ComputeType;

        public void SetArgs(string globalKey, int globalValue, int computeType)
        {
            this.GlobalKey = globalKey;
            this.GlobalValue = globalValue;
            this.ComputeType = computeType;
        }
    }

    [Serializable]
    [ServerJsonNode]
    public class ConditionNodeJsonData : JsonNodeData
    {
        [LabelText("条件组")]
        [ListReference(typeof(int), nameof(Conditions))]
        public List<int> Conditions = new List<int>();
        [LabelText("成功节点ID")]
        public int SuccessID;
        [LabelText("失败节点ID")]
        public int FailID;

        public void SetArgs(List<int> conditions, int successID, int failID)
        {
            this.Conditions = conditions;
            this.SuccessID = successID;
            this.FailID = failID;
        }
    }

    [Serializable]
    [ServerJsonNode]
    public class CreateUnitAtPointNodeJsonData : JsonNodeData
    {
        [LabelText("单位ID")]
        public int EntityID;
        [LabelText("使用输入坐标")]
        public bool UseInputPos;
        [LabelText("坐标")]
        public Vector3 Position;
        [LabelText("朝向")]
        public float Rotate;

        public void SetArgs(int entityID, bool useInputPos, Vector3 position, float rotate)
        {
            this.EntityID = entityID;
            this.UseInputPos = useInputPos;
            this.Position = position;
            this.Rotate = rotate;
        }
    }

    [Serializable]
    [ServerJsonNode]
    public class CreateUnitNodeJsonData : JsonNodeData
    {
        [LabelText("刷怪表ID")]
        public int SpawnerID;

        public void SetArgs(int spawnerID)
        {
            this.SpawnerID = spawnerID;
        }
    }

    [Serializable]
    [ServerJsonNode]
    public class DestroyUnitNodeJsonData : JsonNodeData
    {
        [LabelText("消失类型")]
        public DisapperType DisapperType;

        public void SetArgs(DisapperType disapperType)
        {
            this.DisapperType = disapperType;
        }
    }

    [Serializable]
    [ServerJsonNode]
    public class GiveAwardNodeJsonData : JsonNodeData
    {
        [LabelText("奖励表ID")]
        public int AwardID;

        public void SetArgs(int awardID)
        {
            this.AwardID = awardID;
        }
    }

    [Serializable]
    [ServerJsonNode]
    public class GiveItemsNodeJsonData : JsonNodeData
    {
        [LabelText("道具ID")]
        public int ItemID;
        [LabelText("道具数量")]
        public int ItemCount;

        public void SetArgs(int itemID, int itemCount)
        {
            this.ItemID = itemID;
            this.ItemCount = itemCount;
        }
    }

    [Serializable]
    [ServerJsonNode]
    public class ManualFollowNodeJsonData : JsonNodeData
    {
        [LabelText("目标坐标")]
        public Vector3 Position;
        [LabelText("目标范围")]
        public float Range;
        [LabelText("移动速度")]
        public float MoveSpeed;
        [LabelText("抵达停止")]
        public bool ReachStop;

        public void SetArgs(Vector3 position, float range, float moveSpeed, bool reachStop)
        {
            this.Position = position;
            this.Range = range;
            this.MoveSpeed = moveSpeed;
            this.ReachStop = reachStop;
        }
    }

    [Serializable]
    [ServerJsonNode]
    public class RandomBranchNodeJsonData : JsonNodeData
    {
        [LabelText("分支列表")]
        [ListReference(typeof(RandomBranchData), nameof(BranchDatas))]
        public List<RandomBranchData> BranchDatas = new List<RandomBranchData>();

        public void SetArgs(List<RandomBranchData> branchDatas)
        {
            this.BranchDatas = branchDatas;
        }
    }

    [Serializable]
    [ServerJsonNode]
    public class RemoveItemNodeJsonData : JsonNodeData
    {
        [LabelText("道具列表")]
        [ListReference(typeof(ItemData), nameof(ItemList))]
        public List<ItemData> ItemList = new List<ItemData>();

        public void SetArgs(List<ItemData> itemList)
        {
            this.ItemList = itemList;
        }
    }

    [Serializable]
    [ServerJsonNode]
    public class RequestSelfDropNodeJsonData : JsonNodeData
    {
        [LabelText("掉落类型")]
        public DropType DropType;

        public void SetArgs(DropType dropType)
        {
            this.DropType = dropType;
        }
    }

    [Serializable]
    [ServerJsonNode]
    public class SelectBranchNodeJsonData : JsonNodeData
    {
        [LabelText("分支列表")]
        [ListReference(typeof(BranchData), nameof(BranchDatas))]
        public List<BranchData> BranchDatas = new List<BranchData>();

        public void SetArgs(List<BranchData> branchDatas)
        {
            this.BranchDatas = branchDatas;
        }
    }

    [Serializable]
    [ServerJsonNode]
    public class SetGlobalVariableNodeJsonData : JsonNodeData
    {
        [LabelText("全局变量名")]
        public string GlobalKey;
        [LabelText("全局变量值")]
        public int GlobalValue;

        public void SetArgs(string globalKey, int globalValue)
        {
            this.GlobalKey = globalKey;
            this.GlobalValue = globalValue;
        }
    }

    [Serializable]
    [ServerJsonNode]
    public class SetPositionNodeJsonData : JsonNodeData
    {
        [LabelText("目标为玩家(注意)")]
        public bool TargetPlayer;
        [LabelText("地图ID(默认-1为本地图)")]
        public int MapID;
        [LabelText("坐标")]
        public Vector3 Position;
        [LabelText("朝向")]
        public float RotateY;

        public void SetArgs(bool targetPlayer, int mapID, Vector3 position, float rotateY)
        {
            this.TargetPlayer = targetPlayer;
            this.MapID = mapID;
            this.Position = position;
            this.RotateY = rotateY;
        }
    }

    [Serializable]
    [ServerJsonNode]
    public class UseItemNodeJsonData : JsonNodeData
    {
        [LabelText("道具列表")]
        [ListReference(typeof(ItemData), nameof(ItemList))]
        public List<ItemData> ItemList = new List<ItemData>();

        public void SetArgs(List<ItemData> itemList)
        {
            this.ItemList = itemList;
        }
    }

}
