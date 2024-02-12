using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject _enemy;

    //敵生成時間間隔
    private float _interval;

    private float time = 0f;

    //スポーン時間の範囲
    [SerializeField]
    private float _maxTime, _minTime;

    //スポーン地点
    [SerializeField]
    private Vector3 _spawnPos;

    public float _minPosZ = 4f;
    public float _maxPosZ = -4f;

    //敵の上限数
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
        //最大10体まで
        if(SpawnCount >= 10)
        {
            return;
        }


        if (SceneMoveManager._gameStart == true)
        {
            time += Time.deltaTime;
        }

        //一定時間到達
        if (time > _interval)
        {
            //スポーン
            GameObject enemy = Instantiate(_enemy);
            enemy.transform.position = new Vector3(_spawnPos.x ,_spawnPos.y ,_spawnPos.z);

            SpawnCount++;
            time = 0f;

            //次のスポーン座標の決定
            enemy.transform.position = RandomSpawnPosition();

            //次のスポーン間隔の決定
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
