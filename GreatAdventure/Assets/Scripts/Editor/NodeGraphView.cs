﻿using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;

public class NodeGraphView : GUILayout
{
    private NodeGraphModel m_nodeGraphModel;
    private DialogueAssetBuilder m_assetBuilder;
    private Rect m_nodeGraphRect;

    private List<int> m_nodeIDsSelected = new List<int>(); // for multiselect
    private Vector2 m_graphOffset;
    private Vector2 m_graphDrag;
    private bool m_isDragged;
    private Vector2 m_multiSelectStartPos;
    private Vector2 m_multiSelectEndPos;
    private bool m_isMultiSelectOn;

    private GUIStyle m_nodeStyle; // default (dialogue)
    private GUIStyle m_conditionalNodeStyle; // branching node style
    private GUIStyle m_startNodeStyle;
    private GUIStyle m_optionNodeStyle;
    private GUIStyle m_nodeSelectedStyle;
    private GUIStyle m_inputPlugStyle;
    private GUIStyle m_outputPlugStyle;
    private GUIStyle m_outputFalsePlugStyle;
    private GUIStyle m_outputTruePlugStyle;
    public Vector2 m_plugDimensions = new Vector2(10f, 20f);
    public float plug_height = 20.0f;
    public float in_between_plug_height = 10.0f;

    private Texture errorTex;
    private Texture QuestGivenTex;
    private Texture QuestCompleteTex;
    private Texture ItemGivenTex;

    // used for drawing connections
    private Plug m_currentSelectedInPlug;
    private Plug m_currentSelectedOutPlug;

    public NodeGraphView()
    {
        m_nodeGraphModel = new NodeGraphModel();

        // initializing the different syles for nodes and plugs
        m_nodeStyle = new GUIStyle();
        m_nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node2.png") as Texture2D;
        m_nodeStyle.hover.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node4.png") as Texture2D;
        m_nodeStyle.border = new RectOffset(12, 12, 12, 12);

        // conditional node style
        m_conditionalNodeStyle = new GUIStyle();
        m_conditionalNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        m_conditionalNodeStyle.hover.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node4.png") as Texture2D;
        m_conditionalNodeStyle.border = new RectOffset(12, 12, 12, 12);

        // start node style
        m_startNodeStyle = new GUIStyle();
        m_startNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node3.png") as Texture2D;
        m_startNodeStyle.hover.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node4.png") as Texture2D;
        m_startNodeStyle.border = new RectOffset(12, 12, 12, 12);

        // option node style
        m_optionNodeStyle = new GUIStyle();
        m_optionNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node5.png") as Texture2D;
        m_optionNodeStyle.hover.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node4.png") as Texture2D;
        m_optionNodeStyle.border = new RectOffset(12, 12, 12, 12);

        m_nodeSelectedStyle = new GUIStyle();
        m_nodeSelectedStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node4.png") as Texture2D;
        m_nodeSelectedStyle.border = new RectOffset(15, 15, 15, 15);

        m_inputPlugStyle = new GUIStyle();
        m_inputPlugStyle.normal.background = EditorGUIUtility.Load("builtin skins/lightskin/images/btn left.png") as Texture2D;
        m_inputPlugStyle.active.background = EditorGUIUtility.Load("builtin skins/lightskin/images/btn left on.png") as Texture2D;
        m_inputPlugStyle.border = new RectOffset(4, 4, 12, 12);

        m_outputPlugStyle = new GUIStyle();
        m_outputPlugStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        m_outputPlugStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        m_outputPlugStyle.border = new RectOffset(4, 4, 12, 12);

        // conditional plug styles
        //true
        m_outputTruePlugStyle = new GUIStyle();
        m_outputTruePlugStyle.normal.background = EditorGUIUtility.Load("builtin skins/lightskin/images/btn right.png") as Texture2D;
        m_outputTruePlugStyle.active.background = EditorGUIUtility.Load("builtin skins/lightskin/images/btn right on.png") as Texture2D;
        m_outputTruePlugStyle.border = new RectOffset(4, 4, 12, 12);
        //false
        m_outputFalsePlugStyle = new GUIStyle();
        m_outputFalsePlugStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        m_outputFalsePlugStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        m_outputFalsePlugStyle.border = new RectOffset(4, 4, 12, 12);

        // textures
        errorTex = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
    }

