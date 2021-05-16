using System;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private float _dirX;
    private float _mousePosX;
    public float swipeValue;
    
    void Update()
    {
        switch (GameManager.manager.CurrentGameState)
        {
            case GameManager.GameState.Prepare:
                break;
            case GameManager.GameState.MainGame:
                PlayerMovement();
                break;
            case GameManager.GameState.FinishGame:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }

    private void PlayerMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 playerPos = transform.position;
            _dirX = playerPos.x;
            _mousePosX = GameManager._cam.ScreenToViewportPoint(Input.mousePosition).x;
        }
        if (Input.GetMouseButton(0))
        {
            float newMousePosX = GameManager._cam.ScreenToViewportPoint(Input.mousePosition).x;
            float distanceX = newMousePosX - _mousePosX;
            float posX = _dirX + (distanceX * swipeValue);
            posX = Mathf.Clamp(posX, -0.7f, 0.7f);
            Vector3 pos =transform.localPosition;
            pos.x = posX;
            transform.localPosition = pos;
        }
    }
}

