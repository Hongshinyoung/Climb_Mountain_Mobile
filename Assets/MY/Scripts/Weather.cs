using UnityEngine;

public class Weather : MonoBehaviour
{
    public ParticleSystem particle;
    public bool isRainy;
    public RepeatMap map;
    public LightManager lightManager;


    private void Start()
    {
        isRainy = false;
        particle.gameObject.SetActive(false);
        particle = GetComponent<ParticleSystem>();
        InvokeRepeating("StartEffect", 3f, Random.Range(100f, 500f));
        
    }
    void StartEffect()
    {
        particle.gameObject.SetActive(true);
        isRainy = true;
        map.moveSpeed *= 0.3f;
        lightManager.StartEffect();
        Invoke("StopEffect", 3f);
    }

    void StopEffect()
    {
        isRainy = false;
        particle.gameObject.SetActive(false);
    }
}
