using UnityEngine;

public class Audio_Script: MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip newClip;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Time.time>5&audioSource.clip!=newClip)
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }
}
