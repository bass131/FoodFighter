using System;
using System.IO;
using System.Text;
using UnityEngine;
using Hyun.Extensions;

namespace Croquette
{
    public enum path_type
    {
        xml, //streamingAssets/XML/dsfkl.xml
        data, // binary path  streamingAssets/DATA/dfkdj.dt
    }
    public class PathHelper 
    {
        public static StringBuilder strBuilder = new StringBuilder();

        public static string make_path(path_type pt, string name)
        {
            strBuilder.Clear();

#if UNITY_EDITOR
            string streaming_path = "Assets/StreamingAssets";
#elif UNITY_ANDROID
            string streaming_path = Application.streamingAssetsPath; 
#endif

            switch (pt)
            {
                case path_type.xml:
                    strBuilder.Append(streaming_path)
                       .Append(Path.DirectorySeparatorChar)
                       //.Append('/')
                        .Append(kString.kXml)
                        .Append(Path.DirectorySeparatorChar)
                        //.Append('/')
                        .Append(name)
                        .Append(kString.kXmlExtensionWithDot);
                    break;
                case path_type.data:
                    if (Application.platform == RuntimePlatform.Android)
                    {
                        string path = string.Format("{0}/{1}/{2}{3}", Application.streamingAssetsPath, kString.kData, name, kString.kDTExtensionWithDot);
                        strBuilder.Append(path);
                    }
                    else
                    {
                        strBuilder.Append(streaming_path)
                        .Append(Path.DirectorySeparatorChar)
                        //.Append('/')
                        .Append(kString.kData)
                        .Append(Path.DirectorySeparatorChar)
                        //.Append('/')
                        .Append(name)
                        .Append(kString.kDTExtensionWithDot);
                    }
                    break;
            }
            Hyun.Debug.Log.Message(strBuilder.ToString());
            return strBuilder.ToString();
        }
        public static string mkpath_xml(string format, params object[] args)
        {
            string text = string.Format(format, args);
            return make_path(path_type.xml, text);
        }
        public static string mkpath_dt(string format, params object[] args)
        {
            string text = string.Format(format, args);
            return make_path(path_type.data, text);
        }

        //compiled data
        public static string mkpath_player_dt()
        {
            return mkpath_dt(kString.kPlayer);
        }
        public static string mkpath_monster_dt()
        {
            return mkpath_dt(kString.kMonster);
        }
        public static string mkpath_placement_dt()
        {
            return mkpath_dt(kString.kPlacement);
        }

        //xml
        public static string mkpath_player_xml()
        {
            return mkpath_xml(kString.kPlayer);
        }
        public static string mkpath_monster_xml()
        {
            return mkpath_xml(kString.kMonster);
        }
        public static string mkpath_placement_xml()
        {
            return mkpath_xml(kString.kPlacement);
        }
    }
}