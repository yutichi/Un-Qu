using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject _enemy;

    //�G�������ԊԊu
    private float _interval;

    private float time = 0f;

    //�X�|�[�����Ԃ͈̔�
    [SerializeField]
    private float _maxTime, _minTime;

    //�X�|�[���n�_
    [SerializeField]
    private Vector3 _spawnPos;

    public float _minPosZ = 4f;
    public float _maxPosZ = -4f;

    //�G�̏����
    [SerializeField]
    private int _spawnCount;
    public static int SpawnCount;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;

        _interval = RandomSpawnTime();
        Debug.Log(RandomSpawnTime());

        _spawnCount = 0;
    }

    void Awake()
    {
        SpawnCount = _spawnCount;
    }

    // Update is called once per frame
    void Update()
    {
        //�ő�10�̂܂�
        if(SpawnCount >= 10)
        {
            return;
        }


        if (SceneMoveManager._gameStart == true)
        {
            time += Time.deltaTime;
        }

        //��莞�ԓ��B
        if (time > _interval)
        {
            //�X�|�[��
            GameObject enemy = Instantiate(_enemy);
            enemy.transform.position = new Vector3(_spawnPos.x ,_spawnPos.y ,_spawnPos.z);

            SpawnCount++;
            time = 0f;

            //���̃X�|�[�����W�̌���
            enemy.transform.position = RandomSpawnPosition();

            //���̃X�|�[���Ԋu�̌���
            _interval = RandomSpawnTime();
            Debug.Log(RandomSpawnTime());
        }


    }

    private float RandomSpawnTime()
    {
        return Random.Range(_minTime, _maxTime);
    }

    private Vector3 RandomSpawnPosition()
    {
        float x = _spawnPos.x;
        float y = _spawnPos.y;
        float z = Random.Range(_minPosZ, _maxPosZ);

        return new Vector3(x, y, z);
    }
}
