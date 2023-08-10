#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace FlowEditor.Runtime
{
    public class JsonHelper
    {
        public static T FromJson<T>(string jsonStr)
        {
            return LitJson.JsonMapper.ToObject<T>(jsonStr);
        }

        /// <summary>
        /// 转换成Json，转成一行文本；
        /// 不利于调试，但是体积最小；
        /// </summary>
        /// <param name="data"></param>
        /// <param name="write_property">是否需要序列化属性（建议维持false）</param>
        /// <returns></returns>
        public static string ToJson(object data, bool write_property = false)
        {
            return LitJson.JsonMapper.ToJson(data, write_property);
        }

        /// <summary>
        /// 转换成Json，转换结果会交错排列
        /// </summary>
        /// <param name="data"></param>
        /// <param name="write_property">是否需要序列化属性（建议维持false）</param>
        /// <returns></returns>
        public static string ToJsonPretty(object data, bool write_property = false)
        {
            return LitJson.JsonMapper.ToJson(data, write_property);
        }

#if UNITY_EDITOR
        #region 编辑器代码

        /// <summary>
        /// 将类转换成Json文本文件保存；
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path">保存路径，需要写后缀（*.txt）</param>
        public static void ConvertToJsonTextAsset(object data, string path)
        {
            ConvertToJsonTextAsset_NonRefresh(data, path);

            AssetDatabase.Refresh();
        }

        public static void ConvertToJsonTextAsset_NonRefresh(object data, string path)
        {
            if (data == null) return;
            if (string.IsNullOrEmpty(path)) return;

            var jsonStr = ToJsonPretty(data, false);

            //保存到本地
            FileInfo file = new FileInfo(path);
            if (file.Exists)
                file.Delete();

            var sm = File.CreateText(path);
            sm.Write(jsonStr);
            sm.Close();
        }

        #endregion
#endif

    }
}