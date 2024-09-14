using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : Singleton<MazeGenerator>
{
    [SerializeField] private List<GameObject> mazes;

    // Start is called before the first frame update
    void Start()
    {
        int mazeToSpawnRandIndex = Random.Range(0, mazes.Count);

        Instantiate(mazes[mazeToSpawnRandIndex]);
        Destroy(this.gameObject);
    }
}
