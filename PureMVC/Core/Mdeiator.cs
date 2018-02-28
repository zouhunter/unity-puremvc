using System.Collections.Generic;
using UnityEngine;
namespace PureMVC
{
    public abstract class Mediator : MonoBehaviour, IMediator
    {
        private Mediator m_meditor;

        public virtual void Awake()
        {
            m_meditor = this;
            if (Acceptor != null || Acceptors != null)
            {
                Facade.RegisterMediator(this);
            }
        }

        public virtual void OnDestroy()
        {
            if (Acceptor != null || Acceptors != null)
            {
                if (m_meditor) Facade.RemoveMediator(this);
            }
        }

        public virtual string Acceptor { get { return null; } }

        public virtual IList<string> Acceptors { get { return null; } }

        public abstract void HandleNotification(string observerName);
    }

    public abstract class Mediator<T> : MonoBehaviour, IMediator<T>
    {
        private Mediator<T> m_meditor;
        public virtual void Awake()
        {
            m_meditor = this;
            if (Acceptor != null || Acceptors != null)
            {
                Facade.RegisterMediator(this);
            }
        }
        public virtual void OnDestroy()
        {
            if (Acceptor != null || Acceptors != null)
            {
                if (m_meditor) Facade.RemoveMediator(this);
            }
        }
        public virtual string Acceptor { get { return null; } }

        public virtual IList<string> Acceptors { get { return null; } }

        public abstract void HandleNotification(string observerName, T notification);

    }
}