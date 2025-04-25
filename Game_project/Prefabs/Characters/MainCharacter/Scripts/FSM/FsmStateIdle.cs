using UnityEngine;

namespace Assets.Game_project.Prefabs.Characters.MainCharacter.Scripts.FSM
{
    public class FsmStateIdle : FsmState
    {
        public FsmStateIdle(Fsm fsm) : base(fsm) { }

        public override void Enter()
        {
            Debug.Log("idle state [ENTER]");
        }

        public override void Exit()
        {
            Debug.Log("idle state [EXIT]");
        }

        public override void Update()
        {
            Debug.Log("idle state [UPDATE]");

            if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
            {
                Fsm.SetState<FsmStateWalk>();
            }
        }
    }
}