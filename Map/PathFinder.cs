using System;
using System.Collections.Generic;
using UnityEngine;

// Dijkstra Algorithm
public class RouteFinder{
    private Map map;
    private int inf;
    private int[,] scoreMap;
    private List<int[]> routeBack;

    const int maxAttemps = 10;

    public RouteFinder(Map map)
    {
        this.map = map;
        inf = map.MapSize * map.MapSize; 
    }

    private void InitScoreMap()
    {
        scoreMap = new int[map.MapSize, map.MapSize];

        for (int i = 0; i < map.MapSize; i++)
        {
            for (int j = 0; j < map.MapSize; j++)
                if (map.Cells[i,j].IsObstacle)
                    scoreMap[i, j] = -1;
                else
                    scoreMap[i, j] = inf;
        }
    }

    public Queue<Cell> getNearTarget(Cell from)
    {
        Queue<Cell> route = new Queue<Cell>();

        InitScoreMap();

        if (scoreMap[from.X, from.Y] == -1)
            return route;

        scoreMap[from.X, from.Y] = 0;

        ScoreRoute(from.X, from.Y);

        int minValue = inf;
        Cell to = null;
        for (int i = 0; i < map.MapSize; i++)
        {
            for (int j = 0; j < map.MapSize; j++)
            {
                if(minValue > scoreMap[i,j] && map.Cells[i,j].IsTarget && map.Cells[i, j].IsOccupied == false)
                {
                    minValue = scoreMap[i, j];
                    to = map.Cells[i, j];
                }
            }
        }
        if (to == null)
            return route;

        return FindPath(from, to);
    }

    public Queue<Cell> getRandom(Cell from)
    {
        for (int i = 0; i < maxAttemps; i++)
        {
            int x = UnityEngine.Random.Range(0, map.MapSize);
            int y = UnityEngine.Random.Range(0, map.MapSize);

            if (from.X == x && from.Y == y)
                continue;

            Cell to = map.Cells[x, y];
            if (to.IsObstacle == true)
                continue;

            Queue<Cell> route = FindPath(from, to);
            if (route.Count > 0)
                return route;
        }
        return new Queue<Cell>();
    }

    private Queue<Cell> FindPath(Cell from, Cell to)
    {
        Queue<Cell> route = new Queue<Cell>();

        InitScoreMap();

        if (scoreMap[from.X, from.Y] == -1 || scoreMap[to.X, to.Y] == -1)
            return route;

        scoreMap[from.X, from.Y] = 0;

        ScoreRoute(from.X, from.Y);

        if (scoreMap[to.X, to.Y] == inf)
            return route;

        routeBack = new List<int[]>();

        int[] point = new int[] { to.X, to.Y };
        routeBack.Add(point);
        FindRouteBack(to.X, to.Y);

        if (routeBack.Count > 1)
            routeBack.RemoveAt(routeBack.Count - 1);
        routeBack.Reverse();

        foreach (int[] p in routeBack)
        {
            route.Enqueue(map.Cells[p[0], p[1]]);
        }

        return route;
    }

    private List<int[]> getNeighbors(int x, int y)
    {
        List<int[]> points = new List<int[]>();
        if (x != 0)
        {
            int[] p = new int[] { x - 1, y };
            points.Add(p);
        }
        if (x != map.MapSize - 1)
        {
            int[] p = new int[] { x + 1, y };
            points.Add(p);
        }
        if (y != 0)
        {
            int[] p = new int[] { x, y - 1 };
            points.Add(p);
        }
        if (y != map.MapSize - 1)
        {
            int[] p = new int[] { x, y + 1 };
            points.Add(p);
        }
        return points;
    }

    private void ScoreRoute(int x, int y)
    {
        int value = scoreMap[x, y];

        foreach (int[] p in getNeighbors(x,y))
        {
            if ((scoreMap[p[0], p[1]] != -1) && (scoreMap[p[0], p[1]] > value + 1))
            {
                scoreMap[p[0], p[1]] = value + 1;
                ScoreRoute(p[0], p[1]);
            }
        }
    }

    private void FindRouteBack(int x, int y)
    {
        int value = scoreMap[x, y];
        List<int[]> points = getNeighbors(x, y);

        int minValue = value;
        foreach (int[] p in points)
        {
            if ((scoreMap[p[0], p[1]] != -1))
            {
                minValue = Math.Min(minValue, scoreMap[p[0], p[1]]);
            }
        }

        foreach (int[] p in points)
        {
            if (minValue == scoreMap[p[0], p[1]])
            {
                routeBack.Add(p);
                FindRouteBack(p[0], p[1]);
                break;
            }
        }
    }
}
