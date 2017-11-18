using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    protected Animator myAnimation;
    private Animation _runAnimation;
    UnityEngine.AI.NavMeshAgent nav;
    Transform player;

    [SerializeField]
    private float speedMin;
    [SerializeField]
    private float speedMax;

    public float speed;

    private void Awake()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start () {
        myAnimation = GetComponent<Animator>();
        myAnimation.SetFloat("speed", 1);
        nav.speed = Random.Range(speedMin, speedMax);
        speed = nav.speed;
        myAnimation.speed = speed*0.2f;
    }

    private void Update()
    {
        nav.SetDestination(player.position);
    }
}
