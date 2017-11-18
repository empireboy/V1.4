using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunning : MonoBehaviour {

    private InputManager inputManager;
    private PlayerMovement _pm;
    private PlayerCrouching _pc;
    private Weapon _wp;

    [SerializeField]
    private GameObject _weaponObject;

    public float fastMovementSpeed;

    void Start()
    {
        if (!(inputManager = this.GetComponent<InputManager>()))
        {
            inputManager = this.gameObject.AddComponent<InputManager>();
        }

        _pm = GetComponent<PlayerMovement>();
        _pc = GetComponent<PlayerCrouching>();
        _wp = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
    }

    void Update()
    {
        if (inputManager.Run() && inputManager.Up())
        {
            if (!_wp.aiming)
            {
                _pm.movementSpeed = fastMovementSpeed;
                _weaponObject.transform.position = new Vector3(_weaponObject.transform.position.x, _weaponObject.transform.position.y - 0.01f, _weaponObject.transform.position.z);
            }
            else
            {
                if (_pm.movementSpeed >= 5) _pm.movementSpeed = _pc.slowMovementSpeed;
            }
        }
        else
        {
            if (_pm.movementSpeed >= 5) _pm.movementSpeed = _pm.defaultMovementSpeed;
        }
    }
}
