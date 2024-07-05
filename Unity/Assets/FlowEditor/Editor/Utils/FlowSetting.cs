using Sirenix.OdinInspector;
using UnityEngine;

namespace SeinoCat.FlowEditor.Editor
{
    public class FlowSetting
    {
        
        public static void Initialize()
        {
            InitUIRes();

        }
        
        
        #region 文件夹及路径
        
        [LabelText("流程配置根目录")]
        public static string GraphRootPath = "Assets/Editor/FlowGraphs";

        /// <summary>
        /// 获取流程配置路径
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetFlowAssetPath(string name)
        {
            return $"{GraphRootPath}/{name}.asset";
        }


        #endregion


        #region 缓存字段

        [LabelText("小地图坐标X")]
        public static string MiniMapPosX = "FlowEditor_MiniMap_PosX";
        [LabelText("小地图坐标Y")]
        public static string MiniMapPosY = "FlowEditor_MiniMap_PosY";
        [LabelText("导出面板开关")]
        public static string OperationSwitch = "FlowEditor_ShowOperation";
        [LabelText("配置列表开关")]
        public static string FlieListViewSwitch = "FlowEditor_ShowFileList";
        [LabelText("小地图开关")]
        public static string MiniMapSwitch = "FlowEditor_MiniMap";

        #endregion
        
        
        #region UI设置及资源

        [LabelText("文件名字号大小")] 
        public static int FileFontSize = 15;
        [LabelText("文件Icon宽度")] 
        public static int FileIconWidth = 15;
        [LabelText("文件Icon高度")] 
        public static int FileIconHeight = 15;
        [LabelText("文件夹Icon宽度")] 
        public static int FolderIconWidth = 15;
        [LabelText("文件夹Icon高度")] 
        public static int FolderIconHeight = 15;
        
        [LabelText("选中背景色")] 
        public static Color SelectColor = new(0.22f, 0.4f, 0.67f);
        [LabelText("非选中背景色")] 
        public static Color UnSelectColor = new(0f,0f,0f,0f);
        

        [LabelText("文件夹(开启)Icon")]
        public static Sprite FolderOpenIcon;
        [LabelText("文件夹(关闭)Icon")]
        public static Sprite FolderCloseIcon;
        [LabelText("文件Icon")]
        public static Sprite FileIcon;
        [LabelText("右箭头")]
        public static string ArrowRight = "▶";
        [LabelText("下箭头")]
        public static string ArrowDown = "▼";
        [LabelText("箭头缩进")]
        public static string ArrowIndent = "    ";
        [LabelText("文件夹收缩缓存Key")]
        public static string CollapseKey = "FlowEvent_Collapse";

        private static void InitUIRes()
        {
            FileIcon = Resources.Load<Sprite>("Icon/file_icon");
            FolderOpenIcon = Resources.Load<Sprite>("Icon/folder_open_icon");
            FolderCloseIcon = Resources.Load<Sprite>("Icon/folder_close_icon");
        }
        
        /// <summary>
        /// 获取文件夹Icon
        /// </summary>
        /// <param name="isCollapse"></param>
        /// <returns></returns>  
        public static Sprite GetFolderIcon(bool isCollapse)
        {
            return isCollapse ? FolderCloseIcon : FolderOpenIcon;
        }

        /// <summary>
        /// 获取箭头Icon
        /// </summary>
        /// <param name="isCollapse"></param>
        /// <returns></returns>  
        public static string GetArrowIcon(bool isCollapse)
        {
            return isCollapse ? ArrowRight : ArrowDown;
        }

        /// <summary>
        /// 获取收缩Key
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetCollapseKey(string path)
        {
            return $"{CollapseKey}_{path}";
        }

        /// <summary>
        /// 获取选中颜色
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>
        public static Color GetSelectColor(bool select)
        {
            return select ? SelectColor : UnSelectColor;
        }

        #endregion

        
        
        
        
        
    }
}