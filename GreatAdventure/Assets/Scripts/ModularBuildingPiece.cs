using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ModularBuildingPiece : MonoBehaviour
{
    [System.Serializable]
    public enum Mode { Single, ThreeSlice, NineSlice };

    public bool locked = false;
    public Vector3Int repeats = new Vector3Int(0, 0, 0);

    [Header("General")]
    public MeshFilter filter;
    public Mode mode;
    public bool switchHorizontalAxes = false;
    public Vector3 universalScale = new Vector3(1,1,1);
    public Vector3 universalOffset = new Vector3(0, 0, 0);
    public float horizontalCurve = 0;
    public float horizontalCurveDepth = 0;
    public float verticalCurve = 0;

    [Header("Single Mesh")]
    [InspectorName("Mesh")] public Mesh singleMesh;
    public Vector3 singleRotation;

    [Header("3 Slice Mesh")]
    public ThreeSlice ThreeSlice_TopLeft;
    public ThreeSlice ThreeSlice_Horizontal;
    public MeshInstance ThreeSlice_Middle;

    [System.Serializable]
    public class ThreeSlice
    {
        public Mesh mesh;
        public Vector3 scale = new Vector3(1,1,1);
        public Vector3[] offsets = new Vector3[4];

        public enum CornerIndices { TOP_LEFT = 0, TOP_RIGHT = 1, BOTTOM_LEFT = 2, BOTTOM_RIGHT = 3 };
        public enum SideIndices { TOP = 0, BOTTOM = 1, LEFT = 2, RIGHT = 3 };
    }

    [Header("9 Slice Mesh")]
    public Vector3 spacing;
    public NineSlice.Indices favoredSide = NineSlice.Indices.TOP_HORIZONTAL;
    /*
     TL, TH, TR,
     LV, M,  RV,
     BL, BH, BR
     */
    public NineSlice[] nineSliced = new NineSlice[9]; 
    [System.Serializable]
    public class NineSlice
    {
        public Mesh mesh;
        public Vector3 scale = new Vector3(1,1,1);
        public Vector3 offset;
        public Vector3 rotation;

        public enum Indices { TOP_LEFT = 0, TOP_HORIZONTAL, TOP_RIGHT, MIDDLE_LEFT, MIDDLE, MIDDLE_RIGHT, BOTTOM_LEFT, BOTTOM_HORIZONTAL, BOTTOM_RIGHT };
    }

    [System.Serializable]
    public class MeshInstance
    {
        public Mesh mesh = null;
        public Vector3 scale = new Vector3(1,1,1);
        public Vector3 offset;
        public Vector3 rotation;
    }

    // Private vars
    bool isParent;
    bool isChild;
    Vector3Int currRepeats = new Vector3Int(1,1,1);

    void Start()
    {
        if (filter && filter.gameObject != gameObject)
        {
            if (filter.transform.parent == transform)
                isParent = true;
            else
                isChild = true;
        }
        else
            filter = GetComponent<MeshFilter>();

        if(mode == Mode.Single && singleMesh == null)
            singleMesh = filter.sharedMesh;

        if (!Application.isEditor)
            locked = true;
    }
    public void Update()
    {
        if (Application.isPlaying)
            locked = true;

        if (!Application.isEditor || locked)
            return;

        if (!filter)
        {
            Start();
            return;
        }

        if (isParent || isChild)
            filter.transform.localPosition = Vector3.zero;

        switch(mode)
        {
            case Mode.Single:    RepeatSingle(); return;
            case Mode.ThreeSlice:  Repeat3Slice(); return;
            case Mode.NineSlice: Repeat9Slice(); return;
        }
    }

    void RepeatSingle()
    {
        if (!singleMesh)
            return;

        Vector3Int meshRepeats = repeats;

        if (isParent || isChild)
            meshRepeats = new Vector3Int((int)transform.localScale.x - 1, (int)transform.localScale.y - 1, (int)transform.localScale.z - 1);

        if (isParent)
            filter.gameObject.transform.localScale = new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);


        if (meshRepeats != currRepeats || Application.isEditor)
        {
            Mesh m = new Mesh();
            List<CombineInstance> combs = new List<CombineInstance>();

            for (int x = 0; x <= meshRepeats.x; x++)
            {
                for (int y = 0; y <= meshRepeats.y; y++)
                {
                    for (int z = 0; z <= meshRepeats.z; z++)
                    {
                        CombineInstance comb = new CombineInstance();
                        comb.mesh = singleMesh;

                        float curve = meshRepeats.x == 0 ? 0 : (x / (float)meshRepeats.x - 0.5f) * (x / (float)meshRepeats.x - 0.5f);
                        float curveR = meshRepeats.x == 0 ? 0 : -2 * (x / (float)meshRepeats.x - 0.5f);

                        comb.transform = Matrix4x4.Scale(universalScale) * Matrix4x4.Translate(new Vector3(0, 0, curve * horizontalCurveDepth) + new Vector3(
                            x * (universalOffset.x + 1) * comb.mesh.bounds.size.x - comb.mesh.bounds.center.x,
                            y * (universalOffset.y + 1) * comb.mesh.bounds.size.y - comb.mesh.bounds.center.y,
                            z * (universalOffset.z + 1) * comb.mesh.bounds.size.z - comb.mesh.bounds.center.z))
                            * Matrix4x4.Rotate(Quaternion.Euler(singleRotation + new Vector3(0, horizontalCurve * curveR, 0)));

                        combs.Add(comb);
                    }
                }
            }

            m.CombineMeshes(combs.ToArray());
            filter.mesh = m;

            currRepeats = meshRepeats;
        }

    }

    void Repeat3Slice()
    {
        if (ThreeSlice_Horizontal == null)
            return; // Horizontal needed.

        Vector3Int meshRepeats = repeats;
        if (isParent || isChild)
            meshRepeats = new Vector3Int((int)transform.localScale.x - 1, (int)transform.localScale.y - 1, (int)transform.localScale.z - 1);

        if (isParent)
            filter.gameObject.transform.localScale = new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);

        if (meshRepeats != currRepeats || Application.isEditor)
        {
            Mesh m = new Mesh();
            List<CombineInstance> combs = new List<CombineInstance>();

            for (int x = 0; x <= meshRepeats.x; x++)
            {
                for (int y = 0; y <= meshRepeats.y; y++)
                {
                    for (int z = 0; z <= meshRepeats.z; z++)
                    {
                        CombineInstance comb = new CombineInstance();
                        MeshInstance mi = GetNext3SliceInstance(x,y,z, meshRepeats);
                        
                        if(mi.mesh != null)
                        {
                            comb.mesh = mi.mesh;

                            Vector3 scale = mi.scale;
                            scale.Scale(universalScale);

                            float largestAxis = Mathf.Max(ThreeSlice_Horizontal.mesh.bounds.size.x, Mathf.Max(ThreeSlice_Horizontal.mesh.bounds.size.y, ThreeSlice_Horizontal.mesh.bounds.size.z));

                            float curve = meshRepeats.x == 0 ? 0 : (x / (float)meshRepeats.x - 0.5f) * (x / (float)meshRepeats.x - 0.5f);
                            float curveR = meshRepeats.x == 0 ? 0 : -2 * (x / (float)meshRepeats.x - 0.5f);

                            comb.transform = Matrix4x4.Scale(scale) * Matrix4x4.Translate( new Vector3(0,0,curve * horizontalCurveDepth) + new Vector3(
                                x * (universalOffset.x + 1) * largestAxis + mi.offset.x - mi.mesh.bounds.center.x,
                                y * (universalOffset.y + 1) * largestAxis + mi.offset.y - mi.mesh.bounds.center.y,
                                z * (universalOffset.z + 1) * largestAxis + mi.offset.z - mi.mesh.bounds.center.z))
                                * Matrix4x4.Rotate(Quaternion.Euler(mi.rotation + new Vector3(0, curveR * horizontalCurve, 0)));

                            combs.Add(comb);
                        }

                    }
                }
            }

            m.CombineMeshes(combs.ToArray());
            filter.mesh = m;

            currRepeats = meshRepeats;
        }
    }

    void Repeat9Slice()
    {
        Vector3Int meshRepeats = repeats;
        if (isParent || isChild)
            meshRepeats = new Vector3Int((int)transform.localScale.x - 1, (int)transform.localScale.y - 1, (int)transform.localScale.z - 1);

        if (isParent)
            filter.gameObject.transform.localScale = new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);

        if (meshRepeats != currRepeats || Application.isEditor)
        {
            Mesh m = new Mesh();
            List<CombineInstance> combs = new List<CombineInstance>();

            for (int x = 0; x <= meshRepeats.x; x++)
            {
                for (int y = 0; y <= meshRepeats.y; y++)
                {
                    for (int z = 0; z <= meshRepeats.z; z++)
                    {
                        CombineInstance comb = new CombineInstance();
                        NineSlice ns = GetNext9SliceInstance(x, y, z, meshRepeats);

                        if (ns.mesh != null)
                        {
                            comb.mesh = ns.mesh;

                            Vector3 scale = ns.scale;
                            scale.Scale(universalScale);

                            Vector3 spacing = this.spacing;
                            if (spacing.sqrMagnitude == 0)
                            {
                                Mesh reference = nineSliced[(int)favoredSide].mesh;
                                if (reference == null)
                                    for (int i = 0; i < 9; i++)
                                        if (nineSliced[i].mesh != null)
                                            reference = nineSliced[i].mesh;

                                //float largestAxis = Mathf.Max(reference.bounds.size.x, Mathf.Max(reference.bounds.size.y, reference.bounds.size.z));

                                spacing = reference.bounds.size;
                            }

                            float curve = meshRepeats.x == 0 ? 0 : (x / (float)meshRepeats.x - 0.5f) * (x / (float)meshRepeats.x - 0.5f);
                            float curveR = meshRepeats.x == 0 ? 0 : -2 * (x / (float)meshRepeats.x - 0.5f);

                            comb.transform = Matrix4x4.Scale(scale) * Matrix4x4.Translate(new Vector3(0, 0, curve * horizontalCurveDepth) + new Vector3(
                                x * (universalOffset.x + 1) * spacing.x + ns.offset.x - ns.mesh.bounds.center.x,
                                y * (universalOffset.y + 1) * spacing.y + ns.offset.y - ns.mesh.bounds.center.y,
                                z * (universalOffset.z + 1) * spacing.z + ns.offset.z - ns.mesh.bounds.center.z))
                                * Matrix4x4.Rotate(Quaternion.Euler(ns.rotation + new Vector3(0, horizontalCurve * curveR, 0)));

                            combs.Add(comb);
                        }

                    }
                }
            }

            m.CombineMeshes(combs.ToArray());
            filter.mesh = m;

            currRepeats = meshRepeats;
        }
    }

    MeshInstance GetNext3SliceInstance(int x, int y, int z, Vector3Int meshRepeats)
    {
        MeshInstance ret = new MeshInstance();

        if (y == meshRepeats.y) // Top row
        {
            if (x == 0 && z == 0) // top left
            {
                ret.mesh = ThreeSlice_TopLeft.mesh;
                ret.offset = ThreeSlice_TopLeft.offsets[(int)ThreeSlice.CornerIndices.TOP_LEFT];
                ret.rotation = new Vector3(0,0,90);
            }
            else if (x == meshRepeats.x && z == meshRepeats.z) // top right
            {
                ret.mesh = ThreeSlice_TopLeft.mesh;
                ret.offset = ThreeSlice_TopLeft.offsets[(int)ThreeSlice.CornerIndices.TOP_RIGHT];
            }
            else
            {
                ret.mesh = ThreeSlice_Horizontal.mesh;
                ret.offset = ThreeSlice_Horizontal.offsets[(int)ThreeSlice.SideIndices.TOP];
            }
        }
        else if (y == 0) // Bottom row
        {
            if (x == 0 && z == 0) // Bottom left
            {
                ret.mesh = ThreeSlice_TopLeft.mesh;
                ret.offset = ThreeSlice_TopLeft.offsets[(int)ThreeSlice.CornerIndices.BOTTOM_LEFT]; 
                ret.rotation = new Vector3(0, 0, 180);
            }
            else if (x == meshRepeats.x && z == meshRepeats.z) // bottom right
            {
                ret.mesh = ThreeSlice_TopLeft.mesh;
                ret.offset = ThreeSlice_TopLeft.offsets[(int)ThreeSlice.CornerIndices.BOTTOM_RIGHT];
                ret.rotation = new Vector3(0, 0, 270);
            }
            else
            {
                ret.mesh = ThreeSlice_Horizontal.mesh;
                ret.offset = ThreeSlice_Horizontal.offsets[(int)ThreeSlice.SideIndices.BOTTOM];
            }
        }
        else // Middle rows
        {
            if (x == 0 && z == 0) // Left
            {
                ret.mesh = ThreeSlice_Horizontal.mesh;
                ret.offset = ThreeSlice_Horizontal.offsets[(int)ThreeSlice.SideIndices.LEFT];
                ret.rotation = new Vector3(0, 0, 90);
            }
            else if (x == meshRepeats.x && z == meshRepeats.z) // Right
            {
                ret.mesh = ThreeSlice_Horizontal.mesh;
                ret.offset = ThreeSlice_Horizontal.offsets[(int)ThreeSlice.SideIndices.RIGHT];
                ret.rotation = new Vector3(0, 0, 90);
            }
            else
            {
                // Middle
                return ThreeSlice_Middle;
            }
        }

        if (ret.mesh == ThreeSlice_Horizontal.mesh)
            ret.scale = ThreeSlice_Horizontal.scale;
        else
            ret.scale = ThreeSlice_TopLeft.scale;

        return ret;
    }

    NineSlice GetNext9SliceInstance(int x, int y, int z, Vector3Int meshRepeats)
    {
        if (y == meshRepeats.y && (nineSliced[0].mesh || nineSliced[1].mesh || nineSliced[2].mesh) ) // Top row (check if there are any meshes to display. Important if only displaying bottom row)
        {
            if (x == 0 && z == 0) // top left
                return nineSliced[(int)NineSlice.Indices.TOP_LEFT];
            else if (x == meshRepeats.x && z == meshRepeats.z) // top right
                return nineSliced[(int)NineSlice.Indices.TOP_RIGHT];
            else
                return nineSliced[(int)NineSlice.Indices.TOP_HORIZONTAL];
        }
        else if (y == 0) // Bottom row
        {
            if (x == 0 && z == 0) // Bottom left
                return nineSliced[(int)NineSlice.Indices.BOTTOM_LEFT];
            else if (x == meshRepeats.x && z == meshRepeats.z) // bottom right
                return nineSliced[(int)NineSlice.Indices.BOTTOM_RIGHT];
            else
                return nineSliced[(int)NineSlice.Indices.BOTTOM_HORIZONTAL];
        }
        else // Middle rows
        {
            if (x == 0 && z == 0) // Left
                return nineSliced[(int)NineSlice.Indices.MIDDLE_LEFT];
            else if (x == meshRepeats.x && z == meshRepeats.z) // Right
                return nineSliced[(int)NineSlice.Indices.MIDDLE_RIGHT];
            else
                return nineSliced[(int)NineSlice.Indices.MIDDLE];
        }
    }

}
