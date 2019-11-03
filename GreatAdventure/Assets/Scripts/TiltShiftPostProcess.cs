using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TiltShiftPostProcess : MonoBehaviour
{
    public Material tiltMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        var temporaryTexture = RenderTexture.GetTemporary(source.width, source.height);
        Graphics.Blit(source, temporaryTexture, tiltMaterial, 0);
        Graphics.Blit(temporaryTexture, destination, tiltMaterial, 1);
        RenderTexture.ReleaseTemporary(temporaryTexture);
    }
}
