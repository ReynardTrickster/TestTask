using UnityEngine;

public class Map : MonoBehaviour {
    public int MapSize { get; private set; }
    public Cell[,] Cells { get; private set; }

    void Start ()
    {
        MapGenerator generator = gameObject.GetComponent<MapGenerator>();
        Cells = generator.Generate();
        MapSize = (int) Mathf.Sqrt(Cells.Length);
    }
}
