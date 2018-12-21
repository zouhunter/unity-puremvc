using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC;

public class RandomScaleMediator : Mediator
{
    private Transform target;
    public RandomScaleMediator(int observerName, Transform target) : base(observerName)
    {
        this.target = target;
    }
    public override void HandleNotification(int observerName)
    {
        target.transform.localScale =Vector3.one * Random.Range(0.8f, 1.5f);
    }
}
