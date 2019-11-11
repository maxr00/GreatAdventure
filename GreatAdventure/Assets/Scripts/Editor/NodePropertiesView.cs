﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NodePropertiesView : GUILayout
{
    Rect m_nodePropertiesRect;
    NodeGraphModel m_nodeGraphModel = null;
    Vector2 scrollPosition = new Vector2();
    public NodePropertiesView()
    {
    }

    public void DrawNodeProperties(Rect propertiesRect, List<int> selected_nodes, DialogueAssetBuilder asset)
    {
        if (asset != null)
            m_nodeGraphModel = asset.m_nodeGraphModel;

        m_nodePropertiesRect = new Rect(propertiesRect.position.x + 5f, propertiesRect.position.y + 5f, propertiesRect.width - 10f, propertiesRect.height - 10f);
        BeginArea(m_nodePropertiesRect);
        scrollPosition = BeginScrollView(scrollPosition, false, false, Width(m_nodePropertiesRect.width), Height(m_nodePropertiesRect.height));

        EditorGUI.BeginChangeCheck();

        if (SceneManager.GetActiveScene().name == asset.m_dialogueAsset.SceneName)
        {
            // display first node properties
            if (selected_nodes != null) // if there has been a node selected, display that node's properties
            {
                Node first_node = m_nodeGraphModel.GetNodeFromID(selected_nodes[0]);
                int node_id = first_node.m_id;
                Label("Node Properties", EditorStyles.boldLabel);
                DialogueData data = m_nodeGraphModel.GetDataFromNodeID(node_id);
                if (data != null)
                {
                    // draw start node
                    if (data.m_isStartNode)
                    {
                        DisplayStartNodeProperties(data, node_id);
                    }
                    // draw condition node
                    else if (data.isConditionalBranching)
                    {
                        DisplayConditionalNodeProperties(data, node_id, asset);
                    }
                    // draw option node
                    else if (data.m_isBranching && !data.isConditionalBranching)
                    {
                        DisplayOptionNodeProperties(data, node_id, asset);
                    }
                    // draw normal node
                    else
                    {
                        DisplayNormalNodeProperties(data, asset);
                    }
                }

            }
            else if (asset != null) // display asset properties
            {
                DisplayAssetProperties(asset);
                // cheat sheet for the tags in the markup so far
                {
                    DisplayMarkupCheatSheet();
                }
            }

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(asset.m_dialogueAsset, "");
            }
        }
        else
        {
            Label("Used in Scene :" + asset.m_dialogueAsset.SceneName, EditorStyles.boldLabel);
            Label("Asset locked and not editable because you are in the incorrect scene");

            Label("", EditorStyles.boldLabel);
            Label("", EditorStyles.boldLabel);
            Label("ONLY DO THIS IF YOU'RE SURE", EditorStyles.boldLabel);
            if (Button("Change Asset to Current Scene"))
            {
                asset.m_dialogueAsset.SceneName = SceneManager.GetActiveScene().name;
            }
        }
        EndScrollView();
        EndArea();
    }

    private void DisplayStartNodeProperties(DialogueData data, int node_id)
    {
        if (!data.isConditionalBranching)
        {
            ScriptableObject target = data;
            SerializedObject so = new SerializedObject(target);
            Label("");
            Label("Item information", EditorStyles.boldLabel);
            Label("Items to give at this dialogue node:");
            {
                SerializedProperty stringsProperty = so.FindProperty("itemsToGive");
                EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
                so.ApplyModifiedProperties();
            }

            Label("");
            Label("Quest Information", EditorStyles.boldLabel);
            Label("Quests to add on this node");
            {
                SerializedProperty stringsProperty = so.FindProperty("questsToAdd");
                EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
                so.ApplyModifiedProperties();
            }
            Label("Quests to mark as complete on this node");
            {
                SerializedProperty stringsProperty = so.FindProperty("questsToComplete");
                EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
                so.ApplyModifiedProperties();
            }

            Label("Events to trigger at this node");
            SerializedProperty eventsProperty = so.FindProperty("m_dialogueEvent");
            EditorGUILayout.PropertyField(eventsProperty, true); // True means show children
            so.ApplyModifiedProperties();
        }
        else if (data.isConditionalBranching)
        {
            Label("");
            // items
            Label("Items to check at this dialogue node:");
            {
                ScriptableObject target = data;
                SerializedObject so = new SerializedObject(target);
                SerializedProperty stringsProperty = so.FindProperty("itemsToCheck");
                EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
                so.ApplyModifiedProperties();
            }
            Label("");
            //quests
            Label("Quests required at this dialogue node:");
            {
                ScriptableObject target = data;
                SerializedObject so = new SerializedObject(target);
                SerializedProperty stringsProperty = so.FindProperty("questsRequired");
                EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
                so.ApplyModifiedProperties();
            }
            //quests
            Label("Quests that need to be completed:");
            {
                ScriptableObject target = data;
                SerializedObject so = new SerializedObject(target);
                SerializedProperty stringsProperty = so.FindProperty("questsCompleted");
                EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
                so.ApplyModifiedProperties();
            }
        }        
    }

    private void DisplayNormalNodeProperties(DialogueData data, DialogueAssetBuilder asset)
    {
        if (data.branchingIndex == 0)
            Label("False condition node", EditorStyles.boldLabel);
        else if (data.branchingIndex == 1)
            Label("True condition node", EditorStyles.boldLabel);
        else { Label("out index : " + data.branchingIndex.ToString()); }

        Label("Character Speaking");
        List<string> current_characters = asset.m_dialogueAsset.GetInvolvedCharacterStrings();
        if (current_characters.Count == 0)
        {
            Label("No characters in list", EditorStyles.boldLabel);
        }
        else
        {
            if (data.characterSpeakingIndex >= current_characters.Count)
                data.characterSpeakingIndex = 0;
            data.characterSpeakingIndex = EditorGUILayout.Popup(data.characterSpeakingIndex, current_characters.ToArray());
            data.characterName = current_characters[data.characterSpeakingIndex];
        }
        if (data.previewDialogueText != "")
        {
            Label("Option Preview Text");
            Label(data.previewDialogueText);
            Label("");
        }

        Label("Dialogue Text");
        data.dialogueText = TextArea(data.dialogueText, Height(m_nodePropertiesRect.height * 0.25f));

        Label("Set character emotion to:");
        data.emotion = (CharacterComponent.Emotion)EditorGUILayout.EnumPopup(data.emotion);
            
        // giving items/quests/completeing quests
        ScriptableObject target = data;
        SerializedObject so = new SerializedObject(target);
        Label("");
        Label("Item information", EditorStyles.boldLabel);
        Label("Items to give at this dialogue node:");
        {
            SerializedProperty stringsProperty = so.FindProperty("itemsToGive");
            EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
            so.ApplyModifiedProperties();
        }

        Label("");
        Label("Quest Information", EditorStyles.boldLabel);
        Label("Quests to add on this node");
        {
            SerializedProperty stringsProperty = so.FindProperty("questsToAdd");
            EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
            so.ApplyModifiedProperties();
        }
        Label("Quests to mark as complete on this node");
        {
            SerializedProperty stringsProperty = so.FindProperty("questsToComplete");
            EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
            so.ApplyModifiedProperties();
        }

        Label("Events to trigger at this node");
        SerializedProperty eventsProperty = so.FindProperty("m_dialogueEvent");
        EditorGUILayout.PropertyField(eventsProperty, true); // True means show children
        so.ApplyModifiedProperties();
    }

    private void DisplayOptionNodeProperties(DialogueData data, int node_id, DialogueAssetBuilder asset)
    {
        if (data.branchingIndex == 0)
            Label("False condition node", EditorStyles.boldLabel);
        else if (data.branchingIndex == 1)
            Label("True condition node", EditorStyles.boldLabel);
        else { Label("out index : " + data.branchingIndex.ToString()); }

        // get next dialogue nodes
        List<int> nextData = asset.GetNextDialogueData(data, m_nodeGraphModel);
        int optionIndex = 0;
        foreach (var nextid in nextData)
        {
            Node nextNode = m_nodeGraphModel.GetNodeFromID(nextid);
            if (nextNode != null)
            {
                DialogueData next = m_nodeGraphModel.GetDataFromNodeID(nextid);
                if (next != null)
                {
                    Label("Preview Text for option " + optionIndex.ToString());
                    next.previewDialogueText = TextArea(next.previewDialogueText, Height(50));
                }
            }
            optionIndex++;
        }

        if (data.m_isBranching && !data.isConditionalBranching)
        {
            if (m_nodeGraphModel != null)
            {
                if (Button("Add Dialogue Option"))
                {
                    m_nodeGraphModel.AddOutputPlugToNode(node_id);
                }
            }
        }
    }

    private void DisplayConditionalNodeProperties(DialogueData data, int node_id, DialogueAssetBuilder asset)
    {
        if (data.branchingIndex == 0)
            Label("False condition node", EditorStyles.boldLabel);
        else if (data.branchingIndex == 1)
            Label("True condition node", EditorStyles.boldLabel);
        else { Label("out index : " + data.branchingIndex.ToString()); }           

        Label("");
        // items
        Label("Items to check at this dialogue node:");
        {
            ScriptableObject target = data;
            SerializedObject so = new SerializedObject(target);
            SerializedProperty stringsProperty = so.FindProperty("itemsToCheck");
            EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
            so.ApplyModifiedProperties();
        }
        Label("");
        //quests
        Label("Quests required at this dialogue node:");
        {
            ScriptableObject target = data;
            SerializedObject so = new SerializedObject(target);
            SerializedProperty stringsProperty = so.FindProperty("questsRequired");
            EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
            so.ApplyModifiedProperties();
        }
        //quests
        Label("Quests that need to be completed:");
        {
            ScriptableObject target = data;
            SerializedObject so = new SerializedObject(target);
            SerializedProperty stringsProperty = so.FindProperty("questsCompleted");
            EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
            so.ApplyModifiedProperties();
        }
    }

    private void DisplayAssetProperties(DialogueAssetBuilder asset_builder)
    {
        Label("Dialogue Asset", EditorStyles.boldLabel);
        Label("Used in Scene :" + asset_builder.m_dialogueAsset.SceneName, EditorStyles.boldLabel);
        DialogueAsset asset = asset_builder.m_dialogueAsset;
        asset.m_isActiveDialogue = Toggle(asset.m_isActiveDialogue, "Is Dialogue Active");

        ScriptableObject target = asset;
        SerializedObject so = new SerializedObject(target);

        Label("");
        Label("Characeters Involved");
        if (Button("Add character involved"))
        {
            asset.m_CharactersInvoled.Add(null);
        }
        if (Button("Remove character involved"))
        {
            int index = (asset.m_CharactersInvoled.Count - 1 <= 0) ? 0 : asset.m_CharactersInvoled.Count - 1;
            if (asset.m_CharactersInvoled.Count > 0)
            {
                asset.m_CharactersInvoled.RemoveAt(index);
            }
        }
        for (int i = 0; i < asset.m_CharactersInvoled.Count; ++i)
        {
            asset.m_CharactersInvoled[i] = EditorGUILayout.ObjectField(asset.m_CharactersInvoled[i], typeof(GameObject), true) as GameObject;
        }


        so.ApplyModifiedProperties();

        Label("");
        if (Button("Save Asset", Width(m_nodePropertiesRect.width - 20)))
        {
            // save asset
            asset_builder.SaveAsset(m_nodeGraphModel);
        }
    }

    private void DisplayMarkupCheatSheet()
    {
        Label("Dialogue Markup", EditorStyles.boldLabel);
        //italics
        Label("Italics: <i> .. </i>", Width(m_nodePropertiesRect.width - 20));
        //bold
        Label("Bold: <b> .. </b>", Width(m_nodePropertiesRect.width - 20));
        //line break
        Label("New Line: <ln>", Width(m_nodePropertiesRect.width - 20));
        //color
        Label("Color: <color=#hex> .. </color>", Width(m_nodePropertiesRect.width - 20));
        //size
        Label("Font Size: <size=number> .. </size>", Width(m_nodePropertiesRect.width - 20));
        //speed
        Label("Speed: <sp=float> .. </sp>", Width(m_nodePropertiesRect.width - 20));
        //shake
        Label("Shake: <shake=radius,speed> .. </shake>", Width(m_nodePropertiesRect.width - 20));
        //wiggle
        Label("Wiggle: <wiggle=height,speed> .. </wiggle>", Width(m_nodePropertiesRect.width - 20));
        //pause
        Label("Pause: <p> for default pause time OR <p=duration>", Width(m_nodePropertiesRect.width - 20));
    }
}
