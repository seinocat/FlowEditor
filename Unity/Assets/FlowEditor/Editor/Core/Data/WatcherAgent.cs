using System;
using System.Collections.Generic;
using SeaWar.Module.Common.GameAgent;
using SeaWar.Mono.GameEvent;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace SeaWarEditor.GameEvent.Watcher
{
    [Serializable]
    public class WatcherAgent
    {
        [Title("信息")]
        [LabelText("实体Uid")]
        public long Uid;
        [LabelText("实体Id")]
        public int EntityId;
        [LabelText("实体名称")]
        public string EntityName;
        [LabelText("当前状态")]
        public GameAgentState CurState;

        private GameAgentBase m_Agent;
        
        
        
        [Button("定位实体"), VerticalGroup("操作"), PropertyOrder(10)]    
        public void Location()
        {
            SceneView sceneView = SceneView.lastActiveSceneView;
            if (sceneView != null)
            {
                Selection.activeGameObject = m_Agent.Transform.gameObject;
                sceneView.LookAt(m_Agent.Transform.position);
            }
        }
        
        [Title("事件"), Space(20)]
        [LabelText("事件列表"), TableList(AlwaysExpanded = true), PropertyOrder(20)]
        public List<WatcherEventItem> Events = new();
        
        public void Init(GameAgentBase agent)
        {
            if (agent != null)
            {
                this.m_Agent = agent;
                this.Uid = agent.Uid;
                this.EntityId = agent.EntityId;
                this.EntityName = agent.EntityConfig.Name;
                this.CurState = agent.State;
            }

            for (int i = 0; i < agent.Events.Count; i++)
            {
                WatcherEventItem eventItem = new WatcherEventItem();
                eventItem.Init(agent?.Events[i]);
                Events.Add(eventItem);
            }
        }
    }
}