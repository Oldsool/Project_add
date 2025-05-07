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

        if (other.TryGetComponent<Paladin>(out Paladin paladin))   //������� ����� �������
        {
            if (!_mouseButton && !attackPlayer.swordUp)
            {
                Debug.Log("���� �� ���������� ��������");
                playerScript.player.health--;
            }
            else
            {
                Debug.Log("1" + _mouseButton);
                Debug.Log("2" + attackPlayer.swordUp);
                Debug.Log("�������� �����, ��� ���� ������ ");
            }
        }
        if (other.TryGetComponent<handlingOfInputOfBlow>(out handlingOfInputOfBlow handlingOfInputOfBlow))  //���� ���� ��� � ���
        {
            
                _animator.SetTrigger("isShieldImpact");
                Debug.Log("���� ��� � ���");

            if(norak.norak.health == 0)
            {
                Debug.Log("�� ��� �� ������ �����������");
                _animator.SetTrigger("isNorakDeath");
            }

        }
    }
}
