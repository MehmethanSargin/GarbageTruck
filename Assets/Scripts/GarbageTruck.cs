using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GarbageTruck : MonoBehaviour
{
    public float explosionForce;
    public  float radius;
    public GameManager gameManager;

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.GetComponent<Car>() && !other.gameObject.GetComponent<Car>().crash)
        {
            if (gameManager.Garbages.Count==0)
            {
                gameManager.loseGame = true;
                gameManager.CurrentGameState = GameManager.GameState.FinishGame;
                return;
            }
            other.gameObject.GetComponent<Car>().crash = true;
            other.gameObject.GetComponent<Rigidbody>().AddForce(300,300,300);
            CrashGarbageDrop();
        }
    }

    private void CrashGarbageDrop()
    {
        if (gameManager.Garbages.Count == 1)
        {
            gameManager.Garbages[gameManager.Garbages.Count - 1].GetComponent<Garbage>().drop = true;
            gameManager.Garbages[gameManager.Garbages.Count - 1].GetComponent<Collider>().enabled = true;
            gameManager.Garbages[gameManager.Garbages.Count - 1].GetComponent<Rigidbody>().isKinematic = false;
            gameManager.Garbages[gameManager.Garbages.Count - 1].GetComponent<Rigidbody>().useGravity = true;
            gameManager.Garbages[gameManager.Garbages.Count - 1].GetComponent<Rigidbody>().AddExplosionForce(explosionForce,transform.position,radius);
            gameManager.Garbages.RemoveAt(gameManager.Garbages.Count - 1);
            if (gameManager.offset !=0)
            {
                gameManager.offset-=0.5f;
            }
        }
        if (gameManager.Garbages.Count>=2)
        {
            
            gameManager.Garbages[gameManager.Garbages.Count - 1].GetComponent<Garbage>().drop = true;
            gameManager.Garbages[gameManager.Garbages.Count - 1].GetComponent<Collider>().enabled = true;
            gameManager.Garbages[gameManager.Garbages.Count - 1].GetComponent<Rigidbody>().isKinematic = false;
            gameManager.Garbages[gameManager.Garbages.Count - 1].GetComponent<Rigidbody>().useGravity = true;
            gameManager.Garbages[gameManager.Garbages.Count - 1].GetComponent<Rigidbody>().AddExplosionForce(explosionForce,transform.position,radius);
            gameManager.Garbages.RemoveAt(gameManager.Garbages.Count - 1);
            gameManager.Garbages[gameManager.Garbages.Count - 2].GetComponent<Garbage>().drop = true;
            gameManager.Garbages[gameManager.Garbages.Count - 2].GetComponent<Collider>().enabled = true;
            gameManager.Garbages[gameManager.Garbages.Count - 2].GetComponent<Rigidbody>().isKinematic = false;
            gameManager.Garbages[gameManager.Garbages.Count - 2].GetComponent<Rigidbody>().useGravity = true;
            gameManager.Garbages[gameManager.Garbages.Count - 2].GetComponent<Rigidbody>().AddExplosionForce(explosionForce,transform.position,radius);
            gameManager.Garbages.RemoveAt(gameManager.Garbages.Count - 2);
            if (gameManager.offset !=0)
            {
                gameManager.offset-=1f;
            }
        }
    }


}
