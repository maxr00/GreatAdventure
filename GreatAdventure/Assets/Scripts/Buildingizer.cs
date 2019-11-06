using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Buildingizer : MonoBehaviour
{
    public GameObject cornerBricks;
    public float repeatsPerScale = 1;
    public float bricksInset = 0.3f;
    public float brickYOff = 0.5f;

    public GameObject window;
    public float windowsPerScale = 0.2f;

    public float floor1WindowsY = 1.5f;
    public float floorWindowDistance = 6;

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
        float boundsOverF1 = Mathf.Max(transform.position.y + maxBound.y - floor1WindowsY, 0);

        Debug.Log(boundsOverF1 / floorWindowDistance);

        Vector3 numWindows = new Vector3Int(
            (int)(windowsPerScale * scale.x), 
            (int)Mathf.Max( Mathf.Round(boundsOverF1 / floorWindowDistance), 1), 
            (int)(windowsPerScale * scale.z));
        Vector3 windowDist = new Vector3(scale.x / numWindows.x, 0, scale.z / numWindows.z);
        Vector3 start = new Vector3(-maxBound.x + windowDist.x / 2, 0, -maxBound.z + windowDist.z / 2);

        for(int y = 0; y < numWindows.y; y++)
        {
            float yp = floor1WindowsY + y * floorWindowDistance - transform.position.y;
            for(int x = 0; x < numWindows.x; x++)
            {
                GameObject.Instantiate(window, transform.position + new Vector3(start.x + x * windowDist.x, yp, maxBound.z), Quaternion.Euler(0, 180, 0), parent.transform);
                GameObject.Instantiate(window, transform.position + new Vector3(start.x + x * windowDist.x, yp, -maxBound.z), Quaternion.Euler(0, 0, 0), parent.transform);
            }
            for (int z = 0; z < numWindows.z; z++)
            {
                GameObject.Instantiate(window, transform.position + new Vector3(maxBound.x, yp, start.z + z * windowDist.z), Quaternion.Euler(0, 270, 0), parent.transform);
                GameObject.Instantiate(window, transform.position + new Vector3(-maxBound.x, yp, start.z + z * windowDist.z), Quaternion.Euler(0, 90, 0), parent.transform);
            }
        }

    }
}
