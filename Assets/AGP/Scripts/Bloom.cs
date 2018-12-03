using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode/*, ImageEffectAllowedInSceneView*/]
public class Bloom : MonoBehaviour
{

    public Shader bloomShader;
    public float intensity = 1;
    public int iterations = 4;
    public float threshold = 1;

    private RenderTexture[] textures = new RenderTexture[16];
    private Material bloom;
    private const int prefilterPass = 0;
    private const int downPass = 1;
    private const int upPass = 2;
    private const int bloomPass = 3;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (bloom == null)
        {
            bloom = new Material(bloomShader)
            {
                hideFlags = HideFlags.HideAndDontSave
            };
            bloom.SetFloat("_Threshold", threshold);
            bloom.SetFloat("_Intensity", Mathf.GammaToLinearSpace(intensity));
        }

        int width = source.width / 2;
        int height = source.height / 2;
        RenderTextureFormat format = source.format;

        RenderTexture currentDestination = textures[0] =
               RenderTexture.GetTemporary(width, height, 0, format);

        // Blit using prefilter
        Graphics.Blit(source, currentDestination, bloom, prefilterPass);
        RenderTexture currentSource = currentDestination;
        int i = 1;
        // Down sampling
        for (; i < iterations; i++)
        {
            width /= 2;
            height /= 2;
            if (height < 2)
            {
                return;
            }
            currentDestination = textures[i] =
                RenderTexture.GetTemporary(width, height, 0, format);
            // Blit using down pass
            Graphics.Blit(currentSource, currentDestination, bloom, downPass);
            currentSource = currentDestination;
        }
        // Up sampling
        for (i -= 2; i >= 0; i--)
        {
            currentDestination = textures[i];
            textures[i] = null;
            // Blit using up pass
            Graphics.Blit(currentSource, currentDestination, bloom, upPass);
            RenderTexture.ReleaseTemporary(currentSource);
            currentSource = currentDestination;
        }
        bloom.SetTexture("_SourceTex", currentSource);
        // Blit using bloom pass
        Graphics.Blit(source, destination, bloom, bloomPass);
        RenderTexture.ReleaseTemporary(currentSource);
    }
}
