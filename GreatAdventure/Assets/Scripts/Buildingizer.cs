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
    public float windowClearance = 5;

    public float floor1WindowsY = 1.5f;
    public float floorWindowDistance = 6;

    public GameObject door;
    public Direction doorForward = Direction.Z_NEG;
    public float doorHeight = 1.75f;
    public int doorLevel = 0;

    public enum Direction { X_POS, X_NEG, Z_POS, Z_NEG };

    public void Buildingize()
    {
        // Delete previous
        if(transform.Find("AutoGen"))
        {
            DestroyImmediate(transform.Find("AutoGen").gameObject);
        }
        GameObject parent = new GameObject();
        parent.transform.parent = transform;
        parent.name = "AutoGen";

        parent.transform.localScale = new Vector3(1 / transform.lossyScale.x, 1 / transform.lossyScale.y, 1 / transform.lossyScale.z);

        // Get bounds
        Vector3 scale = transform.lossyScale;
        Vector3 maxBound = transform.lossyScale * 0.5f;

        // Corner Bricks
        GameObject[] bricks = new GameObject[]
        {
            GameObject.Instantiate(cornerBricks, transform.position + new Vector3( maxBound.x - bricksInset, -maxBound.y + brickYOff,  maxBound.z - bricksInset), Quaternion.Euler(0,0,0), parent.transform),
            GameObject.Instantiate(cornerBricks, transform.position + new Vector3( maxBound.x - bricksInset, -maxBound.y + brickYOff, -maxBound.z +  bricksInset), Quaternion.Euler(0,90,0), parent.transform),
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

        int doorIndex = Random.Range(0, (int)((doorForward == Direction.X_POS || doorForward == Direction.X_NEG) ? numWindows.x : numWindows.z) );

        for(int y = 0; y < numWindows.y; y++)
        {
            float yp = floor1WindowsY + y * floorWindowDistance - transform.position.y;
            for(int x = 0; x < numWindows.x; x++)
            {
                if (y == doorLevel && x == doorIndex && doorForward == Direction.Z_POS)
                    SpawnDoor(new Vector3(start.x + x * windowDist.x, doorHeight, maxBound.z), Direction.Z_POS, parent);
                else
                    SpawnWindow(new Vector3(start.x + x * windowDist.x, yp, maxBound.z), Direction.Z_POS, parent);

                if (y == doorLevel && x == doorIndex && doorForward == Direction.Z_NEG)
                    SpawnDoor(new Vector3(start.x + x * windowDist.x, doorHeight, -maxBound.z), Direction.Z_NEG, parent);
                else
                    SpawnWindow(new Vector3(start.x + x * windowDist.x, yp, -maxBound.z), Direction.Z_NEG, parent);
            }
            for (int z = 0; z < numWindows.z; z++)
            {
                if (y == doorLevel && z == doorIndex && doorForward == Direction.X_NEG)
                    SpawnDoor(new Vector3(-maxBound.x, doorHeight, start.z + z * windowDist.z), Direction.X_NEG, parent);
                else
                    SpawnWindow(new Vector3(-maxBound.x, yp, start.z + z * windowDist.z), Direction.X_NEG, parent);

                if (y == doorLevel && z == doorIndex && doorForward == Direction.X_POS)
                    SpawnDoor(new Vector3(maxBound.x, doorHeight, start.z + z * windowDist.z), Direction.X_POS, parent);
                else
                    SpawnWindow(new Vector3(maxBound.x, yp, start.z + z * windowDist.z), Direction.X_POS, parent);
            }
        }
    }

    void SpawnDoor(Vector3 position, Direction dir, GameObject parent)
    {
        Vector3 p = transform.position + position;
        GameObject.Instantiate(door, new Vector3(p.x, doorHeight + doorLevel * floorWindowDistance , p.z), DirToRot(dir), parent.transform);
    }

    void SpawnWindow(Vector3 position, Direction dir, GameObject parent)
    {
        if(!Physics.Raycast(transform.position + position, DirToVector(dir), windowClearance))
        {
            GameObject.Instantiate(window, transform.position + position, DirToRot(dir), parent.transform);
        }
    }

    static Quaternion DirToRot(Direction forward)
    {
        switch(forward)
        {
            case Direction.X_NEG: return Quaternion.Euler(0, 90 , 0);
            case Direction.X_POS: return Quaternion.Euler(0, 270, 0);
            case Direction.Z_NEG: return Quaternion.Euler(0, 0  , 0);
            case Direction.Z_POS: return Quaternion.Euler(0, 180, 0);
        }
        return Quaternion.identity;
    }

    static Vector3 DirToVector(Direction forward)
    {
        switch(forward)
        {
            case Direction.X_NEG: return new Vector3(-1, 0, 0);
            case Direction.X_POS: return new Vector3( 1, 0, 0);
            case Direction.Z_NEG: return new Vector3( 0, 0,-1);
            case Direction.Z_POS: return new Vector3( 0, 0, 1);
        }
        return Vector3.zero;
    }
}
