using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DialogueAssetBuilder : ScriptableObject
{
    public NodeGraphModel m_nodeGraphModel;
    public DialogueAsset m_dialogueAsset;

    class NextDialogueDataContent
    {
        public int branchingIndex;
        public int nextNodeIndex;
    }

    public DialogueAssetBuilder()
    {
        m_nodeGraphModel = new NodeGraphModel();
    }

    public void OnEnable()
    {
        m_dialogueAsset = CreateInstance<DialogueAsset>();
        m_dialogueAsset.m_dialogueData = new Dictionary<int, DialogueData>();
    }

    public void SaveAsset(NodeGraphModel model_data)
    {
        m_dialogueAsset.m_assetData = "";
        // save connections
        m_dialogueAsset.m_assetData += model_data.GetConnections().Count + "`";
        foreach (KeyValuePair<int, Connection> connection_pair in model_data.GetConnections())
        {
            Connection connection = connection_pair.Value;
            m_dialogueAsset.m_assetData += connection.m_id + "`";
            m_dialogueAsset.m_assetData += connection.m_inputNodeId + "`";
            m_dialogueAsset.m_assetData += connection.m_outputNodeId + "`";
            m_dialogueAsset.m_assetData += connection.m_inputPlugId + "`";
            m_dialogueAsset.m_assetData += connection.m_outputPlugId + "`";
        }

        // save nodes
        m_dialogueAsset.m_assetData += model_data.GetNodes().Count + "`";
        foreach (KeyValuePair<int, Node> node_pair in model_data.GetNodes())
        {
            Node node = node_pair.Value;
            m_dialogueAsset.m_assetData += node.m_id + "`";
            m_dialogueAsset.m_assetData += node.m_position.x + "`" + node.m_position.y + "`";
            m_dialogueAsset.m_assetData += node.m_dimension.x + "`" + node.m_dimension.y + "`";
            m_dialogueAsset.m_assetData += node.isConditionalNode + "`";
            SavePlugData(node.m_inputPlug);
            m_dialogueAsset.m_assetData += node.m_outputPlugs.Count + "`";
            foreach (KeyValuePair<int, Plug> plug_pair in node.m_outputPlugs)
            {
                Plug plug = plug_pair.Value;
                SavePlugData(plug);
            }
        }

        //clear current node assets in dialogue asset


        // save dialogue data
        ClearAsset();
        PopulateDialogueList(model_data);
        SaveBuiltDialogueData(model_data);

        EditorUtility.SetDirty(m_dialogueAsset); // tells unity to data in this asset has changed and needs to saved if user asks
        //AssetDatabase.SaveAssets();
    }

    private void ClearAsset()
    {
        foreach (DialogueData data in m_dialogueAsset.m_runtimeBuiltData)
        {
            AssetDatabase.RemoveObjectFromAsset(data);
        }
        m_dialogueAsset.m_runtimeBuiltData = new List<DialogueData>();
    }

    private void SavePlugData(Plug plug)
    {
        m_dialogueAsset.m_assetData += plug.m_nodeId + "`";
        m_dialogueAsset.m_assetData += plug.m_plugId + "`";
        m_dialogueAsset.m_assetData += plug.m_plugIndex + "`";
    }

    private void SaveBuiltDialogueData(NodeGraphModel model_data)
    {
        m_dialogueAsset.m_runtimeBuiltData = new List<DialogueData>();
        int start_node_index = 0;
        foreach (KeyValuePair<int, DialogueData> pair in m_dialogueAsset.m_dialogueData)
        {
            DialogueData dialogue = pair.Value;
            if (dialogue.m_isStartNode)
                start_node_index = dialogue.node_id;

            m_dialogueAsset.m_runtimeBuiltData.Add(dialogue);
            dialogue.name = dialogue.node_id.ToString();
            AssetDatabase.AddObjectToAsset(dialogue, m_dialogueAsset);
            Debug.Log(AssetDatabase.GetAssetPath(dialogue));
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(dialogue));
            EditorUtility.SetDirty(dialogue);

        }
        m_dialogueAsset.m_startIndex = start_node_index;
        m_dialogueAsset.m_charactersInvolvedStrings = m_dialogueAsset.GetInvolvedCharacterStrings();
    }

    private void PopulateDialogueList(NodeGraphModel model_data)
    {
        m_dialogueAsset.m_dialogueData = new Dictionary<int, DialogueData>();

        foreach (KeyValuePair<int, DialogueData> dialogue_pair in model_data.GetDialogueData())
        {
            List<NextDialogueDataContent> nextData = new List<NextDialogueDataContent>();
            DialogueData dialogue = dialogue_pair.Value;
            // populating next branch ids
            dialogue.m_isBranching = model_data.GetNodeFromID(dialogue.node_id).m_outputPlugs.Count > 1;
            dialogue.m_nextDialogueData = new List<int>();
            foreach (KeyValuePair<int, Connection> connection_pair in model_data.GetConnections())
            {
                Connection connection = connection_pair.Value;
                if (connection.m_outputNodeId == dialogue.node_id)
                {
                    // adding branching indices
                    DialogueData next_data = model_data.GetDataFromNodeID(connection.m_inputNodeId);
                    NextDialogueDataContent nextDataContent = new NextDialogueDataContent();
                    nextDataContent.branchingIndex = next_data.branchingIndex;
                    nextDataContent.nextNodeIndex = connection.m_inputNodeId;
                    nextData.Add(nextDataContent);
                }
            }

            nextData.Sort(delegate (NextDialogueDataContent c1, NextDialogueDataContent c2) { return c1.branchingIndex.CompareTo(c2.branchingIndex); });
            foreach(NextDialogueDataContent next_data in nextData)
            {
                Debug.Log(next_data.branchingIndex);
                dialogue.m_nextDialogueData.Add(next_data.nextNodeIndex);
            }

            // sort the next dialogue data by index;
            m_dialogueAsset.m_dialogueData.Add(dialogue.node_id, dialogue);
        }
    }

    public void LoadEditorSaveData()
    {
        m_nodeGraphModel = new NodeGraphModel();

        string[] data = m_dialogueAsset.m_assetData.Split("`".ToCharArray());
        int data_index = 0;
        if (data.Length <= 1) // no data in the asset at the moment
            return;

        // load connections
        int connectionCount = int.Parse(data[data_index++]);
        for (int connection_index = 0; connection_index < connectionCount; ++connection_index)
        {
            Connection connection = new Connection();
            connection.m_id = int.Parse(data[data_index++]);
            connection.m_inputNodeId = int.Parse(data[data_index++]);
            connection.m_outputNodeId = int.Parse(data[data_index++]);
            connection.m_inputPlugId = int.Parse(data[data_index++]);
            connection.m_outputPlugId = int.Parse(data[data_index++]);
            m_nodeGraphModel.AddConnection(connection);
        }

        // load nodes
        int node_count = int.Parse(data[data_index++]);
        for (int node_index = 0; node_index < node_count; ++node_index)
        {
            Node node = new Node();
            node.m_id = int.Parse(data[data_index++]);
            node.m_position = new Vector2();
            node.m_position.x = float.Parse(data[data_index++]); node.m_position.y = float.Parse(data[data_index++]);
            node.m_dimension = new Vector2();
            node.m_dimension.x = float.Parse(data[data_index++]); node.m_dimension.y = float.Parse(data[data_index++]);
            node.isConditionalNode = bool.Parse(data[data_index++]);
            node.m_inputPlug = LoadInputPlug(data, ref data_index, PlugType.kIn);
            int output_plug_count = int.Parse(data[data_index++]);
            node.m_outputPlugs = new Dictionary<int, Plug>();
            for (int i = 0; i < output_plug_count; ++i)
            {
                Plug out_plug = LoadInputPlug(data, ref data_index, PlugType.kOut);
                node.m_outputPlugs.Add(out_plug.m_plugId, out_plug);
            }
            m_nodeGraphModel.AddNode(node);
        }

        // edit here when dialogue data changes
        for (int dialogue_index = 0; dialogue_index < m_dialogueAsset.m_runtimeBuiltData.Count; ++dialogue_index)
        {
            DialogueData dialogue = m_dialogueAsset.m_runtimeBuiltData[dialogue_index];
            m_nodeGraphModel.AddDialogueData(dialogue);
        }

        //updating involved characters gameobject list
        m_dialogueAsset.LoadCharactersInvolvedGameObjects();
    }

    private Plug LoadInputPlug(string[] data, ref int data_index, PlugType type)
    {
        Plug plug = new Plug();
        plug.m_nodeId = int.Parse(data[data_index++]);
        plug.m_plugId = int.Parse(data[data_index++]);
        plug.m_plugIndex = int.Parse(data[data_index++]);
        plug.m_plugType = type;
        return plug;
    }
}
