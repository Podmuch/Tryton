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
        Player.Init();
        LifeCounter.Init();
        lives = 5;
    }

    public void GameOver()
    {
        FishGenerator.StopGenerator();
    }

    public void LoseLife()
    {
        lives--;
        LifeCounter.LifeLose(lives);
        if(lives<=0)
        {
            Destroy(Player.gameObject);
            GameOver();
        }
    }
}
