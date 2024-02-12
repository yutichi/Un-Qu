using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    //SE
    public AudioClip _healSE,
                     _moneyLevelSE,
                     _summonButtonSE,
                     _selectDifficultSE,
                     _pointInSE,
                     _startButtonSE,
                     _explosionSE,
                     _gameOverBGM,
                     _clearBGM;
    

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
