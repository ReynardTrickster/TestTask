using UnityEngine;

public class CellClickHandler : MonoBehaviour {
    private Cell cell;

    private void Start()
    {
        cell = gameObject.GetComponent<Cell>();
    }

    public void LeftClick()
    {
        cell.ToggleObstacle();
    }

    public void RightClick()
    {
        cell.ToggleTarget();
    }
}
