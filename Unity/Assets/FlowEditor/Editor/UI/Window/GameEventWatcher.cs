using System;
using SeaWar.Module.Common.GameAgent;
using SeaWarEditor.GameEvent.Watcher;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace SeaWarEditor.GameEvent
{
    public class GameEventWatcher : OdinMenuEditorWindow
    {
        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree();
            tree.DefaultMenuStyle = OdinMenuStyle.TreeViewStyle;
            tree.Config.DrawSearchToolbar = true;
            if (EditorApplication.isPlaying)
            {
                var agents = GameAgentManager.Instance.GetAllAgent();
                agents.Sort((x, y)=> string.Compare(x.EntityConfig.Name, y.EntityConfig.Name, StringComparison.Ordinal));
                foreach (var agent in agents)
                {
                    var item = new WatcherAgent();
                    item.Init(agent);
                    tree.AddObjectAtPath($"实体列表({agents.Count})/{item.EntityName}_{item.Uid}", item);
                }
            }
            else
            {
                var item = new WatcherAgent();
                tree.AddObjectAtPath("实体列表(1)/TestAgent", item);
            }
            
            return tree;
        }

        [MenuItem("Tools/GameEventWatcher %#w")]
        public static void OpenWindow()
        {
            // if (EditorApplication.isPlaying)
            {
                var window = GetWindow<GameEventWatcher>("GameEventWatcher");
                window.Show();
            }
            // else
            // {
            //     EditorUtility.DisplayDialog("提示", "请在游戏运行时打开", "好的");
            // }
        }
    }
}