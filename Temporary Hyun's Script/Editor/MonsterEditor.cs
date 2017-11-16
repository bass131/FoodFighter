using UnityEngine;
using UnityEditor;
using Hyun;

namespace Croquette
{
    using Data;
    public class MonsterEditor : EditorWindow
    {
        public MonsterEditor() { }

        [MenuItem(kString.kMenuMonsterEditor)]
        public static void ShowEditor()
        {
            var pEditor = EditorWindow.GetWindow<MonsterEditor>();
            pEditor.Show(); Hyun.Debug.Log.Message("adadaad1");
        }

        public void OnGUI()
        {
            Hyun.Debug.Log.Message("adadaad2");
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

        public DT_MonsterCollection s_dtcMonster;
        public void Import()
        {
            var pathXml = PathHelper.mkpath_monster_xml();
            s_dtcMonster = XmlLoader.DeserializeFromFile<DT_MonsterCollection>(pathXml);
            if (s_dtcMonster != null)
            {
                EditorUtility.DisplayDialog("확인", "MonsterCollection Import 성공", "OK");
            }
            else
            {
                Hyun.Debug.Log.ErrorFormat("pathXml : {0}, s_dtcMonster : {1}, DT_MonsterCollection Not Found", pathXml, s_dtcMonster);
            }
        }

        public void Build()
        {
            var pathDt = PathHelper.mkpath_monster_dt();
            Serialization.Compile<DT_MonsterCollection>(s_dtcMonster, pathDt);
        }
    }
}