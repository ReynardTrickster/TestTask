using UnityEngine;

public class Cell : MonoBehaviour {
    private bool isInitialized = false;
    public int X { get; private set; }
    public int Y { get; private set; }

    public bool IsObstacle { get; private set ; }
    public bool IsTarget { get; private set; }
    public bool IsOccupied { get; set; }

    public void Initialize(int x, int y)
    {
        if (isInitialized == false)
        {
            X = x;
            Y = y;
            isInitialized = true;
        }
    }

    public bool Equals(Cell cell)
    {
        return X == cell.X && Y == cell.Y;
    }

    private void Start()
    {
        IsObstacle = false;
        IsTarget = false;
    }

    public void ToggleTarget()
    {
        if (IsObstacle == true)
            return;

        if (IsTarget)
            IsOccupied = false;

        IsTarget = !IsTarget;
        SwitchColor();
    }

    public bool AnyUnitsOnCell()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject unit in units)
        {
            if (this == unit.gameObject.GetComponent<Unit>().GetCurrentCell())
                return true;
        }
        return false;
    }

    public void ToggleObstacle()
    {
        if (IsObstacle)
        {
            MoveDown();
        }
        else
        {
            if (AnyUnitsOnCell())
                return;

            IsTarget = false;
            IsOccupied = false;
            MoveUp();
        }
        IsObstacle = !IsObstacle;
        SwitchColor();
    }

    public void ToggleOccupied()
    {
        if (IsObstacle == true || IsTarget == false)
            return;

        IsOccupied = !IsOccupied;
        SwitchColor();
    }

    private void MoveUp()
    {
        transform.position += Vector3.up;
    }

    private void MoveDown()
    {
        transform.position += Vector3.down;
    }

    private Color getColor()
    {
        if (IsObstacle)
            return Color.black;
        if (IsTarget)
            return Color.green;
        if (IsOccupied)
            return new Color(255, 165, 0, 1);

        return Color.white;
    }

    private void SwitchColor()
    {
        gameObject.GetComponent<Renderer>().material.color = getColor();
    }
}
