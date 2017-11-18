using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public float hp;

    [SerializeField]
    private Camera _cameraMain;
    [SerializeField]
    private Camera _cameraGameOver;

    void Start () {

    }
	
	void Update () {
		if (hp <= 0)
        {
            _cameraMain.enabled = false;
            _cameraGameOver.enabled = true;
        }
	}
}
