using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class norakattackPlayer : MonoBehaviour
{
    bool atac = false;
    Animator _animator;
    GameObject _norak;
    // Start is called before the first frame update
    void Start()
    {
        _norak = GameObject.Find("Norak");

        _animator = _norak.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.TryGetComponent<Paladin>(out Paladin paladin))   //условие атаки плейера
        {
            //_animator.SetTrigger("isShieldImpact");
            Debug.Log("Норак атакует");
        }
        else if (other.TryGetComponent<handlingOfInputOfBlow>(out handlingOfInputOfBlow handlingOfInputOfBlow))  //если удар меч о меч
        {
            _animator.SetTrigger("isShieldImpact");
            Debug.Log("Скрестили мечи");
        }
    }
}
