using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game_project.Prefabs.Characters.EnemyNPC.ML_agents.Scripts
{
    public class AttackPlayer
    {
        public bool swordUp = false;

        public IEnumerator atack(Animator _animator, Transform transform,float dist)  // сначала атакуем
        {
            _animator.SetTrigger("isAttack");
            swordUp = true;
            // Теперь отходим назад
            if (dist < 1.63f)
            {
                yield return RetreatSimple(_animator, transform);
            }

        }

        IEnumerator RetreatSimple(Animator _animator,Transform transform)   // потом чуть отходим назад
        {
            float retreatTime = 0.8f;
            float retreatSpeed = 3f;
            float timer = 0f;

            swordUp = false;


            _animator.SetTrigger("isRunBack");
            

            while (timer < retreatTime)
            {
                transform.position -= transform.forward * retreatSpeed * Time.deltaTime;
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}