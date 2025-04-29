using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game_project.Prefabs.Characters.EnemyNPC.ML_agents.Scripts
{
    public class AttackPlayer
    {
        

        public IEnumerator atack(Animator _animator, Transform transform)  // ������� �������
        {
            _animator.SetTrigger("isAttack");
            
            // ��� ���� ������� ��������
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("attack"));

            // ��� ���� ��� ����������
            yield return new WaitUntil(() =>
                !_animator.GetCurrentAnimatorStateInfo(0).IsName("attack") ||
                _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

            Debug.Log("�������� ����� �����������");

            // ������ ������� �����
            yield return RetreatSimple(_animator, transform);


        }

        IEnumerator RetreatSimple(Animator _animator,Transform transform)   // ����� ���� ������� �����
        {
            float retreatTime = 2.8f;
            float retreatSpeed = 3f;
            float timer = 0f;

            _animator.SetTrigger("isRunBack");

            while (timer < retreatTime)
            {
                //yield return new WaitForSeconds(1f);
                
                
                transform.position -= transform.forward * retreatSpeed * Time.deltaTime;
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}