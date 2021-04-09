using UnityEngine;
using UnityEngine.UI;

public class PostProcessor : MonoBehaviour
{
    private Material backup = null;
    public Slider slider;
    public Slider sliderResA;
    public Material material;
    public Material clearMat;
    // Use this for initialization
    void Start()
    {
        backup = material;
        StartGame(); 
    }

    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetTexture("_MainTex", source);
        WARP();
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



    public void WARP()
    {
        float amount = slider.value;
        material.SetVector("warp",new Vector4(amount,-amount*0.2f,0,0));
        
    }



}
