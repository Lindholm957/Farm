using Project.Scripts.Garden;
using UnityEngine;

namespace Project.Scripts.Events.Base
{
    public class GameEventArgs
    {
        public BedController Sender { get; }
        
        public GameEventArgs(BedController sender)
        {
            Sender = sender;
        }
    }
}