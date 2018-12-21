﻿using System.Collections.Generic;
using UnityEngine;
namespace PureMVC
{
    public abstract class Mediator : IMediator
    {
        protected int[] acceptors;
        public Mediator(params int[] acceptors)
        {
            this.acceptors = acceptors;
        }
        public virtual IList<int> Acceptors { get { return acceptors; } }
        public abstract void HandleNotification(int observerName);
    }

    public abstract class Mediator<T>: IMediator<T>
    {
        protected int[] acceptors;
        public Mediator(params int[] acceptors)
        {
            this.acceptors = acceptors;
        }
        public virtual IList<int> Acceptors { get { return acceptors; } }
        public abstract void HandleNotification(int observerName, T notification);

    }
}