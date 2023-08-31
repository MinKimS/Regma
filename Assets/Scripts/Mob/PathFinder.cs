using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public Transform targetPos;
    Node[] nodes;
    public Transform pos;

    private void Start()
    {
        GameObject[] objNodes = GameObject.FindGameObjectsWithTag("Node");
        nodes = new Node[objNodes.Length];
        for (int i = 0; i < objNodes.Length; i++)
        {
            nodes[i] = objNodes[i].GetComponent<Node>();
        }
    }

    private void Update()
    {
        foreach (Node node in nodes)
        {
            node.IsWay(false);
        }

        Stack<Node> path = PathFinding(targetPos.position);
        while (path.Count > 0)
        {
            Node node = path.Pop();
            node.IsWay(true);
        }
    }

    Node FindClosestNode(Vector2 target)
    {
        Node closestNode = null;
        float minDist = float.MaxValue;

        for (int i = 0; i < nodes.Length; i++)
        {
            float dist = Vector2.Distance(target, nodes[i].transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closestNode = nodes[i];
            }
        }

        return closestNode;
    }


    public Stack<Node> PathFinding(Vector2 destination)
    {
        //initialization
        Stack<Node> path = new Stack<Node>();

        Node curNode = FindClosestNode(pos.position);
        Node desNode = FindClosestNode(destination);
        

        if (curNode == null || desNode == null || curNode == desNode)
        { return path; }

        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        List<Node> adjacentNodes = new List<Node>();

        openList.Add(curNode);
        curNode.SetParent(null);
        curNode.SetEuclideanDist(0f);

        while (openList.Count > 0)
        {
            openList.Remove(curNode); //출발점 제거
            closedList.Add(curNode);
            float dist = curNode.GetEuclideanDist();

            if (curNode ==  desNode) { break; }

            adjacentNodes = curNode.GetAdjacentNode(); //인접한 노드들
            for(int i = 0; i < adjacentNodes.Count; i++)
            {
                if (closedList.Contains(adjacentNodes[i])) { continue; }
                if (openList.Contains(adjacentNodes[i]))
                {
                    if (adjacentNodes[i].GetEuclideanDist() <= Vector2.Distance(curNode.transform.position, adjacentNodes[i].transform.position)) { continue; }
                }
                else
                {
                    openList.Add(adjacentNodes[i]);
                }

                adjacentNodes[i].SetParent(curNode);
                adjacentNodes[i].SetEuclideanDist(dist + Vector2.Distance(curNode.transform.position, adjacentNodes[i].transform.position));
                adjacentNodes[i].SetHeuristic(Vector2.Distance(adjacentNodes[i].transform.position, destination));
                adjacentNodes[i].SetHeuristicCostFun(adjacentNodes[i].GetEuclideanDist() + adjacentNodes[i].GetHeuristic());
            }   

            float smallestNodeValue = openList[0].GetHeuristicCostFun();
            int smallestNodeIdx = 0;
            for (int i = 0; i < openList.Count; i++)
            {
                if (smallestNodeValue > openList[i].GetHeuristicCostFun())
                {
                    smallestNodeValue = openList[i].GetHeuristicCostFun();
                    smallestNodeIdx = i;
                }
            }

            curNode = openList[smallestNodeIdx];
        }

        if(curNode == desNode)
        {
            while(curNode.GetParent() != null)
            {
                path.Push(curNode);
                curNode = curNode.GetParent();
            }
        }

        return path;
    }
}
