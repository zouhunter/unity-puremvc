using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour {
    public int key;
    public bool isMe;

    private NavMeshAgent agent;

    private void Awake()
    {
        var render = GetComponent<Renderer>();
        GameManager.Instence.RegisterMediator(new ColorMaterialMediator(key,render));
        GameManager.Instence.RegisterMediator(new RandomScaleMediator(ObserverName.RANDOM_SCALE,transform));
    }

    private void Start()
    {
        if (isMe)
        {
            agent = GetComponent<NavMeshAgent>();
            GameManager.Instence.eventDispatch.RegistEvent<Vector3>(EventName.MOVE_PLAYER, MovePlayer);
        }
    }

    private void MovePlayer(Vector3 target)
    {
        if(isActiveAndEnabled && agent)
        {
            agent.SetDestination(target);
        }
    }
}
