using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ImageQuantization
{



    class MST_Graph
    {
        // number of vertices in the graph
        private static int vertices; //O(1)

        // using prim's algorithm for mst construction
        public static void MST_Construction() //O(N^2)
        {
            
            vertices = Global_Variables.distinctColors.Count; //O(1)
            
            Global_Variables.graphs = new Graphs[vertices]; //O(1)

            Global_Variables.MST_sum = 0; //O(1)

            // stores constructed MST eventually
            int[] parent = new int[vertices]; //O(1)

            // stores edges connecting our MST
            Global_Variables.edges = new double[vertices]; //O(1)

            // the state of every vertix ( included / not included in our MST)
            bool[] isVisited = new bool[vertices]; //O(1)


            // Set isVisited initally for each value to false, and give every edge a very large value 
            for (int i = 0; i < vertices; i++) //O(N)
            {
                isVisited[i] = false; //O(1)
                Global_Variables.edges[i] = double.MaxValue; //O(1)

            }

            // to set the first node 
            parent[0] = -1;                //O(1)
            Global_Variables.edges[0] = 0; //O(1)


            for (int i = 0; i < vertices - 1; i++) //O(N)
            {
                // get the min node not yet included in our MST
                int smallestNode = getSmallestNode(isVisited, Global_Variables.edges); //O(N)

                // add the returned min node to our MST
                isVisited[smallestNode] = true; //O(1)

                for (int j = 0; j < vertices; j++) //O(N)
                {
                    // check if this vertex is not yet included in our MST & has a smaller edge 
                    if (!isVisited[j] && KCluster.euclideanDistanceSquared(Global_Variables.distinctColors[smallestNode], Global_Variables.distinctColors[j]) != 0 && KCluster.euclideanDistanceSquared(Global_Variables.distinctColors[smallestNode], Global_Variables.distinctColors[j]) < Global_Variables.edges[j]) //O(1)
                    {
                        // assign this node as the child of the smallest node
                        parent[j] = smallestNode; //O(1)
                        // get the euclidean distance between this node and the smallest node
                        Global_Variables.edges[j] = KCluster.euclideanDistanceSquared(Global_Variables.distinctColors[smallestNode], Global_Variables.distinctColors[j]); //O(1)
                    }
                }
            }

            
            // fill the mst
            for (int i = 1; i < vertices; i++) //O(N)
            {
                Global_Variables.graphs[i].u = parent[i]; //O(1)
                Global_Variables.graphs[i].v = i; //O(1)
                Global_Variables.graphs[i].edge = Math.Sqrt(KCluster.euclideanDistanceSquared(Global_Variables.distinctColors[parent[i]], Global_Variables.distinctColors[i])); //O(1)

                // mst sum
                Global_Variables.MST_sum += Math.Sqrt(KCluster.euclideanDistanceSquared(Global_Variables.distinctColors[parent[i]], Global_Variables.distinctColors[i])); //O(1)

            }             

        }


        // iterate over every edge not yet included in our MST to get the smallest one
        public static int getSmallestNode(bool[] visited, double[] edges) //O(N)
        {
            double min = double.MaxValue; //O(1)
            int index = -1; //O(1)

            for (int i = 0; i < vertices; i++) //O(N)
            {
                if (!visited[i] && edges[i] < min) //O(1)
                {
                    min = edges[i]; //O(1)
                    index = i; //O(1)
                }
            }
            return index; //O(1)
        }


    }
}
