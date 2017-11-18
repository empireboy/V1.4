using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public ParticleSystem smoke;

    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private float _spawnTimeMin;
    [SerializeField]
    private float _spawnTimeMax;
    [SerializeField]
    private float _spawnDelay;
    [SerializeField]
    private int _maxEnemies;

    private float _spawnTime;
    private float _spawnTimer;
    private bool _delayActive = false;
    private float _timer;

    void Start () {
        _spawnTime = randomTime();
        _spawnDelay = _spawnDelay * 60;
        _timer = _spawnTime;
        _spawnTimer = _spawnDelay;
	}

	void Update () {
        if (!_delayActive)
        {
            if (_timer <= 0)
            {
                if (GameObject.FindGameObjectsWithTag("Enemy").Length < _maxEnemies)
                {
                    smoke.Play();
                    _delayActive = true;
                    _spawnTime = randomTime();
                    _timer = _spawnTime;
                }
                else
                {
                    _spawnTime = randomTime();
                    _timer = _spawnTime;
                }
            }
            else
            {
                _timer--;
            }
        }
        else
        {
            if (_spawnTimer <= 0)
            {
                Instantiate(_enemy, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                //Debug.Log(GameObject.FindGameObjectsWithTag("Enemy").Length);
                _delayActive = false;

                _spawnTimer = _spawnDelay;
            }
            else
            {
                _spawnTimer--;
            }
        }
    }

    private float randomTime()
    {
        return Random.Range(_spawnTimeMin * 60, _spawnTimeMax * 60);
    }
}
