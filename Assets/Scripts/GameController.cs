using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{

    public GameObject[] block;
    public List<GameObject> levels;

    private GameObject level1;
    private GameObject level2;

    private Vector2 level1Pos;
    private Vector2 level2Pos;

    private void Start()
    {
        Physics2D.gravity = new Vector2(0, -17);
        
        SpawnNewLevel(0, 17, 3, 5);
    }

    private void SpawnNewLevel(int numberLevel1, int numberLevel2, int min, int max)
    {
        level1Pos = new Vector2(3.5f, 1);
        level2Pos = new Vector2(3.5f, -3.4f);

        level1 = levels[numberLevel1];
        level2 = levels[numberLevel2];

        Instantiate(level1, level1Pos, Quaternion.identity);
        Instantiate(level2, level2Pos, Quaternion.identity);
        
        SetBlocksCount(min,max);
    }
    
    private void SetBlocksCount(int min, int max)
    {
        block = GameObject.FindGameObjectsWithTag("Block");

        for (int i = 0; i < block.Length; i++)
        {
            int count = Random.Range(min, max);
            block[i].GetComponent<Block>().SetStartingCount(count);
        }
    }



}
