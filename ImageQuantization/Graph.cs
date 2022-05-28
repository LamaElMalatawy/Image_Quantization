using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace ImageQuantization
{
    class Graph
    {
        static List<RGBPixel> nodes = new List<RGBPixel>(); //O(1)
        static long dimensions; //O(1)
        
        public Graph() //O(N)
        {
            nodes = Global_Variables.distinctColors; //O(1)
            dimensions = nodes.Count; //O(1)

            Global_Variables.graph = new List<KeyValuePair<int, double>>[dimensions]; //O(1)

            // define a list for each node
            for (int i = 0; i < dimensions; i++) //O(N)
            {
                Global_Variables.graph[i] = new List<KeyValuePair<int, double>>(); //O(1)
            }
        }


        public void graphConstruction() //O(N^2)
        {

            for (int u = 0; u < dimensions; u++)  //O(N)
            {

                for (int v = u; v < dimensions; v++) //O(N)
                {
                    if (u != v) //O(1)
                    {
                        Global_Variables.graph[u].Add(new KeyValuePair<int, double>(v, Math.Sqrt(KCluster.euclideanDistanceSquared(nodes[u], nodes[v])))); //O(1)
                        Global_Variables.graph[v].Add(new KeyValuePair<int, double>(u, Math.Sqrt(KCluster.euclideanDistanceSquared(nodes[u], nodes[v])))); //O(1)

                    }
                    else
                    {
                        Global_Variables.graph[u].Add(new KeyValuePair<int, double>(v, 0)); //O(1)
                    }
                }
            }
        }


        public static void dfs(int node, int k) //O(E)
        {

            Global_Variables.visited[node] = true; //O(1)
            Global_Variables.Kclusters[k].Add(node); //O(1)

          
            for (int i = 0; i < Global_Variables.adj[node].Count; i++) //O(E)
            {
                if (!Global_Variables.visited[Global_Variables.adj[node][i]]) //O(1)
                {
                   dfs(Global_Variables.adj[node][i], k); //O(E)
                }
            }           

        
        }

    }

}
