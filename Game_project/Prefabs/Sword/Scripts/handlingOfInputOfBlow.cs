using Assets.Game_project.Prefabs.Characters.MainCharacter.Scripts.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handlingOfInputOfBlow : MonoBehaviour
{
    GameObject _player;       //� �� ���� ��� � ����� ������� ������� ������ ��� ���� ����� � ����� � ������
    GameObject _norak;

    private Collider _collider;

    Animator _animator;
    FsmExample playerScript;

    void Start()
    {
        _player = GameObject.Find("Paladin 1");
        _norak = GameObject.Find("Norak");
        
        _animator = _norak.GetComponent<Animator>();


        _collider = GetComponent<Collider>();
        _collider.enabled = false;

        playerScript = _player.GetComponent<FsmExample>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Norak>(out Norak norak))   //������� ����� �����
        {
            if (playerScript.isAttacking)
            {
                Debug.Log("ya atakuyu Noraka");
            }
            
        }
    }

}
