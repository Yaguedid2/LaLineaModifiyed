using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectListFill : EditorWindow
{
     float age;
    GameObject objectTypeHandler;

    [MenuItem("MyTools/ObjectListFill")]
    public static void ShowWindow()
    {
        GetWindow(typeof(ObjectListFill));
    }
    private void OnGUI()
    {
        age = EditorGUILayout.FloatField("age", age);
        objectTypeHandler = EditorGUILayout.ObjectField("obj", objectTypeHandler, typeof(GameObject), true) as GameObject;
        if (GUILayout.Button("show"))
            {
            loopOnLabels();
        }
        if (GUILayout.Button("checkAdmireForAll"))
        {
            checkAProprtyForAllAbject();
        }
    }
    void loopOnLabels()
    {
        for(int i=0;i<Labels.LABELS.Length;i++)
        {
            objectTypeHandler.GetComponent<ObjectTypeHandler>().objects[i].index = i;
            objectTypeHandler.GetComponent<ObjectTypeHandler>().objects[i].className = Labels.LABELS[i];
        }
    }
    void checkAProprtyForAllAbject()
    {
        for (int i = 0; i < Labels.LABELS.Length; i++)
        {
            objectTypeHandler.GetComponent<ObjectTypeHandler>().objects[i].objectCategories.admire = true;

        }
    }
   
}
