using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : MonoBehaviour {
    protected Animator myAnimation;
    UnityEngine.AI.NavMeshAgent nav;

    private GameObject _cameraGameOver;
    private Camera _cameraGameOverCamera;

    void Start () {
        myAnimation = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _cameraGameOver = GameObject.FindGameObjectWithTag("GameOverCamera");
        _cameraGameOverCamera = _cameraGameOver.GetComponent<Camera>();
    }
	
	void Update () {
        if (_cameraGameOverCamera.enabled)
        {
            myAnimation.SetBool("idle", true);
            nav.speed = 0;
        }
	}
}
