using System.Collections;
using System.Collections.Generic;
using UnityEditor;
[CustomEditor(typeof(GAction))]
[CanEditMultipleObjects]
public class GActionEditor : Editor
{
   public override void OnInspectorGUI() {
        //Draw the default items in the Inspector as Unity would without
        //this script
        DrawDefaultInspector();


   }
}
