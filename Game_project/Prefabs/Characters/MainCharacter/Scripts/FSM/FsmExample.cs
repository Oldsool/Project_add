using UnityEngine;
using UnityEngine.Windows;

namespace Assets.Game_project.Prefabs.Characters.MainCharacter.Scripts.FSM
{
    public class FsmExample : MonoBehaviour
    {
        private Fsm _fsm;
        private float _walkSpeed = 10f;
        private float _runSpeed = 20f;
        private Animator animator;
        handlingOfInputOfBlow _handlingOfInputOfBlow;
        Collider andlingOfInputOfBlow;
 

        void Start()
        {
            _handlingOfInputOfBlow = FindObjectOfType<handlingOfInputOfBlow>();
            andlingOfInputOfBlow = _handlingOfInputOfBlow.GetComponent<Collider>();
          

            animator = GetComponent<Animator>();

            _fsm = new Fsm();

            _fsm.AddState(new FsmStateIdle(_fsm));
            _fsm.AddState(new FsmStateWalk(_fsm, transform, _walkSpeed, animator));
            _fsm.AddState(new FsmStateRun(_fsm, transform, _runSpeed, animator));

            _fsm.SetState<FsmStateIdle>();
        }


        void Update()
        {
            _fsm.Update();

            if (UnityEngine.Input.GetMouseButtonDown(1))
            {
                
                andlingOfInputOfBlow.enabled = true;
                animator.SetBool("isDefence", true);
            }
            else if (UnityEngine.Input.GetMouseButtonUp(1))
            {
                
                andlingOfInputOfBlow.enabled = false;
                animator.SetBool("isDefence", false);
            }
        }
    }
}