using SeinoCat.FlowEditor.Runtime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace SeinoCat.FlowEditor.Editor
{
    public sealed class FlowMiniMapView : MiniMap
    {
        public FlowMiniMapView()
        {
            var posx = Cookie.GetPublic(FlowSetting.MiniMapPosX, 0f);
            var posy = Cookie.GetPublic(FlowSetting.MiniMapPosY, 200f);
            SetPosition(new Rect(posx, posy, 300, 200));
            RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
        }
        
        private void OnGeometryChanged(GeometryChangedEvent evt)
        {
            var rect = GetPosition();
            Cookie.SetPublic(FlowSetting.MiniMapPosX, rect.position.x);
            Cookie.SetPublic(FlowSetting.MiniMapPosY, rect.position.y);
        }
    }
}