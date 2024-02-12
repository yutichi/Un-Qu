using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBGM : MonoBehaviour
{
    [SerializeField] 
    private AudioSource AudioSource;

    //�Đ�
    public void Play()
    {
        AudioSource.Play();
    }

    //��~
    public void Stop()
    {
        AudioSource.Stop();
    }
}
