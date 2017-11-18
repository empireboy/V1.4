using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCameraMovement : MonoBehaviour {

    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;

    private Camera _camera;

    private void Start()
    {
        _camera = this.GetComponent<Camera>();
    }

    void Update () {
		if (_camera.enabled)
        {
            this.transform.position += (this.transform.right * Time.deltaTime * speed);
            this.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        }
	}
}
