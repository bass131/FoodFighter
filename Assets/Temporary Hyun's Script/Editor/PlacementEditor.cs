using UnityEngine;
using UnityEditor;
using Hyun;
namespace Croquette
{
    using Data;
    public class PlacementEditor : EditorWindow
    {
        public PlacementEditor() { }

        [MenuItem(kString.kMenuPlacementEditor)]
        public static void ShowEditor()
        {
            var pEditor = EditorWindow.GetWindow<PlacementEditor>();
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

        public DT_PlacementCollection s_dtcPlacement;
        public void Import()
        {
            var pathXml = PathHelper.mkpath_placement_xml();
            s_dtcPlacement = XmlLoader.DeserializeFromFile<DT_PlacementCollection>(pathXml);
            if (s_dtcPlacement != null)
            {
                EditorUtility.DisplayDialog("확인", "PlacementCollection Import 성공", "OK");
            }

        }

        public void Build()
        {
            var pathDt = PathHelper.mkpath_placement_dt();
            Serialization.Compile<DT_PlacementCollection>(s_dtcPlacement, pathDt);
        }
    }
}