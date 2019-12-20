using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using Satsuma;

namespace Graph
{
    // main class
    class Program
    {
        // Variables
        public static int AMOUNT_OF_NODES = 20;             // The amount of nodes to add to the graph
        private static Random random = new Random();        // Random number generator
        private static CompleteGraph graph = new CompleteGraph(AMOUNT_OF_NODES, Directedness.Undirected);     // The graph storing the nodes
        
        private static double[] weights =
        {
            0.5, 0.5, 0.7, 0.8, 0.6, 0.9, 0.4, 0.2, 0.01, 0.6, 0.6, 0.7, 0.6, 0.9, 0.4, 0.2, 0.01, 0.6, 0.6, 0.7
        };

        static void Main()
        {
            // Store costs for each node
            Dictionary<Node, double> costs = new Dictionary<Node, double>();

            int i = 0;
            foreach (Node node in graph.Nodes())
            {
                // Assign a random cost to node
                costs[node] = weights[i++];
            }


            // comment out when running actual code
            foreach (Arc arc in graph.Arcs())
            {
                Console.Write("Edge: " + (graph.U(arc) + "->" + graph.V(arc)));
                Console.WriteLine(", Cost: " + (Math.Abs(costs[graph.U(arc)] - costs[graph.V(arc)])));
            }

            // The start and goal of the graph
            Node start = graph.GetNode(0), end = graph.GetNode(18);

            // Set up Dijikstra
            Dijkstra dijkstra = new Dijkstra(graph, arc => Math.Abs(costs[graph.U(arc)] - costs[graph.V(arc)]), DijkstraMode.Sum);

            // Set the start for Dijikstra
            dijkstra.AddSource(start);
            // Calculate shortest path to the goal
            dijkstra.RunUntilFixed(end);

            // Display the cost of the path
            Console.WriteLine("Shortest Distance from start to end: " + dijkstra.GetDistance(end));

            Console.Write("Path from start to end: ");
            // Display each node in the path
            foreach (var node in dijkstra.GetPath(end).Nodes())
            {
                Console.Write(node + " -> ");
            }
            Console.WriteLine();
        }
    }
}
