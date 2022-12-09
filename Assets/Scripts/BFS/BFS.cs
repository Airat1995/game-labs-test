using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.BFS
{
    class BFS : MonoBehaviour
    {
        [SerializeField]
        private Node _startNode;

        [SerializeField]
        private Node _endNode;

        private void Start()
        {
            var pathNodes = ShortestPathFunction(_startNode, _endNode);
            string pathString = "";

            int changePathCount = 0;
            int currentLineIndex = 0;
            for (int moveIndex = 0; moveIndex < pathNodes.Count - 1; moveIndex++)
            {
                Node startNode = pathNodes[moveIndex];
                Node moveToNode = pathNodes[moveIndex + 1];
                pathString += $"From: {startNode.name} To {moveToNode.name}\n";

                var moves = startNode.AvailableMoves;
                foreach (var path in moves)
                {
                    if (path.FinishNode == moveToNode)
                    {                                     //cutoff first movement index because
                                                          //player can move from any point and this point may contain 2 or more lines
                                                          //so first move we don't count as line change
                        if (currentLineIndex != path.LineIndex && moveIndex > 0)
                        {
                            changePathCount += 1;
                            currentLineIndex = moveIndex;
                        }
                    }
                }
            }

            Debug.Log($"Line changes count: {changePathCount}");
            
            Debug.Log($"Path: {pathString}");
        }

        private List<Node> ShortestPathFunction(Node start, Node end)
        {
            var previous = new Dictionary<Node, Node>();

            var queue = new Queue<Node>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                foreach (var neighbor in vertex.GetMoveNodes())
                {
                    if (previous.ContainsKey(neighbor))
                        continue;

                    previous[neighbor] = vertex;
                    queue.Enqueue(neighbor);
                }
            }
            var path = new List<Node> { };

            var current = end;
            while (!current.Equals(start))
            {
                path.Add(current);
                current = previous[current];
            };

            path.Add(start);
            path.Reverse();

            return path;
        }
    }
}
