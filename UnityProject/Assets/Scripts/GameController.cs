using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public static GameController Instance { get; private set; }

    #region SCENE REFERENCES

    public PlayButton PlayButton;
    public FishGenerator FishGenerator;
    public PlayerScript Player;
    public LifeCounter LifeCounter;
    public FoodGenerator FoodGenerator;

    #endregion

    #region PREFAB REFERENCES

    public Blood BloodPrefab;

    #endregion

    private int lives;

    #region MONO BEHAVIOUR

    private void Awake()
    {
        Instance = this;
    }

    private void Start () 
    {

    }
	
    private void Update () 
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Application.LoadLevel("mainscene");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            Application.LoadLevel("second_level");
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            Application.LoadLevel("GamePlay");
        }
    }

    #endregion

    public void StartGame()
    {
        FishGenerator.StartGenerator();
        Player.gameObject.SetActive(true);
        Player.Init();
        LifeCounter.Init();
        FoodGenerator.StartGenerator();
        lives = 5;
    }

    public void GameOver()
    {
        Application.LoadLevel("gameover");
    }

    public void LoseLife()
    {
        lives--;
        LifeCounter.LifeLose(lives);
        if(lives<=0)
        {
            GameOver();
        }
    }

    public void NextRound()
    {
        Application.LoadLevel("second_level");
    }
}
