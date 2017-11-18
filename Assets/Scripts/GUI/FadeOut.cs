using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {
    [SerializeField]
    private float _speed;

    private GameObject _cameraGameOver;
    private Camera _cameraGameOverCamera;
    private AudioSource _audio;
    private bool playSoundOnce = false;

    private void Start()
    {
        _cameraGameOver = GameObject.FindGameObjectWithTag("GameOverCamera");
        _cameraGameOverCamera = _cameraGameOver.GetComponent<Camera>();
        _audio = GetComponent<AudioSource>();
    }

    void Update () {
        if (_cameraGameOverCamera.enabled && !_audio.isPlaying)
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += _speed * Time.deltaTime;
            }
            else
            {
                Application.LoadLevel("Menu");
            }
        }

        if (_cameraGameOverCamera.enabled)
        {
            if (!_audio.isPlaying && !playSoundOnce)
            {
                _audio.Play();
                playSoundOnce = true;
            }
            if (_audio.time >= _audio.clip.length-2) _audio.Stop();
        }
    }
}
