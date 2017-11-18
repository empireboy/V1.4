using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    private GameObject cameraObject;
    private InputManager inputManager;

    [SerializeField]
    private float _cameraSpeed = 5;
    [SerializeField]
    private float _maxY;

    /*
     * Initialize the required components components
     */
    void Start() {
        cameraObject = Camera.main.gameObject;
        //check if the inputmanager is present. If it's not, add it.
        if(!(inputManager = this.GetComponent<InputManager>())){
            inputManager = this.gameObject.AddComponent<InputManager>();
        }
    }

	void Update() {
        //rotate the entire gameobject
        this.transform.Rotate(0, inputManager.GetXRot() * _cameraSpeed, 0);
        //rotate the camera only.
        float rotationY = inputManager.GetYRot() * _cameraSpeed;
        cameraObject.transform.Rotate(rotationY, 0, 0);
    }
}
