using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiScript : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(Player.position);
    }
}