    public void DrawNodeGraph(Rect graphRect, DialogueAssetBuilder asset)
    {
        if (asset != null)
        {
            m_nodeGraphModel = asset.m_nodeGraphModel;
            m_assetBuilder = asset;
        }

        if (m_nodeGraphModel.startNodeID == 0)
        {
            int id = m_nodeGraphModel.AddNode(new Vector2(10, 10));
            m_nodeGraphModel.AddOutputPlugToNode(id);
            m_nodeGraphModel.GetDataFromNodeID(id).m_isStartNode = true;
            m_nodeGraphModel.startNodeID = id;
        }

        m_nodeGraphRect = graphRect;
        BeginArea(graphRect);
        Label("Node Graph");

        // write out asset errors

        DrawGrid(graphRect, 20, 0.2f, Color.gray); // light grid-lines
        DrawGrid(graphRect, 100, 0.4f, Color.gray); // dark grid-lines
        DrawNodes();
        DrawConnections();
        DrawActiveConnection(Event.current);
        if (m_isMultiSelectOn)
        {
            DrawMultiSelectBox();
        }
        EndArea();
    }

    public List<int> GetSelectedNodes()
    {
        // return the first selected node for the node properities view
        if (m_nodeIDsSelected.Count != 0)
            return m_nodeIDsSelected;
        return null;
    }

    public void AddNode(Vector2 position)
    {
        int modelID = m_nodeGraphModel.AddNode(position);
        m_nodeGraphModel.AddOutputPlugToNode(modelID);
    }

    public void AddOptionNode(Vector2 position)
    {
        int modelID = m_nodeGraphModel.AddNode(position);
        m_nodeGraphModel.AddOutputPlugToNode(modelID);
        m_nodeGraphModel.GetDataFromNodeID(modelID).m_isBranching = true;
    }

    public void AddConditionalNode(Vector2 position)
    {
        int modelID = m_nodeGraphModel.AddConditionalNode(position);
        Node cond_node = m_nodeGraphModel.GetNodeFromID(modelID);
    }

