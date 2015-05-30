//GamePlay Model
//  provides necessary methods and properties to spawn new monsters
using UnityEngine;
using Asteroids.Interface;

namespace Asteroids.GamePlay
{
    //inherits from base abstract class for all Models (drawing)
    public class GamePlayModel : AbstractModel
    {
        public int numberOfAsteroidsToSpawn;
        public int asteroidsWaveCounter;
        public bool isAsteroidsReadyToSpawn;
        public int numberOfEnemyShipsToSpawn;
        public int enemyShipsWaveCounter;
        public bool isEnemyShipsReadyToSpawn;
        //map corners, they are scalling in gameplay controller
        //  used to randomizing spawn, wrapping for movableObjects and control player
        public static Vector2 leftTopCorner = new Vector2(-8.2f, 5.2f);
        public static Vector2 rightBottomCorner = new Vector2(8.2f, -5.2f);

        //initial spawn parameters
        public GamePlayModel(System.Object player)
        {
            DrawParams=player;
            numberOfAsteroidsToSpawn = 3;
            asteroidsWaveCounter = 0;
            isAsteroidsReadyToSpawn = true;
            numberOfEnemyShipsToSpawn = 1;
            enemyShipsWaveCounter = 0;
            isEnemyShipsReadyToSpawn = false;
        }

        public Vector3 GetRandomPosition()
        {
            return new Vector3(Random.Range(leftTopCorner.x, rightBottomCorner.x), (Random.Range(0, 1) == 1) ?
                                Random.Range(leftTopCorner.y, leftTopCorner.y + 2) : Random.Range(rightBottomCorner.y, rightBottomCorner.y+2), 0);
        }
    
        public void SetRandomRotation(ref Transform _newObject)
        {
 	        _newObject.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
            //normal scale
            _newObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
}
