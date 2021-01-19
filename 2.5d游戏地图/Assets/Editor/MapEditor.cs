using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapEditor : EditorWindow
{
    
    [MenuItem("工具/地图编译器")]
    static void MapEditorDraw()
    {
        if (Application.isPlaying)
        {
            MapEditor window = EditorWindow.GetWindow<MapEditor>("地图编译器");
        }
    }
  
    int x_max;
    int y_max;

    private void OnGUI()
    {
        GUILayout.Label("行数");
        int newX = EditorGUILayout.IntField(x_max);
        if (newX!=x_max) 
        {
            x_max = newX;
        }
        GUILayout.Label("列数");
        int newY = EditorGUILayout.IntField(y_max);
        if (newY != y_max)
        {
            y_max = newY;
        }
        if (GUILayout.Button("生成地图"))
        {
            MapMgr.Get.BeginCreate(x_max, y_max);
          
        }
    }
}
