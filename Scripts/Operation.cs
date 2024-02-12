using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Operation : MonoBehaviour
{
    public GameManager _gameManager;

    [SerializeField]
    public GameObject _targetButton;

    [SerializeField] 
    public Vector3 _summonPos;

    [SerializeField]
    public float _summonPosX, _summonPosY, _summonPosZ;

    // Start is called before the first frame update
    void Start()
    {
        _targetButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChoiceArea()
    {
        GameObject choiceButton = GameObject.Find("Establishment");

        Button btn = choiceButton.GetComponent<Button>();

        btn.image.color = Color.white;

        _gameManager._summonButtonMG.gameObject.SetActive(true);
        _gameManager._summonButtonSG.gameObject.SetActive(true);

        _summonPos = new Vector3(_summonPosX, _summonPosY, _summonPosZ);
    }

}
