using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int m_id;
    public Vector2 m_position;
    public Vector2 m_dimension = new Vector2(150, 45);
    public Plug m_inputPlug;
    public Dictionary<int, Plug> m_outputPlugs; // key = plug.m_id
    public bool isConditionalNode = false;

    public Plug GetOutputPlugAtIndex(int plugIndex)
    {
        int currIndex = 0;
        foreach(var plug_pair in m_outputPlugs)
        {
            if (plug_pair.Value.m_plugIndex == plugIndex)
                return plug_pair.Value;
            ++currIndex;
        }
        return null;
    }

    public void SortOutputPlugIds()
    {
        int currIndex = 0;
        foreach (var plug_pair in m_outputPlugs)
        {
            plug_pair.Value.m_plugIndex = currIndex;
            ++currIndex;
        }
    }
}

public enum PlugType { kIn, kOut }

public class Plug
{
    public PlugType m_plugType;
    public Vector2 m_position;
    public int m_plugId;
    public int m_nodeId;
    public int m_plugIndex; // only matters for conditional nodes
}

public class Connection
{
    public int m_id;
    public int m_inputNodeId;
    public int m_outputNodeId;
    public int m_inputPlugId;
    public int m_outputPlugId;

    public Connection()
    {

    }

    public Connection(int inputNodeId, int inputPlugId, int outputNodeId, int outputPlugId)
    {
        m_inputNodeId = inputNodeId;
        m_inputPlugId = inputPlugId;
        m_outputNodeId = outputNodeId;
        m_outputPlugId = outputPlugId;
    }
}
public class OutputPlugToNode
{
    public int outputPlugID;
    public int outputNodeID;
    public int inputNodeID;
}


public class NodeGraphModel
{
    private Dictionary<int, Node> m_nodes; // key = node.m_id
    private Dictionary<int, Connection> m_connections; // key = connection.m_id
    private Dictionary<int, DialogueData> m_dialogueData; // key = node_id	

    public float plug_height = 20.0f;
    public float in_between_plug_height = 10.0f;

    public int startNodeID = 0;

    public NodeGraphModel()
    {
        m_nodes = new Dictionary<int, Node>();
        m_connections = new Dictionary<int, Connection>();
        m_dialogueData = new Dictionary<int, DialogueData>();
    }

    public int AddNode(Vector2 position) // returns created node id;
    {
        Node newNode = new Node();
        newNode.m_id = newNode.GetHashCode();
        newNode.m_position = position;
        newNode.m_inputPlug = new Plug();
        newNode.m_inputPlug.m_nodeId = newNode.m_id;
        newNode.m_inputPlug.m_plugId = newNode.m_inputPlug.GetHashCode();
        newNode.m_inputPlug.m_plugType = PlugType.kIn;
        newNode.m_outputPlugs = new Dictionary<int, Plug>();
        DialogueData dialogue = ScriptableObject.CreateInstance<DialogueData>();
        dialogue.node_id = newNode.m_id;
        m_dialogueData.Add(newNode.m_id, dialogue);
        m_nodes.Add(newNode.m_id, newNode);

        return newNode.m_id;
    }

    public int AddConditionalNode(Vector2 position) // returns node id
    {
        Node newNode = new Node();
        newNode.m_id = newNode.GetHashCode();
        newNode.m_position = position;
        newNode.isConditionalNode = true;
        newNode.m_inputPlug = new Plug();
        newNode.m_inputPlug.m_nodeId = newNode.m_id;
        newNode.m_inputPlug.m_plugId = newNode.m_inputPlug.GetHashCode();
        newNode.m_inputPlug.m_plugType = PlugType.kIn;
        newNode.m_outputPlugs = new Dictionary<int, Plug>();
        m_nodes.Add(newNode.m_id, newNode);

        DialogueData dialogue = ScriptableObject.CreateInstance<DialogueData>();
        dialogue.node_id = newNode.m_id;
        dialogue.isConditionalBranching = true;
        m_dialogueData.Add(newNode.m_id, dialogue);

        int plug_id_0 = AddOutputPlugToNode(newNode.m_id);
        newNode.m_outputPlugs[plug_id_0].m_plugIndex = 0;
        int plug_id_1 = AddOutputPlugToNode(newNode.m_id);
        newNode.m_outputPlugs[plug_id_1].m_plugIndex = 1;

        return newNode.m_id;
    }


    public int DuplicateNode(int node_id_to_duplicate)
    {
        Node node_to_duplicate = GetNodeFromID(node_id_to_duplicate);
        Node duplicate = new Node();
        duplicate.m_id = duplicate.GetHashCode();
        duplicate.m_position = node_to_duplicate.m_position + new Vector2(20, 20);
        duplicate.m_inputPlug = new Plug();
        duplicate.m_inputPlug.m_nodeId = duplicate.m_id;
        duplicate.m_inputPlug.m_plugId = duplicate.m_inputPlug.GetHashCode();
        m_nodes.Add(duplicate.m_id, duplicate);

        //dialogue data
        DialogueData dialogue = GetDataFromNodeID(node_to_duplicate.m_id);
        m_dialogueData.Add(duplicate.m_id, dialogue.Copy());


        duplicate.m_outputPlugs = new Dictionary<int, Plug>();
        for (int i = 0; i < node_to_duplicate.m_outputPlugs.Count; ++i)
        {
            AddOutputPlugToNode(duplicate.m_id);
        }
        return duplicate.m_id;
    }

