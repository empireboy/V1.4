using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouching : MonoBehaviour {

    private InputManager inputManager;
    private PlayerMovement _pm;
    private CapsuleCollider _cc;
    private Weapon _wp;

    private float _ccHeight;

    [SerializeField]
    private float _minimum;
    [SerializeField]
    private float _add;
    [SerializeField]
    private float _deduct;
    [SerializeField]
    public float slowMovementSpeed;

    void Start () {
        if (!(inputManager = this.GetComponent<InputManager>()))
        {
            inputManager = this.gameObject.AddComponent<InputManager>();
        }

        _pm = GetComponent<PlayerMovement>();
        _cc = GetComponent<CapsuleCollider>();
        _wp = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
        _ccHeight = _cc.height;
    }
	
	void Update () {
        if (inputManager.Crouch())
        {
            if (_cc.height > _minimum)
            {
                _cc.height -= _deduct;
                if (_pm.movementSpeed >= _pm.defaultMovementSpeed) _pm.movementSpeed = slowMovementSpeed;
            }
        }
        else
        {
            if (_cc.height < _ccHeight)
            {
                _cc.height += _add;
            }
            if (_pm.movementSpeed <= _pm.defaultMovementSpeed && !_wp.aiming) _pm.movementSpeed = _pm.defaultMovementSpeed;
        }
    }
}