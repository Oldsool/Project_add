using UnityEngine;
using System.Collections;


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

        public bool PressMouseButton1 = false;
        bool isAttacking = false;


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

            if (Input.GetMouseButtonDown(1))
            {
                PressMouseButton1 = true;
                andlingOfInputOfBlow.enabled = true;
                animator.SetBool("isDefence", true);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                PressMouseButton1 = false;
                andlingOfInputOfBlow.enabled = false;
                animator.SetBool("isDefence", false);
            }

            Debug.Log(isAttacking);

            if (Input.GetMouseButtonDown(0) && !isAttacking)
            {
                isAttacking = true;
                
                StartCoroutine(WaitForAttackEnd());
            }

        }


        IEnumerator WaitForAttackEnd()
        {
            animator.SetTrigger("isAttack");
            yield return new WaitForSeconds(1.5f); ;

            isAttacking = false;
        }
    }
}