using UnityEngine;

public class LightManager : MonoBehaviour
{

    public Light light;
    public Weather weather;

    private void Start()
    {
        light = GetComponent<Light>();
    }
    void StartEffect()
    {
        if(weather.isRainy)
        {
            Invoke("StopEffect", 3f);
            light.enabled = false;
        }

    }

    void StopEffect()
    {
        light.enabled = true;
    }

}
