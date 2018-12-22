using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Player : MonoBehaviour {
    public int key;
    public bool isMe;
    private NavMeshAgent agent;

    private void Start()
    {
        var render = GetComponent<Renderer>();
        GameManager.Regist(new ColorMaterialMediator(key,render));
        GameManager.Regist(new RandomScaleMediator(ObserverName.RANDOM_SCALE,transform));
  
        if (isMe)
        {
            agent = GetComponent<NavMeshAgent>();
            GameManager.Regist<Vector3>(EventName.MOVE_PLAYER, MovePlayer);
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
