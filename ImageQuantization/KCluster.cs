using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace ImageQuantization
{

    class KCluster
    {

        public static void createCluster(int K) //O(N^2)
        {

            // sort descendingly
            Array.Sort(Global_Variables.graphs, (x, y) => x.edge.CompareTo(y.edge)); //O(NLogN)
            Array.Reverse(Global_Variables.graphs); //O(N)

            // Get the number of clusters needed
            K = Global_Variables.K; //O(1)

            // Build adjacency list of the new clusters
            Global_Variables.adj = new List<int>[Global_Variables.graphs.Length]; //O(1)

            for (int i = 0; i < Global_Variables.graphs.Length; i++) //O(N)
            {
                Global_Variables.adj[i] = new List<int>(); //O(1)
            }

            // Start after K-1 biggest edges
            for (int i = K - 1; i < Global_Variables.graphs.Length; i++) //O(N)
            {
                if (Global_Variables.graphs[i].u == Global_Variables.graphs[i].v) //O(1)
                    continue;
                Global_Variables.adj[Global_Variables.graphs[i].u].Add(Global_Variables.graphs[i].v); //O(1)
                Global_Variables.adj[Global_Variables.graphs[i].v].Add(Global_Variables.graphs[i].u); //O(1)
            }

            // bool array for dfs
            Global_Variables.visited = new bool[Global_Variables.graphs.Length]; //O(1)
            // array of lists for storing our clusters
            Global_Variables.Kclusters = new List<int>[Global_Variables.K]; //O(1)

            for (int i = 0; i < Global_Variables.K; i++) //O(N)
            {
                Global_Variables.Kclusters[i] = new List<int>(); //O(1)
            }

            // create clusters
            addToCluster(); //O(V+E)

            // initialize new color palette
            Global_Variables.palette = new RGBPixel[Global_Variables.K]; //O(1)

            for (int i = 0; i < Global_Variables.Kclusters.Length; i++) //O(N)
            {
                // get cluster's centroid
                getClusterColor(i); //O(N)

            }


        }

        // create clusters
        public static void addToCluster() //O(V+E)
        {
            // number of components
            int k = 0; //O(1)
            for (int i = 0; i < Global_Variables.distinctColors.Count; i++) //O(V)
            {
                if (!Global_Variables.visited[i]) //O(1)
                {
                    Graph.dfs(i, k); //O(E)
                    ++k; //O(1)
                }
            }

        }


        // get the average color of each cluster
        public static void getClusterColor(int k) //O(N)
        {
            double R = 0.0; //O(1)
            double G = 0.0; //O(1)
            double B = 0.0; //O(1)


            int count = Global_Variables.Kclusters[k].Count; //O(1)

            for (int i = 0; i < count; i++) //O(N)
            {

                R += Global_Variables.distinctColors[Global_Variables.Kclusters[k][i]].red; //O(1)
                G += Global_Variables.distinctColors[Global_Variables.Kclusters[k][i]].green; //O(1)
                B += Global_Variables.distinctColors[Global_Variables.Kclusters[k][i]].blue; //O(1)

            }

            R /= Global_Variables.Kclusters[k].Count; //O(1)
            G /= Global_Variables.Kclusters[k].Count; //O(1)
            B /= Global_Variables.Kclusters[k].Count; //O(1)

            RGBPixel pixel = new RGBPixel(); //O(1)
            pixel.red = (byte)R; //O(1)
            pixel.green = (byte)G; //O(1)
            pixel.blue = (byte)B; //O(1)

            // save the new color in our pallet
            Global_Variables.palette[k] = pixel; //O(1)

        }


        // calculate the euclidean distance between two colors
        public static int euclideanDistanceSquared(RGBPixel color1, RGBPixel color2) //O(1)
        {
            int distance = (color1.red - color2.red) * (color1.red - color2.red) + (color1.green - color2.green) * (color1.green - color2.green) + (color1.blue - color2.blue) * (color1.blue - color2.blue); //O(1)
            return distance; //O(1)
        }

                                                                 // Bonus //

        // get K automatically
        public static int getK() //O(N)
        {

            // edges of our graph
            List<double> edges = new List<double>(); //O(1)

            for (int i = 0; i < Global_Variables.edges.Length; i++) //O(N)
                edges.Add(Math.Sqrt(Global_Variables.edges[i])); //O(1)

            double cost = 0; //O(1)
            double removedEdge = 0; //O(1)
            double STDdev = 0; //O(1)
            double reducedSTDdev = 0; //O(1)
            double mean = 0; //O(1)
            int K = 0; //O(1)

            for (int i = 0; i < edges.Count; i++) //O(N)
                mean += edges[i]; //O(1)


            mean /= edges.Count; //O(1)


            for (int i = 0; i < edges.Count; i++) //O(N)
            {
                if ((edges[i] - mean) * (edges[i] - mean) > cost) //O(1)
                {
                    cost = (edges[i]- mean) * (edges[i] - mean); //O(1)
                    removedEdge = edges[i]; //O(1)
                }
                STDdev += (edges[i] - mean) * (edges[i] - mean); //O(1)
            }

            cost = 0;//O(1)
            STDdev /= edges.Count - 1; //O(1)
            STDdev = Math.Sqrt(STDdev); //O(1)

            while (Math.Abs(STDdev - reducedSTDdev) > 0.0001)
            {
                edges.Remove(removedEdge); //O(N)
                reducedSTDdev = STDdev; //O(1)

                mean = 0; //O(1)
                for (int i = 0; i < edges.Count; i++) //O(N)
                    mean += edges[i]; //O(1)
               

                mean /= edges.Count(); //O(1)

                STDdev = 0; //O(1)
                for (int i = 0; i < edges.Count; i++) //O(N)
                {
                    if ((edges[i] - mean) * (edges[i] - mean) > cost) //O(1)
                    {
                        cost = (edges[i] - mean) * (edges[i] - mean); //O(1)
                        removedEdge = edges[i]; //O(1)
                    }
                    STDdev += (edges[i] - mean) * (edges[i] - mean); //O(1)
                }

                cost = 0; //O(1)

                STDdev /= (edges.Count() - 1); //O(1)
                STDdev = Math.Sqrt(STDdev); //O(1)

                ++K; //O(1)

            }
            


            return K; //O(1)



        }

        public static int addToKCluster() //O(V+E)
        {
            // number of components
            int k = 0; //O(1)
            for (int i = 0; i < Global_Variables.adj.Length; i++) //O(V)
            {
                if (!Global_Variables.visited[i]) //O(1)
                {
                    Graph.dfs(i, k); //O(E)
                    ++k; //O(1)
                }
            }

            return k;
        }

        public static void createKClusters(int K) //O(N^2)
        {
            int Q = 0; //O(1)

            while (Q != Global_Variables.K) //O(N)
            {
                Q = 1; //O(1)

                MST_Graph.MST_Construction(); //O(V^2)

                // Build adjacency list of the new clusters
                Global_Variables.adj = new List<int>[Global_Variables.graphs.Length]; //O(1)

                // sort descendingly
                Array.Sort(Global_Variables.graphs, (x, y) => x.edge.CompareTo(y.edge)); //O(NLogN)
                Array.Reverse(Global_Variables.graphs); //O(N)
                for (int i = 0; i < Global_Variables.graphs.Length; i++) //O(N)
                    Global_Variables.adj[i] = new List<int>(); //O(1)


                double mean = 0; //O(1)
                double STDdev = 0; //O(1)
                int edgesCount = -1; //O(1)

                for (int i = 0; i < Global_Variables.graphs.Length; i++) //O(N)
                {

                    if (Global_Variables.graphs[i].edge != -1) //O(1)
                    {
                        //Console.WriteLine(Global_Variables.graphs[i].edge);
                        mean += Global_Variables.graphs[i].edge; //O(1)
                        ++edgesCount; //O(1)
                    }
                }

                mean /= edgesCount; //O(1)

                for (int i = 0; i < Global_Variables.graphs.Length; i++) //O(N)
                {
                    if (Global_Variables.graphs[i].edge != -1) //O(1)
                    {
                        STDdev += (Global_Variables.graphs[i].edge - mean) * (Global_Variables.graphs[i].edge - mean); //O(1)
                    }
                }

                STDdev /= edgesCount; //O(1)
                STDdev = Math.Sqrt(STDdev); //O(1)

                double check = mean + STDdev; //O(1)

                for (int i = 0; i < Global_Variables.graphs.Length; i++) //O(N)
                {
                    if (Global_Variables.graphs[i].edge > check) //O(1)
                    {
                        Global_Variables.graphs[i].edge = -1; //O(1)
                        ++Q; //O(1)
                    }
                }



                if (Q <= Global_Variables.K) //O(1)
                {

                    for (int i = 0; i < Global_Variables.graphs.Length; i++) //O(N)
                    {
                        if (Q == Global_Variables.K) //O(1)
                            break;

                        if (Global_Variables.graphs[i].edge != -1) //O(1)
                        {
                            Global_Variables.graphs[i].edge = -1; //O(1)
                            ++Q; //O(1)
                        }
                    }

                    for (int i = 0; i < Global_Variables.graphs.Length; i++) //O(N)
                    {
                        if (Global_Variables.graphs[i].edge != -1) //O(1)
                        {
                            if (Global_Variables.graphs[i].u == Global_Variables.graphs[i].v) //O(1)
                                continue; //O(1)
                            Global_Variables.adj[Global_Variables.graphs[i].u].Add(Global_Variables.graphs[i].v); //O(1)
                            Global_Variables.adj[Global_Variables.graphs[i].v].Add(Global_Variables.graphs[i].u); //O(1)

                        }
                    }
                    // bool array for dfs
                    Global_Variables.visited = new bool[Global_Variables.graphs.Length]; //O(1)
                    // array of lists for storing our clusters
                    Global_Variables.Kclusters = new List<int>[Global_Variables.K]; //O(1)

                    for (int i = 0; i < Global_Variables.K; i++) //O(N)
                    {
                        Global_Variables.Kclusters[i] = new List<int>(); //O(1)
                    }

                    addToCluster(); //O(V+E)

                    // initialize new color palette
                    Global_Variables.palette = new RGBPixel[Global_Variables.K]; //O(1)
                    // initialize new color palette
                    Global_Variables.updatedPallete = new RGBPixel[Q]; //O(1)


                    for (int i = 0; i < Global_Variables.Kclusters.Length; i++) //O(N)
                        getClusterColor(i); //O(N)

                }
                else if (Q > Global_Variables.K) //O(1)
                {

                    for (int i = 0; i < Global_Variables.graphs.Length; i++) //O(N)
                    {
                        if (Global_Variables.graphs[i].edge != -1) //O(1)
                        {
                            if (Global_Variables.graphs[i].u == Global_Variables.graphs[i].v) //O(1)
                                continue; //O(1)
                            Global_Variables.adj[Global_Variables.graphs[i].u].Add(Global_Variables.graphs[i].v); //O(1)
                            Global_Variables.adj[Global_Variables.graphs[i].v].Add(Global_Variables.graphs[i].u); //O(1)

                        }
                    }
                    // bool array for dfs
                    Global_Variables.visited = new bool[Global_Variables.graphs.Length]; //O(1)
                    // array of lists for storing our clusters
                    Global_Variables.Kclusters = new List<int>[Q]; //O(1)

                    for (int i = 0; i < Q; i++) //O(N)
                        Global_Variables.Kclusters[i] = new List<int>(); //O(1)

                    // return Q clusters
                    Console.WriteLine(addToKCluster()); //O(V+E)

                    // initialize new color palette
                    Global_Variables.palette = new RGBPixel[Q]; //O(1)
                    // initialize new color palette
                    Global_Variables.updatedPallete = new RGBPixel[Q]; //O(1)

                    for (int i = 0; i < Global_Variables.Kclusters.Length; i++) //O(N)
                        getKClusterColor(i); //O(N)

                    // update distinct colors with centroids of our clusters                    
                    Global_Variables.distinctColors = Global_Variables.updatedPallete.ToList(); //O(N)

                }

            }
        }

        public static void getKClusterColor(int k) //O(N)
        {
            double R = 0.0; //O(1)
            double G = 0.0; //O(1)
            double B = 0.0; //O(1)


            int count = Global_Variables.Kclusters[k].Count; //O(1)

            for (int i = 0; i < count; i++) //O(N)
            {

                R += Global_Variables.distinctColors[Global_Variables.Kclusters[k][i]].red; //O(1)
                G += Global_Variables.distinctColors[Global_Variables.Kclusters[k][i]].green; //O(1)
                B += Global_Variables.distinctColors[Global_Variables.Kclusters[k][i]].blue; //O(1)

            }

            R /= Global_Variables.Kclusters[k].Count; //O(1)
            G /= Global_Variables.Kclusters[k].Count; //O(1)
            B /= Global_Variables.Kclusters[k].Count; //O(1)

            RGBPixel pixel = new RGBPixel(); //O(1)
            pixel.red = (byte)R; //O(1)
            pixel.green = (byte)G; //O(1)
            pixel.blue = (byte)B; //O(1)

            // save the new color in our pallet
            Global_Variables.palette[k] = pixel; //O(1)

            RGBPixel color = new RGBPixel(); //O(1)
            RGBPixel centroid = new RGBPixel(); //O(1)
            double minDist = double.MaxValue; //O(1)


            for (int i = 0; i < Global_Variables.Kclusters[k].Count; i++) //O(N)
            {
                color.red = Global_Variables.distinctColors[Global_Variables.Kclusters[k][i]].red; //O(1)
                color.blue = Global_Variables.distinctColors[Global_Variables.Kclusters[k][i]].blue; //O(1)
                color.green = Global_Variables.distinctColors[Global_Variables.Kclusters[k][i]].green; //O(1)

                minDist = Math.Min(euclideanDistanceSquared(color, pixel), minDist); //O(1)
                if (minDist == euclideanDistanceSquared(color, pixel)) //O(1)
                    centroid = color; //O(1)
            }


            Global_Variables.updatedPallete[k] = centroid; //O(1)
        }

    }


}
