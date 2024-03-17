using UnityEngine;

public class Weather : MonoBehaviour
{
    public ParticleSystem particle;
    public bool isRainy;


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
        Invoke("StopEffect", 3f);
    }

    void StopEffect()
    {
        isRainy = false;
        particle.gameObject.SetActive(false);
    }
}
