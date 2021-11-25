using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPlatfom : MonoBehaviour
{
    public float AttackCD = 3f;
    float _timer;
    bool _isFireing = false;
    bool _playerIn = false;
    Animator _animator;
    GameObject player;
    private void Awake()
    {
        _animator =GetComponent<Animator>();
        if(_animator == null)
        {
            throw new System.Exception("Cann't find posionPlatform Animator");
        }
    }
    private void Update()
    {
        if(!_isFireing)
        {
            _timer += Time.deltaTime;
            if(_timer >= AttackCD)
            {
                _isFireing = true;
                _animator.SetTrigger("Fire");
                _timer = 0;
            }
        }
        else
        {
            Attack();
        }

    }

    void Attack()
    {

        //FIXME: player dead
        if(_playerIn)
        {
            player.GetComponent<IGetHurt>().GetHurt(this.transform);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _playerIn = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            _playerIn = false;
        }
    }

    //Animation Event
    public void EndAttack()
    {
        _isFireing = false;
        _timer = 0;
    }
}
