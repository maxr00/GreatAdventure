using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class GlowPrePass : MonoBehaviour
{
    private static RenderTexture PrePass;
    private static RenderTexture Blurred;

    private Material blurMat;
    private int currentScreenWidth;
    private int currentScreenHeight;

    void OnEnable()
    {
        CreateRenderTargets();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            ReleaseRenderTargets();
            CreateRenderTargets();
        }
    }

    private void Update()
    {
        if (currentScreenHeight != Screen.height || currentScreenWidth != Screen.width)
        {
            ReleaseRenderTargets();
            CreateRenderTargets();
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination);
        Graphics.SetRenderTarget(Blurred);
        GL.Clear(false, true, Color.clear);

        Graphics.Blit(source, Blurred);

        for (int i = 0; i < 4; ++i)
        {
            var temp = RenderTexture.GetTemporary(Blurred.width, Blurred.height);
            Graphics.Blit(Blurred, temp, blurMat, 0);
            Graphics.Blit(temp, Blurred, blurMat, 1);
            RenderTexture.ReleaseTemporary(temp);
        }
    }

    private void ReleaseRenderTargets()
    {
        PrePass.Release();
        Blurred.Release();
    }

    private void CreateRenderTargets()
    {
        currentScreenHeight = Screen.height; currentScreenWidth = Screen.width;

        PrePass = new RenderTexture(Screen.width, Screen.height, 24);
        PrePass.antiAliasing = QualitySettings.antiAliasing;
        Blurred = new RenderTexture(Screen.width >> 1, Screen.height >> 1, 0);

        var camera = GetComponent<Camera>();
        var glowShader = Shader.Find("Hidden/GlowReplace");
        camera.targetTexture = PrePass;
        camera.SetReplacementShader(glowShader, "Glowable");
        Shader.SetGlobalTexture("_GlowPrePassTex", PrePass);

        Shader.SetGlobalTexture("_GlowBlurredTex", Blurred);

        blurMat = new Material(Shader.Find("Hidden/Blur"));
        blurMat.SetVector("_BlurSize", new Vector2(Blurred.texelSize.x * 1.5f, Blurred.texelSize.y * 1.5f));
    }
}
