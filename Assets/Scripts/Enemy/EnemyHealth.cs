using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    protected Animator myAnimation;
    UnityEngine.AI.NavMeshAgent nav;

    public float hp = 5;
    public bool hit = false;

    [SerializeField]
    private float _deathTime;

    private EnemyMovement _em;

    void Start () {
        myAnimation = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _em = gameObject.GetComponent<EnemyMovement>();
    }
	
	void Update () {
		if (hp <= 0)
        {
            myAnimation.SetInteger("hp", 0);
            nav.speed = 0F;
            Destroy(GetComponent<MeshCollider>());
            Destroy(gameObject, _deathTime);
        }
        if (hit)
        {
            myAnimation.SetInteger("hit", 1);
            nav.speed = 0F;
            if (myAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5 && /*myAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.5 &&*/ /*!myAnimation.IsInTransition(0) &&*/ myAnimation.GetCurrentAnimatorStateInfo(0).IsName("New State 1"))
            {
                myAnimation.SetInteger("hit", 0);
                hit = false;
                nav.speed = _em.speed;
            }
        }
    }
}
