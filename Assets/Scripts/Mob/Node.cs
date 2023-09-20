using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Node : MonoBehaviour
{
    [SerializeField]
    private List<Node> neighbors;

    Node parent;
    float euclideanDist; //from startNode to curNode
    float heuristic; //from current node to destination node
    float heuristicCostFun; //euclideanDist + heuristic

    public void SetParent(Node parent) { this.parent = parent; }
    public Node GetParent() { return parent; }
    public void SetEuclideanDist(float distance) { this.euclideanDist = distance; }
    public float GetEuclideanDist() { return euclideanDist; }
    public void SetHeuristic(float heuristic) { this.heuristic = heuristic; }
    public float GetHeuristic() { return heuristic; }
    public void SetHeuristicCostFun(float heuristicCostFun) { this.heuristicCostFun = heuristicCostFun; }
    public float GetHeuristicCostFun() { return heuristicCostFun; }
    public void SetAdjacentNode(List<Node> neighbors) { this.neighbors = neighbors; }
    public List<Node> GetAdjacentNode() { return neighbors; }


    //------추후 지움------
#if UNITY_EDITOR


    private void OnDrawGizmos()
    {
        Handles.Label(transform.position, gameObject.name);

        if (neighbors == null) { return; }

        for (int i = 0; i < neighbors.Count; i++)
        {
            if (neighbors[i] != null)
            {
                Vector2 curNodePos = transform.position;
                Vector2 targetNodePos = neighbors[i].transform.position;

                //오른쪽에 노드가 인접함
                if (neighbors[i].transform.position.x >= transform.position.x)
                {
                    Gizmos.color = Color.red;
                }
                //왼쪽에 노드가 인접함
                else
                {
                    Gizmos.color = Color.blue;
                    //겹쳤을 때 보이게 하기 위해
                    curNodePos.x += 0.1f;
                    curNodePos.y += 0.1f;
                    targetNodePos.x += 0.1f;
                    targetNodePos.y += 0.1f;
                }

                //path
                Gizmos.DrawLine(curNodePos, targetNodePos);
            }
        }
    }
#endif
}
