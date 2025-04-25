
using UnityEngine;

namespace Assets.Game_project.Prefabs.Characters.MainCharacter.Scripts.FSM
{
    public class FsmStateRun : FsmStateMovement
    {
        Animator animator;
        public FsmStateRun(Fsm fsm, Transform transform, float speed, Animator animator) : base(fsm, transform, speed) 
        { 
            this.animator = animator;
        }

        public override void Update()
        {
            Debug.Log("Run state [UPDATE]");

            var inputDirection = ReadInput();

            if (inputDirection.sqrMagnitude == 0f)
            {
                animator.SetBool("isWalking", false);
                Fsm.SetState<FsmStateIdle>();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                animator.SetBool("isRunning", false);
                Fsm.SetState<FsmStateWalk>();
            }

            Move(inputDirection);
        }
    }
}