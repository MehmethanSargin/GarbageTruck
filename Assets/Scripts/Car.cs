using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    public float speed = 1f;
    public bool crash = false;
    void Update()
    {
        switch (GameManager.manager.CurrentGameState)
        {
            case GameManager.GameState.Prepare:
                break;
            case GameManager.GameState.MainGame:
                transform.Translate(0,0,1 * speed * Time.deltaTime);
                break;
            case GameManager.GameState.FinishGame:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }
}
