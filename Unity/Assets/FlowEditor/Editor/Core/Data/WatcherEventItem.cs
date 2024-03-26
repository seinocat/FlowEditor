using System;
using System.Collections.Generic;
using System.Diagnostics;
using SeaWar;
using SeaWar.Module.Common.Dialog.UI;
using SeaWar.Module.Common.GameAgent;
using SeaWar.Module.Common.GameEvent;
using SeaWar.Module.Common.GameEvent.Utils;
using SeaWar.Module.Common.Interaction.Utils;
using SeaWar.Mono.GameEvent;
using SeaWar.Mono.GameEvent.Data;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using YooAsset.AssetSystem;
using Debug = UnityEngine.Debug;

namespace SeaWarEditor.GameEvent.Watcher
{
    [Serializable]
    public class WatcherEventItem
    {
        [LabelText("事件名称"), VerticalGroup("事件信息")]
        public string EventName;
        [LabelText("事件ID"), VerticalGroup("事件信息")]
        public int EventId;
        [LabelText("类型"), VerticalGroup("事件信息")]
        public EventActorType ActorType; 
        [LabelText("触发类型"), VerticalGroup("事件信息")]
        public TriggerType TriggerType; 
        [LabelText("触发状态"), VerticalGroup("事件信息")]
        public GameAgentState TriggerState;

        private EventActorBase m_Actor;

        [Button("执行流程"), VerticalGroup("操作")]    
        public void Execute()
        {
            switch (ActorType)
            {
                case EventActorType.Presentation:
                    InteractUIHelper.Show(new List<GameAgentBase>(){m_Actor.AgentBase});
                    break;
                case EventActorType.TaskEvent:
                    break;
                case EventActorType.CustomEvent:
                    m_Actor.ExecuteQueue();
                    break;
                case EventActorType.Dialog:
                    DialogWindow.OpenWindow((m_Actor as GameDialogActor).GroupID);
                    break;
            }
        }

        [Button("查看Json配置"), VerticalGroup("操作")]
        public void OpenJsonConfig()
        {
            var cfgName = this.ActorType == EventActorType.CustomEvent ? GameEventController.Instance.GetConfigNameById(this.EventId) : this.EventName;
            string path = Application.dataPath + $"/GameRes/Config/GameEvent/{cfgName}.json";
            path = path.Replace("/", "\\");
            Process.Start("explorer.exe", "/select," + path);
        }
        
        [Button("事件编辑器中打开"), VerticalGroup("操作")]    
        public void OpenInEditor()
        {
            string configName = string.Empty;
            switch (ActorType)
            {
                case EventActorType.Dialog:
                case EventActorType.TaskEvent:
                case EventActorType.Presentation:
                    configName = this.EventName;
                    break;
                case EventActorType.CustomEvent:
                    configName = GameEventController.Instance.GetConfigNameById(this.EventId);
                    break;
            }
            GameEventGraphWindow.OpenTagetGraph(configName);
        }
        