    public bool ProcessEvents(Event e, DialogueAssetBuilder asset)
    {
        m_graphDrag = Vector2.zero;
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0) // left click
                {
                    Node selectedNode = PointInNode(e.mousePosition);
                    if (selectedNode != null) // clicked on node
                    {
                        if (m_nodeIDsSelected.Count <= 1)
                        {
                            m_nodeIDsSelected = new List<int>();
                            m_nodeIDsSelected.Add(selectedNode.m_id);
                        }
                        m_isDragged = true;
                    }
                    else if(m_nodeGraphRect.Contains(e.mousePosition))// clicked on graph
                    {
                        m_isMultiSelectOn = true;
                        m_multiSelectStartPos = e.mousePosition;
                        m_multiSelectEndPos = m_multiSelectStartPos;
                        m_nodeIDsSelected = new List<int>();
                    }
                    ClearConnectionSelection();
                    return true;
                }
                else if (e.button == 1) // right click
                {
                    Node node_selected = PointInNode(e.mousePosition);
                    if (node_selected != null) //right click on node
                    {
                        ProcessNodeContextMenu(node_selected.m_id);
                        e.Use();
                    }
                    else if (m_nodeGraphRect.Contains(e.mousePosition)) // right click on node graph
                    {
                        ProcessNodeGraphContextMenu(e.mousePosition);
                    }
                    return true;
                }
                else if (e.button == 2) // middle-mouse click
                {
                    m_isDragged = true;
                }
                break;
            case EventType.MouseUp:
                m_isDragged = false;
                m_isMultiSelectOn = false;
                if (m_nodeIDsSelected.Count == 0)
                    SelectNodesInBox();
                break;
            case EventType.MouseDrag:
                if (e.button == 0)
                {
                    if (m_isDragged) // moving selected nodes
                    {
                        DragSelectedNodes(e.delta);
                        e.Use();
                        return true;
                    }
                    if (m_isMultiSelectOn) // changing multi-select size
                    {
                        m_multiSelectEndPos = e.mousePosition;
                        e.Use();
                        return true;
                    }
                }
                if (e.button == 2 && m_isDragged) //panning the graph
                {
                    m_graphDrag = e.delta;
                    DragAllNodes();
                }
                break;
            case EventType.KeyDown:
                if (e.keyCode == KeyCode.Delete) // delete selected nodes
                {
                    for (int i = 0; i < m_nodeIDsSelected.Count; ++i)
                    {
                        RemoveNode(m_nodeIDsSelected[i]);
                    }
                    m_nodeIDsSelected = new List<int>();
                    return true;
                }
                else if (e.control && e.keyCode == KeyCode.S) //Ctrl+S = save asset
                {
                    asset.SaveAsset(asset.m_nodeGraphModel);
                }
                else if (e.control && e.keyCode == KeyCode.D)
                {
                    for (int i = 0; i < m_nodeIDsSelected.Count; ++i)
                    {
                        DuplicateNode(m_nodeIDsSelected[i]);
                    }
                    m_nodeIDsSelected = new List<int>();
                    return true;
                }
                break;
        }
        return false;
    }

    private void DrawMultiSelectBox()
    {
        Vector3 vert1 = new Vector3(m_multiSelectStartPos.x, m_multiSelectStartPos.y, 0);
        Vector3 vert2 = new Vector3(m_multiSelectEndPos.x, m_multiSelectStartPos.y, 0);
        Vector3 vert3 = new Vector3(m_multiSelectStartPos.x, m_multiSelectEndPos.y, 0);
        Vector3 vert4 = new Vector3(m_multiSelectEndPos.x, m_multiSelectEndPos.y, 0);

        Handles.BeginGUI();
        Handles.color = Color.blue;
        Handles.DrawLine(vert1, vert2);
        Handles.DrawLine(vert1, vert3);
        Handles.DrawLine(vert4, vert2);
        Handles.DrawLine(vert4, vert3);
        Handles.color = Color.white;
        Handles.EndGUI();

        GUI.changed = true;
    }

    private void SelectNodesInBox()
    {
        float x = Mathf.Min(m_multiSelectStartPos.x, m_multiSelectEndPos.x);
        float y = Mathf.Min(m_multiSelectStartPos.y, m_multiSelectEndPos.y);
        Rect multiselect_rect = new Rect(x, y, Mathf.Abs(m_multiSelectStartPos.x - m_multiSelectEndPos.x), Mathf.Abs(m_multiSelectStartPos.y - m_multiSelectEndPos.y));
        foreach (KeyValuePair<int, Node> node_pair in m_nodeGraphModel.GetNodes())
        {
            Node node = node_pair.Value;
            Vector2 node_position = node.m_position;
            Vector2 node_dimensions = node.m_dimension;
            if (multiselect_rect.Contains(node.m_position))
                m_nodeIDsSelected.Add(node.m_id);
        }
    }

    private void DrawActiveConnection(Event e)
    {
        if (m_currentSelectedInPlug != null && m_currentSelectedOutPlug == null)
        {
            Handles.DrawBezier(m_currentSelectedInPlug.m_position, e.mousePosition,
                m_currentSelectedInPlug.m_position + Vector2.left * 50f, e.mousePosition - Vector2.left,
                Color.white, null, 2f);
        }
        else if (m_currentSelectedOutPlug != null && m_currentSelectedInPlug == null)
        {
            Handles.DrawBezier(m_currentSelectedOutPlug.m_position, e.mousePosition,
                m_currentSelectedOutPlug.m_position - Vector2.left * 50f, e.mousePosition + Vector2.left,
                Color.white, null, 2f);
        }

        GUI.changed = true;
    }

    private void CreateConnection()
    {
        // add connection in model
        if (m_currentSelectedInPlug != null && m_currentSelectedOutPlug != null)
        {
            Node inputNode = m_nodeGraphModel.GetNodeFromID(m_currentSelectedInPlug.m_nodeId);
            Node outputNode = m_nodeGraphModel.GetNodeFromID(m_currentSelectedOutPlug.m_nodeId);
            m_nodeGraphModel.AddConnection(m_currentSelectedInPlug.m_plugId, m_currentSelectedOutPlug.m_plugId, inputNode.m_id, outputNode.m_id);
            m_currentSelectedOutPlug = null;
            m_currentSelectedInPlug = null;
        }
    }

    private void OnClickRemoveConnection(Connection connection)
    {
        m_nodeGraphModel.RemoveConnection(connection.m_id);
    }

    private void ClearConnectionSelection()
    {
        m_currentSelectedInPlug = null;
        m_currentSelectedOutPlug = null;
    }

    private void DrawConnections()
    {
        if (m_nodeGraphModel.GetConnections().Count != 0)
        {
            Connection connection_selected = null;
            foreach (KeyValuePair<int, Connection> connection_pair in m_nodeGraphModel.GetConnections())
            {
                Connection connection = connection_pair.Value;
                Plug input_plug = m_nodeGraphModel.GetNodeFromID(connection.m_inputNodeId).m_inputPlug;
                Plug output_plug;
                m_nodeGraphModel.GetNodeFromID(connection.m_outputNodeId).m_outputPlugs.TryGetValue(connection.m_outputPlugId, out output_plug);

                Handles.DrawBezier(input_plug.m_position, output_plug.m_position,
                    input_plug.m_position + Vector2.left * 50f, output_plug.m_position - Vector2.left * 50f,
                    Color.white, null, 2f);


                if (Handles.Button((input_plug.m_position + output_plug.m_position) * 0.5f, Quaternion.identity, 8, 16, Handles.SphereHandleCap))
                {
                    connection_selected = connection;
                }
            }
            if (connection_selected != null) // clicking on the selection will delete it
            {
                OnClickRemoveConnection(connection_selected);
            }
        }
    }

    private void ProcessNodeGraphContextMenu(Vector2 mouse_position)
    {
        GenericMenu generic_menu = new GenericMenu();
        generic_menu.AddItem(new GUIContent("Add Dialogue Node"), false, () => AddNode(mouse_position));
        generic_menu.AddItem(new GUIContent("Add Conditional Node"), false, () => AddConditionalNode(mouse_position));
        generic_menu.AddItem(new GUIContent("Add Option Node"), false, () => AddOptionNode(mouse_position));
        generic_menu.ShowAsContext();
    }

    private void ProcessNodeContextMenu(int node_id)
    {
        GenericMenu generic_menu = new GenericMenu();
        generic_menu.AddItem(new GUIContent("Remove Node"), false, () => RemoveNode(node_id));
        generic_menu.AddItem(new GUIContent("Duplicate Node"), false, () => DuplicateNode(node_id));
        Node selectedNode = m_nodeGraphModel.GetNodeFromID(node_id);
        if (selectedNode.isConditionalNode)
            generic_menu.AddItem(new GUIContent("Change to Normal"), false, () => ChangeToNormal(node_id));
        else
            generic_menu.AddItem(new GUIContent("Change to Conditional"), false, () => ChangeToConditional(node_id));

        generic_menu.ShowAsContext();
    }

    private void ProcessConnectionContextMenu(Connection connection)
    {
        GenericMenu generic_menu = new GenericMenu();
        generic_menu.AddItem(new GUIContent("Remove Connection"), false, () => OnClickRemoveConnection(connection));
        generic_menu.ShowAsContext();
    }

    private void ChangeToConditional(int node_id)
    {
        DialogueData data = m_nodeGraphModel.GetDataFromNodeID(node_id);
        int characterSpeakingIndex = data.characterSpeakingIndex;
        bool isStartNode = data.m_isStartNode;
        string dialogueText = data.dialogueText;
        Vector2 position = m_nodeGraphModel.GetNodeFromID(node_id).m_position;
        
        RemoveNode(node_id);
        int new_id = m_nodeGraphModel.AddConditionalNode(position);
        m_nodeGraphModel.GetDataFromNodeID(new_id).dialogueText = dialogueText;
        m_nodeGraphModel.GetDataFromNodeID(new_id).characterSpeakingIndex = characterSpeakingIndex;
        m_nodeGraphModel.GetDataFromNodeID(new_id).m_isStartNode = isStartNode;
        m_nodeGraphModel.startNodeID = new_id;
    }

    private void ChangeToNormal(int node_id)
    {
        DialogueData data = m_nodeGraphModel.GetDataFromNodeID(node_id);
        int characterSpeakingIndex = data.characterSpeakingIndex;
        bool isStartNode = data.m_isStartNode;
        string dialogueText = data.dialogueText;
        Vector2 position = m_nodeGraphModel.GetNodeFromID(node_id).m_position;

        RemoveNode(node_id);
        int new_id = m_nodeGraphModel.AddNode(position);
        m_nodeGraphModel.AddOutputPlugToNode(new_id);
        m_nodeGraphModel.GetDataFromNodeID(new_id).dialogueText = dialogueText;
        m_nodeGraphModel.GetDataFromNodeID(new_id).characterSpeakingIndex = characterSpeakingIndex;
        m_nodeGraphModel.GetDataFromNodeID(new_id).m_isStartNode = isStartNode;
        m_nodeGraphModel.startNodeID = new_id;
    }

    private void DuplicateNode(int node_id)
    {
        m_nodeGraphModel.DuplicateNode(node_id);
    }

    private void RemoveNode(int node_id)
    {
        m_nodeIDsSelected.Remove(node_id);
        m_nodeGraphModel.RemoveNode(node_id);
    }

    private void DragSelectedNodes(Vector2 drag)
    {
        foreach (int nodeId in m_nodeIDsSelected)
        {
            Node selectedNode = m_nodeGraphModel.GetNodeFromID(nodeId);
            selectedNode.m_position += drag;
        }
    }

    private void DragAllNodes()
    {
        foreach (KeyValuePair<int, Node> node_pair in m_nodeGraphModel.GetNodes())
        {
            Node node = node_pair.Value;
            node.m_position += m_graphDrag;
        }
    }

    private void DrawGrid(Rect gridRect, float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(gridRect.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(gridRect.height / gridSpacing);

        Handles.BeginGUI();
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        m_graphOffset += m_graphDrag * 0.5f; // if graph has been panned by an amount
        Vector3 newOffset = new Vector3(m_graphOffset.x % gridSpacing, m_graphOffset.y % gridSpacing, 0);

        for (int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, gridRect.height, 0f) + newOffset);
        }

        for (int j = 0; j < heightDivs; j++)
        {
            Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(gridRect.width, gridSpacing * j, 0f) + newOffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }

    private Node PointInNode(Vector2 position) // returns which node the point given is in (null if none)
    {
        foreach (KeyValuePair<int, Node> node_pair in m_nodeGraphModel.GetNodes())
        {
            Node node = node_pair.Value;
            Vector2 node_position = node.m_position;
            Vector2 node_dimensions = node.m_dimension;
            Rect nodeRect = new Rect(node_position.x, node_position.y, node_dimensions.x, node_dimensions.y);
            if (nodeRect.Contains(position))
                return node;
        }
        return null;
    }

    private void DrawNodes()
    {
        foreach (KeyValuePair<int, Node> node_pair in m_nodeGraphModel.GetNodes())
        {
            Node node = node_pair.Value;
            Vector2 node_position = node.m_position;

            // if current node is in selected nodes list, draw it with the selected node style
            if (m_nodeIDsSelected.Find(x => x == node.m_id) == node.m_id)
            {
                if (node.m_id == m_nodeGraphModel.startNodeID)
                    DrawStartNode(node, m_nodeSelectedStyle);
                else if (node.isConditionalNode)
                    DrawConditionalNode(node, m_nodeSelectedStyle);
                else if (node.m_outputPlugs.Count > 1)
                    DrawOptionNode(node, m_nodeSelectedStyle);
                else
                    DrawNode(node, m_nodeSelectedStyle);
                continue;
            }
            if (node.m_id == m_nodeGraphModel.startNodeID)
                DrawStartNode(node, m_startNodeStyle);
            else if (node.isConditionalNode)
                DrawConditionalNode(node, m_conditionalNodeStyle);
            else if (node.m_outputPlugs.Count > 1)
                DrawOptionNode(node, m_optionNodeStyle);
            else
                DrawNode(node, m_nodeStyle);
        }
    }

    private void DrawNode(Node node, GUIStyle style)
    {
        Rect nodeRect = new Rect(node.m_position.x, node.m_position.y, node.m_dimension.x, node.m_dimension.y);

        // draw plugs on specific node
        DrawPlug(node.m_inputPlug, nodeRect, m_inputPlugStyle, 1, 1);
        int plug_count = 1;
        Plug delete_plug = null;
        foreach (KeyValuePair<int, Plug> output_plug in node.m_outputPlugs)
        {
            if (delete_plug == null)
                delete_plug = DrawPlug(output_plug.Value, nodeRect, m_outputPlugStyle, plug_count, node.m_outputPlugs.Count);
            else
                DrawPlug(output_plug.Value, nodeRect, m_outputPlugStyle, plug_count, node.m_outputPlugs.Count);
            ++plug_count;
        }
        if (delete_plug != null)
        {
            m_nodeGraphModel.RemoveOutputPlugFromNode(delete_plug.m_plugId, delete_plug.m_nodeId);
        }


        // drawing the node itself with the contents in it
        GUI.Box(nodeRect, "", style);
        float padding = 7f;
        Rect nodeContentRect = new Rect(node.m_position.x + padding, node.m_position.y + padding, node.m_dimension.x - (2 * padding), node.m_dimension.y - (2 * padding));
        BeginArea(nodeContentRect);
        Label("Dialogue", EditorStyles.boldLabel);
        DialogueData data = m_nodeGraphModel.GetDataFromNodeID(node.m_id);
        if (data != null)
        {
            Label(data.characterName);
            string tag_pattern = "<[^>]+>";
            string displayText = Regex.Replace(data.dialogueText, tag_pattern, "");
            Label(displayText, Height(node.m_dimension.y));
        }
        EndArea();
    }

    private void DrawOptionNode(Node node, GUIStyle style)
    {
        Rect nodeRect = new Rect(node.m_position.x, node.m_position.y, node.m_dimension.x, node.m_dimension.y);

        // draw plugs on specific node
        DrawPlug(node.m_inputPlug, nodeRect, m_inputPlugStyle, 1, 1);
        int plug_count = 1;
        Plug delete_plug = null;
        foreach (KeyValuePair<int, Plug> output_plug in node.m_outputPlugs)
        {
            if (delete_plug == null)
                delete_plug = DrawPlug(output_plug.Value, nodeRect, m_outputPlugStyle, plug_count, node.m_outputPlugs.Count);
            else
                DrawPlug(output_plug.Value, nodeRect, m_outputPlugStyle, plug_count, node.m_outputPlugs.Count);
            ++plug_count;
        }
        if (delete_plug != null)
        {
            m_nodeGraphModel.RemoveOutputPlugFromNode(delete_plug.m_plugId, delete_plug.m_nodeId);
        }


        // drawing the node itself with the contents in it
        GUI.Box(nodeRect, "", style);
        float padding = 7f;
        Rect nodeContentRect = new Rect(node.m_position.x + padding, node.m_position.y + padding, node.m_dimension.x - (2 * padding), node.m_dimension.y - (2 * padding));
        BeginArea(nodeContentRect);
        Label("Option", EditorStyles.boldLabel);

        int currIndex = 0;
        OutputPlugToNode outputdata;
        foreach (var outplug in node.m_outputPlugs)
        {
            outputdata = m_nodeGraphModel.GetOutputPlugToNode(node.m_id, currIndex);
            if (outputdata != null)
            {
                DialogueData inputNodeData = m_nodeGraphModel.GetDataFromNodeID(outputdata.inputNodeID);
                if (inputNodeData != null)
                    inputNodeData.branchingIndex = currIndex;
            }
            ++currIndex;
        }

        DialogueData data = m_nodeGraphModel.GetDataFromNodeID(node.m_id);
        string tag_pattern = "<[^>]+>";
        if (data.m_nextDialogueData != null)
        {
            foreach(var nodeId in data.m_nextDialogueData)
            {
                DialogueData nextData = m_nodeGraphModel.GetDataFromNodeID(nodeId);
                if (nextData != null)
                {
                    string previewData = Regex.Replace(nextData.previewDialogueText, tag_pattern, "");
                    Label(previewData);
                    Label("");
                }
            }
        }
        EndArea();
    }

    private void DrawStartNode(Node node, GUIStyle style)
    {
        if (node.isConditionalNode)
        {
            Rect nodeRect = new Rect(node.m_position.x, node.m_position.y, node.m_dimension.x, node.m_dimension.y);

            // draw plugs on specific node
            int plug_count = 1;
            foreach (KeyValuePair<int, Plug> output_plug in node.m_outputPlugs)
            {
                DrawConditionalOutPlug(output_plug.Value, nodeRect, m_outputPlugStyle, plug_count, node.m_outputPlugs.Count);
                ++plug_count;
            }


            // drawing the node itself with the contents in it
            GUI.Box(nodeRect, "", style);
            float padding = 7f;
            Rect nodeContentRect = new Rect(node.m_position.x + padding, node.m_position.y + padding, node.m_dimension.x - (2 * padding), node.m_dimension.y - (2 * padding));
            BeginArea(nodeContentRect);
            DialogueData data = m_nodeGraphModel.GetDataFromNodeID(node.m_id);
            Label("Conditional", EditorStyles.boldLabel);
            Label("Start Node", EditorStyles.boldLabel);
            EndArea();
        }
        else
        {
            Rect nodeRect = new Rect(node.m_position.x, node.m_position.y, node.m_dimension.x, node.m_dimension.y);

            // draw plugs on specific node
            int plug_count = 1;
            foreach (KeyValuePair<int, Plug> output_plug in node.m_outputPlugs)
            {
                DrawPlug(output_plug.Value, nodeRect, m_outputPlugStyle, plug_count, node.m_outputPlugs.Count);
                ++plug_count;
            }

            // drawing the node itself with the contents in it
            GUI.Box(nodeRect, "", style);
            float padding = 7f;
            Rect nodeContentRect = new Rect(node.m_position.x + padding, node.m_position.y + padding, node.m_dimension.x - (2 * padding), node.m_dimension.y - (2 * padding));
            BeginArea(nodeContentRect);
            //DialogueData data = m_nodeGraphModel.GetDataFromNodeID(node.m_id);
            Label("Start Node", EditorStyles.boldLabel);
            EndArea();
        }
    }

    private void DrawConditionalNode(Node node, GUIStyle style)
    {
        Rect nodeRect = new Rect(node.m_position.x, node.m_position.y, node.m_dimension.x, node.m_dimension.y);

        // draw plugs on specific node
        DrawPlug(node.m_inputPlug, nodeRect, m_inputPlugStyle, 1, 1);
        Plug trueOutput = node.GetOutputPlugAtIndex(0);
        Plug falseOutput = node.GetOutputPlugAtIndex(1);

        DrawConditionalOutPlug(trueOutput, nodeRect, m_outputTruePlugStyle, 1, node.m_outputPlugs.Count);
        DrawConditionalOutPlug(falseOutput, nodeRect, m_outputFalsePlugStyle, 2, node.m_outputPlugs.Count);

        // drawing the node itself with the contents in it
        GUI.Box(nodeRect, "", style);
        float padding = 7f;
        Rect nodeContentRect = new Rect(node.m_position.x + padding, node.m_position.y + padding, node.m_dimension.x - (2 * padding), node.m_dimension.y - (2 * padding));
        BeginArea(nodeContentRect);
        DialogueData data = m_nodeGraphModel.GetDataFromNodeID(node.m_id);
        if (data != null)
        {
            var rightAlign = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.UpperRight };
            Label("Conditional", EditorStyles.boldLabel);
            Label("True", rightAlign);
            Label("");
            string tag_pattern = "<[^>]+>";
            string displayText = Regex.Replace(data.dialogueText, tag_pattern, "");
            Label("False", rightAlign);
        }

        OutputPlugToNode outputdata = m_nodeGraphModel.GetOutputPlugToNode(node.m_id, 0);
        if (outputdata != null)
        {
            DialogueData inputNodeData = m_nodeGraphModel.GetDataFromNodeID(outputdata.inputNodeID);
            inputNodeData.branchingIndex = 1;
        }
        outputdata = m_nodeGraphModel.GetOutputPlugToNode(node.m_id, 1);
        if (outputdata != null)
        {
            DialogueData inputNodeData = m_nodeGraphModel.GetDataFromNodeID(outputdata.inputNodeID);
            inputNodeData.branchingIndex = 0;
        }

        EndArea();
    }

    private void OnClickInPlug(Plug in_plug)
    {
        m_currentSelectedInPlug = in_plug;
        if (m_currentSelectedOutPlug != null)
        {
            if (m_currentSelectedInPlug.m_nodeId != m_currentSelectedOutPlug.m_nodeId)
            {
                CreateConnection();
            }
            ClearConnectionSelection();
        }
    }

    private void OnClickOutPlug(Plug out_plug)
    {
        //outplug clicked when there is already an output
        Connection remove_connection = null;
        foreach (KeyValuePair<int, Connection> connection_pair in m_nodeGraphModel.GetConnections())
        {
            Connection connection = connection_pair.Value;
            if (connection.m_outputPlugId == out_plug.m_plugId)
            {
                remove_connection = connection;
                break;
            }
        }
        if (remove_connection != null)
        {
            Node input_node = m_nodeGraphModel.GetNodeFromID(remove_connection.m_inputNodeId);

            if (m_currentSelectedInPlug != null)
            {
                m_currentSelectedOutPlug = out_plug;
                CreateConnection();
            }
            else
            {
                m_currentSelectedInPlug = input_node.m_inputPlug;
                m_currentSelectedOutPlug = null;
            }

            if (m_currentSelectedInPlug != null && input_node.m_inputPlug.m_plugId == m_currentSelectedInPlug.m_plugId)
                m_nodeGraphModel.RemoveConnection(remove_connection.m_id);
            return;
        }

        // no connection on the plug yet
        m_currentSelectedOutPlug = out_plug;
        if (m_currentSelectedInPlug != null)
        {
            if (m_currentSelectedInPlug.m_nodeId != m_currentSelectedOutPlug.m_nodeId)
            {
                CreateConnection();
            }
            ClearConnectionSelection();
        }
    }

    private Plug DrawPlug(Plug plug, Rect node_rect, GUIStyle style, int plug_count, int total_count)
    {
        Rect plug_rect = new Rect(0, 0, m_plugDimensions.x, m_plugDimensions.y);
        plug_rect.y = node_rect.y + ((node_rect.height / (total_count + 1)) + (in_between_plug_height * (plug_count - 1)) + (plug_height * (plug_count - 1))) - plug_rect.height * 0.5f;
        switch (plug.m_plugType)
        {
            case PlugType.kIn: // position is on left side of the node
                plug_rect.x = node_rect.x - plug_rect.width + 8f;
                break;
            case PlugType.kOut:
                plug_rect.x = node_rect.x + node_rect.width - 8f;
                break;
        }

        plug.m_position = plug_rect.center;
        if (GUI.Button(plug_rect, "", style))
        {
            Event e = Event.current;
            if (e.button == 1)
            {
                return plug;
            }

            if (plug.m_plugType == PlugType.kIn)
            {
                OnClickInPlug(plug);
            }
            else
            {
                OnClickOutPlug(plug);
            }
        }
        return null;
    }

    private void DrawConditionalOutPlug(Plug plug, Rect node_rect, GUIStyle style, int plug_count, int total_count)
    {
        Rect plug_rect = new Rect(0, 0, m_plugDimensions.x, m_plugDimensions.y);
        plug_rect.y = node_rect.y + ((node_rect.height / (total_count + 1)) + ((in_between_plug_height + 3f)* (plug_count - 1)) + (plug_height * (plug_count - 1))) - plug_rect.height * 0.5f;
        switch (plug.m_plugType)
        {
            case PlugType.kIn: // position is on left side of the node
                plug_rect.x = node_rect.x - plug_rect.width + 8f;
                break;
            case PlugType.kOut:
                plug_rect.x = node_rect.x + node_rect.width - 8f;
                break;
        }

        plug.m_position = plug_rect.center;
        if (GUI.Button(plug_rect, "", style))
        {
            OnClickOutPlug(plug);
        }
    }
}
