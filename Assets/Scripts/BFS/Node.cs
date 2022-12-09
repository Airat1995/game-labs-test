using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.BFS
{
    [CreateAssetMenu(fileName = "Node", menuName = "Create Node", order = 0)]
    public class Node : ScriptableObject
    {
        public List<MoveLine> AvailableMoves;

        public List<Node> GetMoveNodes()
        {
            List<Node> nodes = new List<Node>();

            foreach (var moveNode in AvailableMoves)
            {
                nodes.Add(moveNode.FinishNode);
            }

            return nodes;
        }
    }
}