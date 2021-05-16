using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
   public bool finishGame = false;
   private void OnTriggerEnter(Collider other)
   {
      if (other.GetComponent<GarbageTruck>())
      {

         GameManager.manager.CurrentGameState = GameManager.GameState.FinishGame;
         //other.GetComponent<PlayerMovementController>().enabled = false;
         //other.GetComponent<PlayerInputController>().enabled = false;
         //finishGame = true;
      }
   }
   
}
