using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public static Camera _cam;
    public enum GameState
    {
        Prepare,
        MainGame,
        FinishGame
    }

    private GameState _currentGameState;
    public List<GameObject> Garbages = new List<GameObject>();
    public Finish finish;
    public GameObject cameraSecondPos;
    public List<Car> Cars = new List<Car>();
    private bool carDestroyed = false;
    public float offset = 0;
    public Transform SpawnPos;
    private bool resetOffset = false;
    public GarbageTruck GarbageTruck;
    private bool rotateTruck = false;
    public GameObject leftCharacter;
    public GameObject rightCharacter;
    public float explosionForce;
    public  float radius;
    public GameObject startGame;
    [HideInInspector] public bool loseGame;
    
    public GameState CurrentGameState
    {
        get
        {
            return _currentGameState;
        }
        set
        {
            switch (value)
            {
                //hepsi 1 kere çalışıcak
                case GameState.Prepare: // bu calısmaz ama atarsam buraya gamestate 1 kere calısır
                    Debug.Log("1");
                    break;
                case GameState.MainGame:
                    Debug.Log("Main Game gectıkten sonra 1 kere");
                    break;
                case GameState.FinishGame:
                    if (loseGame)
                    {
                        Debug.Log("Oyunu kaybettiniz");
                    }
                    else
                    {
                        Debug.Log("Oyunu kazandınız.");
                        StartCoroutine(ThrowTheRubbish());
                        Camera.main.transform.SetParent(cameraSecondPos.transform);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            _currentGameState = value;
        }
    }
    private void Awake()
    {
        manager = this;
        _cam = Camera.main;
    }

    private void Start()
    {
        startGame.SetActive(true);
    }

    void Update()
    {
        switch (CurrentGameState)
        {
            case GameState.Prepare:
                Debug.Log("4");
                Swiped();
                break;
            case GameState.MainGame:
                Debug.Log("5");
                break;
            case GameState.FinishGame:
                if (!loseGame)
                {
                    Camera.main.transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime);
                    Camera.main.transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime); 
                    RotateTruck();
                }
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    

    public IEnumerator StackGarbage(GameObject other)
    {
        if (!other.GetComponent<Garbage>().drop)
        {
            yield return new WaitForSeconds(.7f);
            Garbages.Add(other.gameObject);
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.SetParent(SpawnPos.transform);
            other.GetComponent<Collider>().enabled = false;
            other.transform.position = Vector3.Lerp(other.transform.position, SpawnPos.position - new Vector3(0,0,offset), 1f);
            offset+=0.5f;
            if (Garbages.Count > 3)
            {
                if (!resetOffset)
                {
                    offset = 0f;
                    resetOffset = true;
                }
                other.transform.position = Vector3.Lerp(other.transform.position, new Vector3(SpawnPos.position.x,0.9f,SpawnPos.position.z -offset), 1f);
                Debug.Log(offset);
            }
        }
    }

    private void RotateTruck()
    {
        if (!rotateTruck)
        {
            GarbageTruck.gameObject.transform.position += new Vector3(0, 1f, 0);
            GarbageTruck.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            GarbageTruck.gameObject.transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(-20f,0f,0f), 4f);
            rotateTruck = true;
        }
    }

    private IEnumerator ThrowTheRubbish()
    {
        leftCharacter.GetComponent<Collider>().enabled = false;
        rightCharacter.GetComponent<Collider>().enabled = false;
        for (int i = Garbages.Count-1; i >= 0; i--)
        {
            yield return new WaitForSeconds(1f);
            Garbages[i].GetComponent<Collider>().enabled = true;
            Garbages[i].GetComponent<Rigidbody>().useGravity = true;
            Garbages[i].GetComponent<Rigidbody>().isKinematic = false;
            Garbages[i].GetComponent<Rigidbody>().AddForce(0,1.5f,-1f,ForceMode.Impulse);
        }
    }

    private void Swiped()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startGame.SetActive(false);
            CurrentGameState = GameState.MainGame;
        }
    }
    
}
