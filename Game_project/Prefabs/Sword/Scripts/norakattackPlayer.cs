using Assets.Game_project.Prefabs.Characters.MainCharacter.Scripts.FSM;

using UnityEngine;

public class norakattackPlayer : MonoBehaviour
{
    Animator _animator;
    GameObject _norak;
    GameObject _player;
    bool _mouseButton;
    FsmExample playerScript;

    // Start is called before the first frame update
    void Start()
    {
        _norak = GameObject.Find("Norak");
        _player = GameObject.Find("Paladin 1");
        

        _animator = _norak.GetComponent<Animator>();

        playerScript = _player.GetComponent<FsmExample>();
        
    }

    private void Update()
    {
        _mouseButton = playerScript.PressMouseButton1;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<Paladin>(out Paladin paladin))   //������� ����� �������
        {
            if (!_mouseButton)
            {
                Debug.Log("���� �� ���������� ��������");
                playerScript.player.health--;
            }
            else
            {
                Debug.Log("�������� �����, ��� ���� ������ ");
            }
        }
        if (other.TryGetComponent<handlingOfInputOfBlow>(out handlingOfInputOfBlow handlingOfInputOfBlow))  //���� ���� ��� � ���
        {
            
                _animator.SetTrigger("isShieldImpact");
                Debug.Log("���� ��� �������������� ����");
                
            
            
            
        }
    }
}
