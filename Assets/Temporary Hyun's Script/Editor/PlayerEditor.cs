using UnityEngine;
using UnityEditor;
using Hyun;

namespace Croquette
{
    using Data;
    public class PlayerEditor : EditorWindow
    {
        public PlayerEditor() { }

        [MenuItem(kString.kMenuPlayerEditor)]
        public static void ShowEditor()
        {
            var pEditor = EditorWindow.GetWindow<PlayerEditor>();
            pEditor.Show();
        }
        public void OnGUI()
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            if (GUILayout.Button("Import Xml"))
            {
                Import();
            }
            if (GUILayout.Button("Build"))
            {
                Build();
            }
            EditorGUILayout.EndVertical();
        }

        public DT_PlayerCollection s_dtcPlayer;
        public void Import()
        {
            var pathXml = PathHelper.mkpath_player_xml();
            s_dtcPlayer = XmlLoader.DeserializeFromFile<DT_PlayerCollection>(pathXml);
            if (s_dtcPlayer != null)
            {
                EditorUtility.DisplayDialog("확인", "PlayerCollection Import 성공", "OK");
            }
        }

        public void Build()
        {
            var pathDt = PathHelper.mkpath_player_dt();
            Serialization.Compile<DT_PlayerCollection>(s_dtcPlayer, pathDt);
        }
    }
}