        [Button("重新加载(当前实体)"), VerticalGroup("操作")]    
        public async void Reload()
        {
            if (ActorType == EventActorType.CustomEvent)
            {
                var actor = m_Actor.AgentBase.Events.Find(x => x.EventID == this.EventId);
                if (actor != null)
                {
                    m_Actor.AgentBase.Events.Remove(actor);

                    var configName = GameEventController.Instance.GetConfigNameById(this.EventId);
                    var newActor = await m_Actor.AgentBase.GenerateEventActors(configName, this.EventId);
                    if (newActor != null)
                    {
                        m_Actor.AgentBase.ResetEventsDic();
                        InteractUIHelper.Refresh();
                        newActor.Init();
                        m_Actor.AgentBase.Events.Add(newActor);
                        m_Actor.AgentBase.ClassifyEvents();
                        InteractUIHelper.Refresh();
                    }
                }
            }
            else
            {
                m_Actor.AgentBase.Events.RemoveAll(x => x.EventName == this.EventName);
                var config = await AssetSys.Instance.LoadAssetAsync<TextAsset>(this.EventName);
                if (config == null)
                {
                    Debug.LogError($"找不到事件配置资源=>{this.EventName}！！！");
                    return;
                }
        
                var gameAgentData = JsonHelper.FromJson<GameAgentData>(config.text);
                if (gameAgentData == null)
                {
                    Debug.LogError($"反序列化配置{this.EventName}失败！");
                    return;
                }
                
                var interactList = GameEventFactory.ParseInteract(m_Actor.AgentBase, gameAgentData.InteractList, this.EventName);
                if (interactList.Count > 0)
                {
                    m_Actor.AgentBase.ResetEventsDic();
                    InteractUIHelper.Refresh();
                    interactList.ForEach(x=>x.Init());
                    m_Actor.AgentBase.Events.AddRange(interactList);
                    m_Actor.AgentBase.ClassifyEvents();
                    InteractUIHelper.Refresh();
                }
            }
        }
        
        [Button("重新加载(所有实体)"), VerticalGroup("操作")]    
        public async void ReloadAll()
        {
            if (!EditorUtility.DisplayDialog("提示", "确认加载所有实体吗？", "确认", "取消"))
                return;
            
            if (ActorType == EventActorType.CustomEvent)
            {
                int count = 0;
                var agents = GameAgentManager.Instance.GetAllAgent();
                foreach (var agent in agents)
                {
                    var actor = agent.Events.Find(x => x.EventID == this.EventId);
                    if (actor != null)
                    {
                        count++;
                        agent.Events.Remove(actor);

                        var configName = GameEventController.Instance.GetConfigNameById(this.EventId);
                        var newActor = await agent.GenerateEventActors(configName, this.EventId);
                        if (newActor != null)
                        {
                            agent.ResetEventsDic();
                            InteractUIHelper.Refresh();
                            newActor.Init();
                            agent.Events.Add(newActor);
                            agent.ClassifyEvents();
                            InteractUIHelper.Refresh();
                        }
                    }
                }

                EditorUtility.DisplayDialog("提示", $"重新加载完成，共计处理{count}个实体", "好的");
            }
            else
            {
                int count = 0;
                var agents = GameAgentManager.Instance.GetAllAgent();
                foreach (var agent in agents)
                {
                    if (!agent.Events.Exists(x => x.EventName == this.EventName))
                        continue;

                    count++;
                    agent.Events.RemoveAll(x => x.EventName == this.EventName);
                    var config = await AssetSys.Instance.LoadAssetAsync<TextAsset>(this.EventName);
                    if (config == null)
                    {
                        Debug.LogError($"找不到事件配置资源=>{this.EventName}！！！");
                        continue;
                    }
        
                    var gameAgentData = JsonHelper.FromJson<GameAgentData>(config.text);
                    if (gameAgentData == null)
                    {
                        Debug.LogError($"反序列化配置{this.EventName}失败！");
                        continue;
                    }
                
                    var interactList = GameEventFactory.ParseInteract(agent, gameAgentData.InteractList, this.EventName);
                    if (interactList.Count > 0)
                    {
                        agent.ResetEventsDic();
                        InteractUIHelper.Refresh();
                        interactList.ForEach(x=>x.Init());
                        agent.Events.AddRange(interactList);
                        agent.ClassifyEvents();
                        InteractUIHelper.Refresh();
                    }
                }
                
                EditorUtility.DisplayDialog("提示", $"重新加载完成，共计处理{count}个实体", "好的");
            }
        }
        


        public void Init(EventActorBase actor)
        {
            if (actor == null)
                return;
            
            this.m_Actor = actor;
            this.EventId = actor.EventID;
            this.EventName = actor.EventName;
            this.ActorType = actor.ActorType;
            this.TriggerType = actor.TriggerType;
            this.TriggerState = actor.AgentState;
        }

    }
}