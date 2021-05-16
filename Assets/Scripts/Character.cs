using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Animator _animator;
    public GameManager gameManager;
    public GameObject handPos;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
   {
       if (other.GetComponent<Garbage>() && !other.GetComponent<Garbage>().collected)
       {
           other.GetComponent<Garbage>().collected = true;
           _animator.SetTrigger("Topla");
            TakeHandGarbage(other.gameObject);
           StartCoroutine(gameManager.StackGarbage(other.gameObject));  
          
       }
   }

    private void TakeHandGarbage(GameObject other)
    {
        other.transform.SetParent(handPos.transform);
        other.GetComponent<Rigidbody>().isKinematic = true;
        other.GetComponent<Collider>().enabled = false;
        other.transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 4f);
        other.transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, 4f);
    }
}
