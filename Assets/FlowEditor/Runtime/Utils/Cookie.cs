using UnityEngine;

namespace FlowEditor.Runtime
{
    public class Cookie
    {
        private static string _Cached_GUID = string.Empty;

        public static void SetUserGUID(string guid)
        {
            _Cached_GUID = guid;
        }

        public static void SetPrivate(string key, string value)
        {
            string privateKey = null;
            if (!TryGetPrivateKey(key, out privateKey))
            {
                return;
            }
            PlayerPrefs.SetString(privateKey, value);
        }
        public static void SetPrivate(string key, int value)
        {
            string privateKey = null;
            if (!TryGetPrivateKey(key, out privateKey))
            {
                return;
            }
            PlayerPrefs.SetInt(privateKey, value);
        }
        public static void SetPrivate(string key, float value)
        {
            string privateKey = null;
            if (!TryGetPrivateKey(key, out privateKey))
            {
                return;
            }
            PlayerPrefs.SetFloat(privateKey, value);
        }
        public static string GetPrivate(string key, string defaultValue = null)
        {
            string privateKey = null;
            if (!TryGetPrivateKey(key, out privateKey))
            {
                return defaultValue;
            }
            return PlayerPrefs.GetString(privateKey, defaultValue);
        }
        public static int GetPrivate(string key, int defaultValue = 0)
        {
            string privateKey = null;
            if (!TryGetPrivateKey(key, out privateKey))
            {
                return defaultValue;
            }
            return PlayerPrefs.GetInt(privateKey, defaultValue);
        }
        public static float GetPrivate(string key, float defaultValue = 0f)
        {
            string privateKey = null;
            if (!TryGetPrivateKey(key, out privateKey))
            {
                return defaultValue;
            }
            return PlayerPrefs.GetFloat(privateKey, defaultValue);
        }
        public static void DeletePrivateKey(string key)
        {
            string privateKey = null;
            if (!TryGetPrivateKey(key, out privateKey))
            {
                return;
            }
            PlayerPrefs.DeleteKey(privateKey);
        }
        public static bool ContainsPrivateKey(string key)
        {
            string privateKey = null;
            if (TryGetPrivateKey(key, out privateKey))
            {
                return PlayerPrefs.HasKey(privateKey);
            }
            return false;
        }
        private static bool TryGetPrivateKey(string key, out string privateKey)
        {
            if (!string.IsNullOrEmpty(_Cached_GUID))
            {
                privateKey = string.Format("{0}_{1}", _Cached_GUID, key);
                return true;
            }
            else
            {
                privateKey = null;
                Debug.LogError("player should be logon!");
                return false;
            }
        }
        public static void SetPublic(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }
        public static void SetPublic(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }
        public static void SetPublic(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public static void SetPublic(string key,bool value)
        {
            if (value)
                PlayerPrefs.SetInt(key, 1);
            else
                PlayerPrefs.SetInt(key, 0);
        }

        public static string GetPublic(string key, string defaultValue = null)
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }
        public static int GetPublic(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }
        public static float GetPublic(string key, float defaultValue = 0f)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }

        public static bool GetPublic(string key,bool defaultValue=false)
        {
            int newValue = 0;
            if (defaultValue)
                newValue = 1;
            return PlayerPrefs.GetInt(key, newValue) ==1;
        }

        public static void DeletePublicKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
        public static bool ContainsPublicKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        public static void Save()
        {
            PlayerPrefs.Save();
        }
    }
}