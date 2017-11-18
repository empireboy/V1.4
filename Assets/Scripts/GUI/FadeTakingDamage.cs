using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTakingDamage : MonoBehaviour {
    private CanvasGroup canvasGroup;
    private bool _fadeIn = true;

    public bool checkFade = false;

    [SerializeField]
    private float fadeInSpeed;
    [SerializeField]
    private float fadeOutSpeed;

    void Update () {
        canvasGroup = GetComponent<CanvasGroup>();
        if (checkFade)
        {
            if (_fadeIn)
            {
                if (canvasGroup.alpha < 1)
                {
                    canvasGroup.alpha += fadeInSpeed;
                }
                else
                {
                    _fadeIn = false;
                }
            }
            else if (!_fadeIn)
            {
                if (canvasGroup.alpha > 0)
                {
                    canvasGroup.alpha -= fadeOutSpeed;
                }
                else
                {
                    _fadeIn = true;
                    checkFade = false;
                }
            }
        }
    }
}
