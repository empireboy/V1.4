using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOutOfBounds : MonoBehaviour {
    [SerializeField]
    private GameObject _outOfBounds;
    [SerializeField]
    private Text _outOfBoundsText;
    [SerializeField]
    private Text _returnText;
    [SerializeField]
    private Text _counterText;
    [SerializeField]
    private float _outOfBoundsRange;
    [SerializeField]
    private float _counter;
    [SerializeField]
    private Camera _cameraMain;
    [SerializeField]
    private Camera _cameraGameOver;

    private Vector3 _playerPos;
    private bool _isCounting = false;
    private float _counterCurrent;
    private bool _gameOver = false;

    void Start () {
        _playerPos = _outOfBounds.transform.position;
        _outOfBoundsText.text = "";
        _returnText.text = "";
        _counterText.text = "";
        _counterCurrent = _counter*60;
    }

    void Update()
    {
        if (!_gameOver)
        {
            if (Vector3.Distance(transform.position, _playerPos) > _outOfBoundsRange)
            {
                if (!_isCounting)
                {
                    _outOfBoundsText.text = "You are out of bounds!";
                    _returnText.text = "Return to the village";
                    _counterCurrent = _counter * 60;
                    _isCounting = true;
                }
            }
            else
            {
                _outOfBoundsText.text = "";
                _returnText.text = "";
                _counterText.text = "";
                _isCounting = false;
            }

            // Counter
            if (_isCounting)
            {
                if (_counterCurrent <= 0)
                {
                    _cameraMain.enabled = false;
                    _cameraGameOver.enabled = true;
                    _outOfBoundsText.text = "";
                    _returnText.text = "";
                    _counterText.text = "";
                    _isCounting = false;
                    _gameOver = true;
                }
                if (!_gameOver)
                {
                    if (_counterCurrent > 0) _counterCurrent -= 1;
                    _counterText.text = _counterCurrent + "";
                }
            }
        }
    }
}
