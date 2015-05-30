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
        FishGenerator.StopGenerator();
        PlayButton.Init();
        Player.gameObject.SetActive(false);
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
