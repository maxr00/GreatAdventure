using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class GlowComposite : MonoBehaviour
{
    [Range(0, 10)]
    public float Intensity = 2;

    private Material compositeMat;

    void OnEnable()
    {
        compositeMat = new Material(Shader.Find("Hidden/GlowComposite"));
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        compositeMat.SetFloat("_Intensity", Intensity);
        Graphics.Blit(source, destination, compositeMat, 0);
    }
}
