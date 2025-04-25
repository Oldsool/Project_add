
using UnityEngine;


namespace Assets.Game_project.Prefabs.Characters.MainCharacter.Scripts.FSM
{
    public class FsmStateWalk : FsmStateMovement
    {
        Animator animator;

        public FsmStateWalk(Fsm fsm, Transform transform, float speed, Animator animator) : base(fsm, transform, speed) 
        {
            this.animator = animator;
        } 
        
        public override void Update()
        {
            Debug.Log("Walk state [UPDATE]");

            animator.SetBool("isWalking", true); //animation

            var inputDirection = ReadInput();

            if (inputDirection.sqrMagnitude == 0f)
            {
                animator.SetBool("isWalking", false);
                Fsm.SetState<FsmStateIdle>();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                animator.SetBool("isRunning", true);
                Fsm.SetState<FsmStateRun>();
            }

            Move(inputDirection);
        }
    }
}