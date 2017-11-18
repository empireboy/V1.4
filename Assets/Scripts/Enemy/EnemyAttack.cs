using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    private Transform _player;
    private GameObject _playerObject;
    protected Animator myAnimation;
    UnityEngine.AI.NavMeshAgent nav;
    private EnemyMovement _em;

    [SerializeField]
    private GameObject _canvasTakingDamage;

    private bool _hit = false;

    void Start () {
        _player = GameObject.FindWithTag("Player").transform;
        _playerObject = GameObject.FindWithTag("Player");
        myAnimation = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _em = GetComponent<EnemyMovement>();
        _canvasTakingDamage = GameObject.Find("CanvasTakingDamage");
    }
	
	void Update () {
		if (Vector3.Distance(transform.position, _player.position) < 3)
        {
            myAnimation.SetFloat("attack", 1);
            nav.speed = 0F;
            if (!_hit)
            {
                if (myAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5 && myAnimation.GetCurrentAnimatorStateInfo(0).IsName("New State 2"))
                {
                    if (Vector3.Distance(transform.position, _player.position) < 3)
                    {
                        _canvasTakingDamage.GetComponent<FadeTakingDamage>().checkFade = true;
                        _playerObject.GetComponent<PlayerHealth>().hp--;
                    }
                    _hit = true;
                }
                else
                {
                    _hit = false;
                }
            }
        }
        else
        {
            myAnimation.SetFloat("attack", 0);
            if (myAnimation.GetCurrentAnimatorStateInfo(0).IsName("New State 0"))
            {
                nav.speed = _em.speed;
            }
        }
	}
}
