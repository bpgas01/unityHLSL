using UnityEngine;
using UnityEngine.UI;

public class PostProcessor : MonoBehaviour
{
    private Material backup = null;
    public Slider sliderResA;
    public Material material;
    public Material clearMat;
    public Material WarpedSpace;
    // Use this for initialization
    void Start()
    {
        backup = material;
        StartGame(); 
    }

    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetTexture("_MainTex", source);
        RESOLUTION();
        Graphics.Blit(source, destination, material);
    }

    public void StartGame()
    {
        
    }

   public void RESOLUTION()
    {
        float amount = sliderResA.value;
        material.SetFloat("resScale", amount);

    }



}




