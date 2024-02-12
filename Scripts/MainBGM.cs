using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBGM : MonoBehaviour
{
    [SerializeField] 
    private AudioSource AudioSource;

    //çƒê∂
    public void Play()
    {
        AudioSource.Play();
    }

    //í‚é~
    public void Stop()
    {
        AudioSource.Stop();
    }
}
