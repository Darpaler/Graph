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
            CustomGraph customGraph = new CustomGraph();
            Node[] nodes = new Node[AMOUNT_OF_NODES];
            // Store costs for each node
            Dictionary<Node, double> costs = new Dictionary<Node, double>();

            for (int i = 0; i < AMOUNT_OF_NODES; i++)
            {
                nodes[i] = customGraph.AddNode();
            }

            customGraph.AddArc(nodes[0], nodes[1], Directedness.Directed);
            customGraph.AddArc(nodes[0], nodes[2], Directedness.Directed);
            customGraph.AddArc(nodes[1], nodes[3], Directedness.Directed);
            customGraph.AddArc(nodes[1], nodes[4], Directedness.Directed);
            customGraph.AddArc(nodes[2], nodes[3], Directedness.Directed);
            customGraph.AddArc(nodes[3], nodes[4], Directedness.Directed);
            customGraph.AddArc(nodes[3], nodes[5], Directedness.Directed);
            customGraph.AddArc(nodes[4], nodes[5], Directedness.Directed);
            customGraph.AddArc(nodes[5], nodes[6], Directedness.Directed);
            customGraph.AddArc(nodes[5], nodes[7], Directedness.Directed);
            customGraph.AddArc(nodes[6], nodes[7], Directedness.Directed);
            customGraph.AddArc(nodes[7], nodes[8], Directedness.Directed);
            customGraph.AddArc(nodes[8], nodes[9], Directedness.Directed);
            customGraph.AddArc(nodes[9], nodes[10], Directedness.Directed);
            customGraph.AddArc(nodes[10], nodes[11], Directedness.Directed);
            customGraph.AddArc(nodes[10], nodes[12], Directedness.Directed);
            customGraph.AddArc(nodes[10], nodes[13], Directedness.Directed);
            customGraph.AddArc(nodes[10], nodes[14], Directedness.Directed);
            customGraph.AddArc(nodes[14], nodes[15], Directedness.Directed);
            customGraph.AddArc(nodes[14], nodes[16], Directedness.Directed);
            customGraph.AddArc(nodes[16], nodes[17], Directedness.Directed);
            customGraph.AddArc(nodes[16], nodes[18], Directedness.Directed);
            customGraph.AddArc(nodes[18], nodes[19], Directedness.Directed);


            int n = 0;
            foreach (Node node in customGraph.Nodes())
            {
                // Assign a random cost to node
                costs[node] = weights[n++];
            }

            Console.WriteLine("Graph: ");
            foreach (Arc arc in customGraph.Arcs())
            {
                
                Console.Write("Edge: " + (customGraph.U(arc) + "->" + customGraph.V(arc)));
                Console.WriteLine(", Cost: " + (Math.Abs(costs[customGraph.U(arc)] - costs[customGraph.V(arc)])));
            }
            Console.WriteLine();
            
            // The start and goal of the graph
            Node start = nodes[0], end = nodes[19];

            // Set up Dijikstra
            Dijkstra dijkstra = new Dijkstra(customGraph, arc => Math.Abs(costs[customGraph.U(arc)] - costs[customGraph.V(arc)]), DijkstraMode.Sum);

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
                if (node != end)
                {
                    Console.Write(node + " -> ");
                }
                else
                {
                    Console.Write(node);
                }
            }
            Console.WriteLine();
        }
    }
}
