using UnityEngine;
using UnityEngine.AI;

    public enum CustomerState
    {
        Idle,
        WalkingToShalf,
        PickingItem,
        WalkingToCounter,
        PlacingItem
    }
 public class Timer
    {
        private float timeRemaining;

        public void Set(float time)
        {
            timeRemaining = time;
        }
        public void Updete(float deltaTime)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= deltaTime;
            }
        }
    public bool IsFinished()
    {
        return timeRemaining <= 0;
    }
    }

