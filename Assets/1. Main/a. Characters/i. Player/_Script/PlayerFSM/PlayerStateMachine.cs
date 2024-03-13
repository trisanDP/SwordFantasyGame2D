using OriginL.EnemySpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OriginL
{
    public class PlayerStateMachine
    {
        public PlayerState CurrentState;
        public void Initialize(PlayerState startingState) {
            CurrentState = startingState;
            CurrentState.EnterState();
        }

        public void ChangeState(PlayerState newState) {
            CurrentState.ExitState();
            CurrentState = newState;
            CurrentState.EnterState();
        }
    }
}
