using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Camera))]
public class PostEffectsBase : MonoBehaviour
{
    [SerializeField]
    protected Material postEffectMaterial;

    protected void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, postEffectMaterial);
    }
}
