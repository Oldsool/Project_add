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
        public bool isAttacking = false;
        public Character player;
        bool erg = true;
        void Start()
        {
            player = new Character();
            player.health = 4f;


            _handlingOfInputOfBlow = FindObjectOfType<handlingOfInputOfBlow>();
            andlingOfInputOfBlow = _handlingOfInputOfBlow.GetComponent<Collider>();  // скрываем и показываем колладйдер меча для лучшей обработки удара
          

            animator = GetComponent<Animator>();

            _fsm = new Fsm();

            _fsm.AddState(new FsmStateIdle(_fsm));
            _fsm.AddState(new FsmStateWalk(_fsm, transform, _walkSpeed, animator));
            _fsm.AddState(new FsmStateRun(_fsm, transform, _runSpeed, animator));

            _fsm.SetState<FsmStateIdle>();
        }


        void Update()
        {
            if (player.health == 0)
            {
                
                if (erg)
                {
                    //if (!animator.GetBool("isDeath"))
                   // {
                        animator.SetTrigger("isDeath");
                    //}
                    Debug.Log("зашли один раз");
                    erg = false;
                }
            }

            _fsm.Update();

            if (Input.GetMouseButtonDown(1))  // ПКМ
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

            if (Input.GetMouseButtonDown(0) && !isAttacking)  //ЛКМ
            {
                isAttacking = true;
                andlingOfInputOfBlow.enabled = true;
                StartCoroutine(WaitForAttackEnd());
            }
   
        }



        IEnumerator WaitForAttackEnd()
        {
            animator.SetTrigger("isAttack");
            yield return new WaitForSeconds(1.5f); ;
            andlingOfInputOfBlow.enabled = false;
            isAttacking = false;
        }
    }
}