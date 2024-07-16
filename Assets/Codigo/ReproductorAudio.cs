using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ReproductorAudio : MonoBehaviour
{
    public AudioClip Audio;
    public bool PonerAlEmpezar;
    public bool DebeRepetirse;
    AudioSource Reproductor;

    private void Awake()
    {
        Reproductor = GetComponent<AudioSource>();
    }
    
    void Start()
    {
       if(PonerAlEmpezar)
        {
            PonerAudio();
        }
    }
    public void PonerAudio()
    {
        Reproductor.clip = Audio;
        Reproductor.loop = DebeRepetirse;
        Reproductor.Play();
    }
}
