using UnityEngine;

public class LightManager : MonoBehaviour
{

    public Light light;
    public Weather weather;

    private void Start()
    {
        light = GetComponent<Light>();
    }
    public void StartEffect()
    {
        if(weather.isRainy)
        {
            light.enabled = false;
            Invoke("StopEffect", 3f);
            
        }

    }

    void StopEffect()
    {
        light.enabled = true;
    }

}
