using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BoardGenerator))]
public class BoardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BoardGenerator map = target as BoardGenerator;

        map.GenerateBoard();
    }
}
