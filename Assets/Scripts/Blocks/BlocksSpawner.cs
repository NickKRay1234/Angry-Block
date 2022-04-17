using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BlocksSpawner : MonoBehaviour
{
    private Vector2 _coordinatesOfTheLowerBlocks = new Vector2(3.5f, 1f);
    private Vector2 _coordinatesOfTheUpperBlocks = new Vector2(3.5f, -3.4f);
    [SerializeField] private List<GameObject> _blockСombinations;
    [SerializeField] private GameObject[] _block;

    private void Start()
    {
        //Physics2D.gravity = new Vector2(0, -17);
        BlocksSpawn(0, 17, 3, 5);
    }

    private void BlocksSpawn(int numberLevel1, int numberLevel2, int min, int max)
    {
        Instantiate(_blockСombinations[numberLevel1], _coordinatesOfTheLowerBlocks, Quaternion.identity);
        Instantiate(_blockСombinations[numberLevel2], _coordinatesOfTheUpperBlocks, Quaternion.identity);
        
        SetBlocksCount(min,max);
    }
    
    private void SetBlocksCount(int min, int max)
    {
        _block = GameObject.FindGameObjectsWithTag("Block");

        for (int i = 0; i < _block.Length; i++)
        {
            int count = Random.Range(min, max);
            _block[i].GetComponent<Block>().SetStartingCount(count);
        }
    }



}
