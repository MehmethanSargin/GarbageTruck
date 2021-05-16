using System;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region Variables

    public static PlayerMovementController Instance;

    [SerializeField] float forwardSpeed;
    [SerializeField] float maxLimit;
    [SerializeField] float minLimit;
    public Animator playerAnim;
  

    #endregion

    #region Monobehaviour Callbacks

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        switch (GameManager.manager.CurrentGameState)
        {
            case GameManager.GameState.Prepare:
                break;
            case GameManager.GameState.MainGame:
                Running();
                break;
            case GameManager.GameState.FinishGame:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
       
    }
    #endregion

    #region Other Methods
    void Running()
    {
        transform.Translate(transform.forward * forwardSpeed * Time.deltaTime, Space.World);
    }

    public void Turn(float movementValue)
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x + movementValue, minLimit, maxLimit), transform.position.y, transform.position.z);
    }
    #endregion
}