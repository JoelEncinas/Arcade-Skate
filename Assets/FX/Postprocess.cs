using System.Collections.Generic;
using UnityEngine;

public class Postprocess : MonoBehaviour
{
    [SerializeField] private DataManagerScript dataManagerScript;
    private Material _material;
    [SerializeField] private List<Shader> shaderList;

    private void Start()
    {
        CheckShader(); // check for shader on load
    }

    private void Update()
    {
        CheckShader();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _material);
    }

    private void CheckShader()
    {
        dataManagerScript.LoadGraphicsMode();

        if (dataManagerScript.graphicModeSaved == 1)
            _material = new Material(shaderList[1]);
        else
            _material = new Material(shaderList[0]);
    }
}