    public void AddNode(Node node)
    {
        m_nodes.Add(node.m_id, node);
    }

    public void RemoveNode(int node_id)
    {
        Node node_to_remove = GetNodeFromID(node_id);

        // removing connections related to the node
        List<Connection> connections_to_remove = new List<Connection>();
        foreach (KeyValuePair<int, Connection> connection_pair in m_connections)
        {
            Connection connection = connection_pair.Value;
            if (connection.m_inputNodeId == node_to_remove.m_id)
            {
                connections_to_remove.Add(connection);
            }
            else if (connection.m_outputNodeId == node_to_remove.m_id)
            {
                connections_to_remove.Add(connection);
            }
        }
        foreach (Connection connection in connections_to_remove)
        {
            RemoveConnection(connection.m_id);
        }

        // removing dialogue data assoiciated with the node
        m_dialogueData.Remove(node_id);
        // removing actual node
        m_nodes.Remove(node_id);

        if (startNodeID == node_id)
            startNodeID = 0;
    }

    public int AddOutputPlugToNode(int node_id) // returns created plug id
    {
        Plug newOutPlug = new Plug();
        newOutPlug.m_nodeId = node_id;
        newOutPlug.m_plugId = newOutPlug.GetHashCode();
        newOutPlug.m_plugType = PlugType.kOut;
        GetNodeFromID(node_id).m_outputPlugs.Add(newOutPlug.m_plugId, newOutPlug);
        GetNodeFromID(node_id).m_dimension.y += in_between_plug_height + plug_height;

        GetNodeFromID(node_id).SortOutputPlugIds();

        return newOutPlug.m_plugId;
    }

    public void RemoveOutputPlugFromNode(int output_id, int node_id)
    {
        Node node = GetNodeFromID(node_id);
        Plug plug_to_remove;
        node.m_outputPlugs.TryGetValue(output_id, out plug_to_remove);

        // find connections related to plug being removed
        List<Connection> connections_to_remove = new List<Connection>();
        foreach (KeyValuePair<int, Connection> connection_pair in m_connections)
        {
            Connection connection = connection_pair.Value;
            if (connection.m_outputPlugId == output_id)
            {
                connections_to_remove.Add(connection);
            }
        }
        // remove found connections
        foreach (Connection connection in connections_to_remove)
        {
            RemoveConnection(connection.m_id);
        }

        // remova plug itself
        node.m_outputPlugs.Remove(output_id);
        // adjust node dimensions according to new plug count
        node.m_dimension.y -= in_between_plug_height + plug_height;

        // sort output plugs ids
        node.SortOutputPlugIds();
    }

    public Node GetNodeFromID(int node_id)
    {
        Node returnVal;
        m_nodes.TryGetValue(node_id, out returnVal);
        return returnVal;
    }

    public DialogueData GetDataFromNodeID(int node_id)
    {
        DialogueData data;
        m_dialogueData.TryGetValue(node_id, out data);
        return data;
    }

    public Dictionary<int, Node> GetNodes()
    {
        return m_nodes;
    }

    public Dictionary<int, Connection> GetConnections()
    {
        return m_connections;
    }

    public Dictionary<int, DialogueData> GetDialogueData()
    {
        return m_dialogueData;
    }

    public void AddDialogueData(DialogueData dialogue)
    {
        m_dialogueData.Add(dialogue.node_id, dialogue);
    }

    public int AddConnection(int inPlugId, int outPlugId, int inNodeId, int outNodeId) // returns created connection id
    {
        Connection newConnection = new Connection(inNodeId, inPlugId, outNodeId, outPlugId);
        newConnection.m_id = newConnection.GetHashCode();
        m_connections.Add(newConnection.m_id, newConnection);
        return newConnection.m_id;
    }

    public void AddConnection(Connection connection)
    {
        m_connections.Add(connection.m_id, connection);
    }

    public void RemoveConnection(int connectionId)
    {
        m_connections.Remove(connectionId);
    }

    public OutputPlugToNode GetOutputPlugToNode(int node_id, int plug_index)
    {
        OutputPlugToNode result = new OutputPlugToNode();
        Node node = GetNodeFromID(node_id);
        Plug outPlug = node.GetOutputPlugAtIndex(plug_index);
        foreach (KeyValuePair<int, Connection> connection_pair in GetConnections())
        {
            Connection connection = connection_pair.Value;
            if (connection.m_outputNodeId == node_id && connection.m_outputPlugId == outPlug.m_plugId)
            {
                // adding branching indices
                result.inputNodeID = connection.m_inputNodeId;
                result.outputNodeID = connection.m_outputNodeId;
                result.outputPlugID = connection.m_outputPlugId;

                return result;
            }
        }

        return null;
    }
}
