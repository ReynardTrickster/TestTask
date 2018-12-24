using UnityEngine;

public class MapGenerator : MonoBehaviour {
    const int minMapSize = 5;
    const int maxMapSize = 10;
    [SerializeField]
    private GameObject cellPrefab;

    public Cell[,] Generate()
    {
        int mapSize = Random.Range(minMapSize, maxMapSize);
        Cell[,] map = new Cell[mapSize, mapSize];

        float offset = -0.5f + (float) mapSize / 2;
        Vector3 offsetVector = new Vector3(offset, 0.5f, offset);
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                Cell cell = Instantiate(cellPrefab, transform).GetComponent<Cell>();
                cell.Initialize(i, j);
                cell.gameObject.name = "Cell_" + i + "_" + j;
                cell.transform.localPosition += new Vector3(i, 0, j) - offsetVector;

                map[i, j] = cell;
            }
        }
        return map;
    }
}
