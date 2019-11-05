using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Buildingizer : MonoBehaviour
{
    public GameObject cornerBricks;
    public float repeatsPerScale = 1;
    public float bricksInset = 0.5f;
    public float brickYOff = 0.5f;

    public GameObject window;
    public float windowsPerScale = 0.2f;

    public void Buildingize()
    {
        // Delete previous
        if(transform.Find("AutoGen"))
        {
            DestroyImmediate(transform.Find("AutoGen").gameObject);
        }
        GameObject parent = GameObject.Instantiate(new GameObject(), transform);
        parent.name = "AutoGen";

        parent.transform.localScale = new Vector3(1 / transform.lossyScale.x, 1 / transform.lossyScale.y, 1 / transform.lossyScale.z);

        // Get bounds
        Vector3 scale = transform.lossyScale;
        Vector3 maxBound = transform.lossyScale * 0.5f;

        // Corner Bricks
        GameObject[] bricks = new GameObject[]
        {
            GameObject.Instantiate(cornerBricks, transform.position + new Vector3( maxBound.x - bricksInset, -maxBound.y + brickYOff,  maxBound.z - bricksInset), Quaternion.Euler(0,0  ,0), parent.transform),
            GameObject.Instantiate(cornerBricks, transform.position + new Vector3( maxBound.x - bricksInset, -maxBound.y + brickYOff, -maxBound.z +  bricksInset), Quaternion.Euler(0,90 ,0), parent.transform),
            GameObject.Instantiate(cornerBricks, transform.position + new Vector3(-maxBound.x + bricksInset, -maxBound.y + brickYOff, -maxBound.z +  bricksInset), Quaternion.Euler(0,180,0), parent.transform),
            GameObject.Instantiate(cornerBricks, transform.position + new Vector3(-maxBound.x + bricksInset, -maxBound.y + brickYOff,  maxBound.z - bricksInset), Quaternion.Euler(0,270,0), parent.transform),
        };
        foreach(GameObject brick in bricks)
        {
            brick.GetComponent<ModularBuildingPiece>().repeats = new Vector3Int(0, (int)(repeatsPerScale * scale.y)-1, 0);
        }

        // Windows
        Vector3 numWindows = new Vector3Int((int)(windowsPerScale * scale.x), 0, (int)(windowsPerScale * scale.z));
        for(int x = 0; x < numWindows.x; x++)
        {

        }
    }
}
