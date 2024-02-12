using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class ClearAndGameOverFade : MonoBehaviour
{
    public Image fadePanel;             // �t�F�[�h�p��UI�p�l���iImage�j
    public float fadeDuration = 1.0f;   // �t�F�[�h�̊����ɂ����鎞��

    public static Image FadePanel;
    public static float FadeDuration;

    [SerializeField]
    private GameObject _fadePanel;

    void Start()
    {
        _fadePanel.SetActive(true);
        FadeInTrigger();
    }

    void Awake()
    {
        FadeDuration = fadeDuration;
        FadePanel = fadePanel;
    }


    //�t�F�[�h�A�E�g���s
    public void FadeOutTrigger()
    {
        StartCoroutine(FadeOut());
    }

    //�t�F�[�h�C�����s
    public void FadeInTrigger()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeOutLoadTrigger()
    {
        StartCoroutine(FadeOutLoading());
    }

    public void FadeInLoadTrigger()
    {
        StartCoroutine(FadeInLoading());
    }

    public static IEnumerator FadeOut()
    {
        FadePanel.enabled = true;       
        float elapsedTime = 0.0f;                
        Color startColor = FadePanel.color;       
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f); 

       
        while (elapsedTime < FadeDuration)
        {
            elapsedTime += Time.deltaTime;                      
            float t = Mathf.Clamp01(elapsedTime / FadeDuration);  
            FadePanel.color = Color.Lerp(startColor, endColor, t); 
            yield return null;                                     
        }

        FadePanel.color = endColor;  
        FadePanel.enabled = false;   

    }

    public static IEnumerator FadeIn()
    {
        FadePanel.enabled = true;       
        float elapsedTime = 0.0f;           
        Color startColor = FadePanel.color;     
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);

      
        while (elapsedTime < FadeDuration)
        {
            elapsedTime += Time.deltaTime;                        
            float t = Mathf.Clamp01(elapsedTime / FadeDuration);  
            FadePanel.color = Color.Lerp(startColor, endColor, t); 
            yield return null;                                     
        }

        FadePanel.color = endColor;  
        FadePanel.enabled = false;�@ 
    }

    public static IEnumerator FadeOutLoading()
    {
        FadePanel.enabled = true;          
        float elapsedTime = 0.0f;              
        Color startColor = FadePanel.color;       
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f); 

       
        while (elapsedTime < FadeDuration)
        {
            elapsedTime += Time.deltaTime;                        
            float t = Mathf.Clamp01(elapsedTime / FadeDuration);  
            FadePanel.color = Color.Lerp(startColor, endColor, t); 
            yield return null;                                   
        }

        FadePanel.color = endColor; 
    }

    public static IEnumerator FadeInLoading()
    {
        FadePanel.enabled = true;             
        float elapsedTime = 0.0f;                
        Color startColor = FadePanel.color;       
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f); 

        
        while (elapsedTime < FadeDuration)
        {
            elapsedTime += Time.deltaTime;                        
            float t = Mathf.Clamp01(elapsedTime / FadeDuration);  
            FadePanel.color = Color.Lerp(startColor, endColor, t); 
            yield return null;                                    
        }

        FadePanel.color = endColor;  
    }
}
