using Assets.Game_project.Prefabs.Characters.MainCharacter.Scripts.FSM;
using Assets.Game_project.Prefabs.Characters.EnemyNPC.ML_agents.Scripts;
using UnityEngine;

public class norakattackPlayer : MonoBehaviour
{
    Animator _animator;
    GameObject _norak;
    GameObject _player;
    bool _mouseButton;
    FsmExample playerScript;
    Norak norak;
    AttackPlayer attackPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _norak = GameObject.Find("Norak");
        _player = GameObject.Find("Paladin 1");
        

        _animator = _norak.GetComponent<Animator>();

        playerScript = _player.GetComponent<FsmExample>();

        norak = _norak.GetComponent<Norak>();

        attackPlayer = new AttackPlayer();

    }

    private void Update()
    {
        _mouseButton = playerScript.PressMouseButton1;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<Paladin>(out Paladin paladin))   //условие атаки плейера
        {
            if (!_mouseButton && !attackPlayer.swordUp)
            {
                Debug.Log("удар по коллайдеру паладина");
                playerScript.player.health--;
            }
            else
            {
                Debug.Log("1" + _mouseButton);
                Debug.Log("2" + attackPlayer.swordUp);
                Debug.Log("Ќеверна€ атака, меч типа подн€т ");
            }
        }
        if (other.TryGetComponent<handlingOfInputOfBlow>(out handlingOfInputOfBlow handlingOfInputOfBlow))  //если удар меч о меч
        {
            
                _animator.SetTrigger("isShieldImpact");
                Debug.Log("”дар меч о меч");

            if(norak.norak.health == 0)
            {
                Debug.Log("то что не должно происходить");
                _animator.SetTrigger("isNorakDeath");
            }

        }
    }
}
