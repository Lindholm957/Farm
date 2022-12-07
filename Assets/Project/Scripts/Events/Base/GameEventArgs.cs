using UnityEngine;

namespace Project.Scripts.Events.Base
{
    public class GameEventArgs
    {
        public GameObject Sender { get; }
        
        public GameEventArgs(GameObject sender)
        {
            Sender = sender;
        }
    }
}