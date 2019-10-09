﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

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

        // display first node properties
        if (selected_nodes != null) // if there has been a node selected, display that node's properties
        {
            Node first_node = m_nodeGraphModel.GetNodeFromID(selected_nodes[0]);
            int node_id = first_node.m_id;
            Label("Node Properties", EditorStyles.boldLabel);
            DialogueData data = m_nodeGraphModel.GetDataFromNodeID(node_id);
            if (data != null )
            {
                data.m_isStartNode = Toggle(data.m_isStartNode, "Is start node");
                Label(" ");
                
                Label("Choose branching index (order in which options are shown)");
                Label("Note: if this node is a result from a conditional node,");
                Label("choose 0 = false and 1 = true");
                data.branchingIndex = EditorGUILayout.IntField(data.branchingIndex);
                Label(" ");


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

                Label("Preview Text");
                data.previewDialogueText = TextArea(data.previewDialogueText, Height(50));

                Label("Dialogue Text");
                data.dialogueText = TextArea(data.dialogueText, Height(m_nodePropertiesRect.height * 0.25f));

                Label("Set character emotion to:");
                data.emotion = (CharacterComponent.Emotion)EditorGUILayout.EnumPopup(data.emotion);

                if (!data.isConditionalBranching)
                {
                    if (m_nodeGraphModel != null)
                    {
                        if (Button("Add Dialogue Option"))
                        {
                            m_nodeGraphModel.AddOutputPlugToNode(node_id);
                        }
                    }

                    ScriptableObject target = data;
                    SerializedObject so = new SerializedObject(target);
                    Label("");
                    Label("Item information", EditorStyles.boldLabel);
                    Label("Items to give at this dialogue node:");
                    {
                        if (Button("Add item to give"))
                        {
                            data.itemsToGive.Add(ScriptableObject.CreateInstance<Item>());
                        }
                        if (Button("Remove item to give"))
                        {
                            int index = (data.itemsToGive.Count - 1 <= 0) ? 0 : data.itemsToGive.Count - 1;
                            if (data.itemsToGive.Count > 0)
                            {
                                data.itemsToGive.RemoveAt(index);
                            }
                        }
                        SerializedProperty stringsProperty = so.FindProperty("itemsToGive");
                        EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
                        so.ApplyModifiedProperties();
                    }

                    Label("");
                    Label("Quest Information", EditorStyles.boldLabel);
                    Label("Quests to add on this node");
                    {
                        if (Button("Add quest to give"))
                        {
                            data.questsToAdd.Add(ScriptableObject.CreateInstance<Quest>());
                        }
                        if (Button("Remove quest to give"))
                        {
                            int index = (data.questsToAdd.Count - 1 <= 0) ? 0 : data.questsToAdd.Count - 1;
                            if (data.questsToAdd.Count > 0)
                            {
                                data.questsToAdd.RemoveAt(index);
                            }
                        }
                        SerializedProperty stringsProperty = so.FindProperty("questsToAdd");
                        EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
                        so.ApplyModifiedProperties();
                    }
                    Label("Quests to mark as complete on this node");
                    {
                        if (Button("Add quest to complete"))
                        {
                            data.questsToComplete.Add(ScriptableObject.CreateInstance<Quest>());
                        }
                        if (Button("Remove quest to complete"))
                        {
                            int index = (data.questsToComplete.Count - 1 <= 0) ? 0 : data.questsToComplete.Count - 1;
                            if (data.questsToComplete.Count > 0)
                            {
                                data.questsToComplete.RemoveAt(index);
                            }
                        }
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
                        if (Button("Add item to check"))
                        {
                            data.itemsToCheck.Add(ScriptableObject.CreateInstance<Item>());
                        }
                        if (Button("Remove item to check"))
                        {
                            int index = (data.itemsToCheck.Count - 1 <= 0) ? 0 : data.itemsToCheck.Count - 1;
                            if (data.itemsToCheck.Count > 0)
                            {
                                data.itemsToCheck.RemoveAt(index);
                            }
                        }

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
                        if (Button("Add quest required"))
                        {
                            data.questsRequired.Add(ScriptableObject.CreateInstance<Quest>());
                        }
                        if (Button("Remove quest required"))
                        {
                            int index = (data.questsRequired.Count - 1 <= 0) ? 0 : data.questsRequired.Count - 1;
                            if (data.questsRequired.Count > 0)
                            {
                                data.questsRequired.RemoveAt(index);
                            }
                        }

                        ScriptableObject target = data;
                        SerializedObject so = new SerializedObject(target);
                        SerializedProperty stringsProperty = so.FindProperty("questsRequired");
                        EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
                        so.ApplyModifiedProperties();
                    }
                    //quests
                    Label("Quests that need to be completed:");
                    {
                        if (Button("Add quest to complete"))
                        {
                            data.questsCompleted.Add(ScriptableObject.CreateInstance<Quest>());
                        }
                        if (Button("Remove quest to complete"))
                        {
                            int index = (data.questsCompleted.Count - 1 <= 0) ? 0 : data.questsCompleted.Count - 1;
                            if (data.questsCompleted.Count > 0)
                            {
                                data.questsCompleted.RemoveAt(index);
                            }
                        }

                        ScriptableObject target = data;
                        SerializedObject so = new SerializedObject(target);
                        SerializedProperty stringsProperty = so.FindProperty("questsCompleted");
                        EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
                        so.ApplyModifiedProperties();
                    }
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
        EndScrollView();
        EndArea();
    }

    private void DisplayAssetProperties(DialogueAssetBuilder asset_builder)
    {
        Label("Dialogue Asset", EditorStyles.boldLabel);
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
