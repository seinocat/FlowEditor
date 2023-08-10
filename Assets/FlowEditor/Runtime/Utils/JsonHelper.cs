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
    }
}