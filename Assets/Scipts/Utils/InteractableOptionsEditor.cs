using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractableOptions))]
public class InteractableOptionsEditor : Editor
{
    private SerializedProperty interactionTypeProp;
    private SerializedProperty idProp;
    private SerializedProperty movableObjectProp;

    private void OnEnable()
    {
        interactionTypeProp = serializedObject.FindProperty("interactionType");
        idProp = serializedObject.FindProperty("iD");
        movableObjectProp = serializedObject.FindProperty("movableObject");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(interactionTypeProp, new GUIContent("Seleccionar opción"));
       
        InteractionType interactionType = (InteractionType)interactionTypeProp.enumValueIndex;

        switch (interactionType)
        {
            case InteractionType.StartDialogue:
                EditorGUILayout.PropertyField(idProp, new GUIContent("ID"));
                break;

            case InteractionType.StartMoving:
                EditorGUILayout.PropertyField(movableObjectProp, new GUIContent("Objeto"));
                break;
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("justAnInterraction"), new GUIContent("Solo una interacción"));
        serializedObject.ApplyModifiedProperties();
    }
}
