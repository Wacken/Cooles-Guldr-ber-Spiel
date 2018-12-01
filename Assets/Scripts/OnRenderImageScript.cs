using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class OnRenderImageScript : MonoBehaviour {

    public Material visorMaterial;
    private void Awake()
    {
        if(visorMaterial == null)
        {
            Debug.Log("VisorMaterial(Postprocessing shader for visor was not set properly!");
            visorMaterial = new Material(Shader.Find("Custom/VisorShader"));
        }
        if (visorMaterial == null)
        {
            Debug.Log("VisorMaterial(Postprocessing shader for visor was not set!");
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // Copy the source Render Texture to the destination,
        // applying the material along the way.
        Graphics.Blit(source, destination, visorMaterial);
    }
}
