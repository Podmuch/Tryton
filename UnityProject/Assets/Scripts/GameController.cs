using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public static GameController Instance { get; private set; }

    #region SCENE REFERENCES

    public PlayButton PlayButton;
    public FishGenerator FishGenerator;

    #endregion

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
    }
}
