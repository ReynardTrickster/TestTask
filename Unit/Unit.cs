using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    public enum Behavior { Journey, Competition, Occupation, Confusing }

    public Queue<Cell> Route { get; private set; }
    private Cell nextCell;
    private Cell destinationCell;
    private float speed = 1;

    public Behavior PreviousBehavior { get; private set; }
    public Behavior CurrentBehavior { get; private set; }

    public void ChangeBehavior(Behavior behavior)
    {
        PreviousBehavior = CurrentBehavior;
        if (PreviousBehavior  == Behavior.Occupation)
        {
            GetCurrentCell().ToggleOccupied();
        }
        CurrentBehavior = behavior;
        switch (behavior)
        {
            case Behavior.Journey:
                StartJourney();
                break;
            case Behavior.Competition:
                StartJourney();
                break;
            case Behavior.Occupation:
                StartOccupation();
                break;
            case Behavior.Confusing:
                StartConfusing();
                break;
        }
        SwitchColor();
    }

    private void Start()
    {
        Route = new Queue<Cell>();
        ChangeBehavior(Behavior.Journey);
    }

    void Update () {
        if (CurrentBehavior == Behavior.Journey)
        {
            if (isOnCellCentre(destinationCell) || nextCell.IsObstacle)
            {
                StartJourney();
                return;
            }

            MoveToTheNextCell();
        }

        if (CurrentBehavior == Behavior.Competition)
        {
            if (nextCell.IsObstacle || destinationCell.IsOccupied)
            {
                StartJourney();
                return;
            }
            if (isOnCellCentre(destinationCell))
            {
                destinationCell.ToggleOccupied();
                ChangeBehavior(Behavior.Occupation);
                return;
            }
            MoveToTheNextCell();
        }

        if (CurrentBehavior == Behavior.Occupation)
        {

        }

        if (CurrentBehavior == Behavior.Confusing)
        {

        }
	}

    private void MoveToTheNextCell()
    {
        if (isOnCellCentre(nextCell))
        {
            nextCell = Route.Dequeue();
        }
        float x = nextCell.transform.position.x;
        float y = transform.position.y;
        float z = nextCell.transform.position.z;
        Vector3 destination = new Vector3(x, y, z);

        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }
  
    public bool isOnCellCentre(Cell cell)
    {
        return transform.position.x == cell.transform.position.x && transform.position.z == cell.transform.position.z;
    }

    public Cell GetCurrentCell()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 5))
            return hit.transform.GetComponent<Cell>();
        else
            return null;
    }

    void StartJourney()
    {
        Map map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        RouteFinder pathFinder = new RouteFinder(map);

        Cell currentCell = GetCurrentCell();
        if (currentCell == null)
        {
            ChangeBehavior(Behavior.Confusing);
            return;
        }
        if (CurrentBehavior == Behavior.Journey)
            Route = pathFinder.getRandom(currentCell);
        else
            Route = pathFinder.getNearTarget(currentCell);

        if (Route.Count > 0)
        {
            destinationCell = Route.ToArray()[Route.Count -1];
            nextCell = Route.Dequeue();
        }
        else {
            ChangeBehavior(Behavior.Confusing);
        }
    }

    private Color getColor()
    {
        switch (CurrentBehavior)
        {
            case Behavior.Confusing:
                return Color.red;
            case Behavior.Journey:
                return Color.yellow;
            case Behavior.Occupation:
                return Color.green;
            case Behavior.Competition:
                return Color.blue;
            default:
                return Color.white;
        }
    }

    private void SwitchColor()
    {
        gameObject.GetComponent<Renderer>().material.color = getColor();
    }

    void StartOccupation()
    {

    }

    void StartConfusing()
    {

    }
}
