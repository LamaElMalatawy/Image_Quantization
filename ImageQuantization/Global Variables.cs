using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ImageQuantization
{

    struct Graphs
    {
        public int u; //O(1)
        public int v; //O(1)
        public double edge; //O(1)
    }
    class Global_Variables
    {
        // struct for saving our graph
        public static Graphs[] graphs; //O(1)
        // adjacency list 
        public static List<int>[] adj; //O(1)
        // Graph
        public static List<KeyValuePair<int, double>>[] graph; //O(1)
        // List of image colors
        public static List<RGBPixel> colorsList = new List<RGBPixel>(); //O(1)
        // List of distinct colors
        public static List<RGBPixel> distinctColors = new List<RGBPixel>(); //O(1)
        // Duplicate list of distinct colors
        public static List<RGBPixel> dupDistinctColors = new List<RGBPixel>(); //O(1)
        // bool array to check visited colors
        public static bool[,,] visitedColors; //O(1)
        // MST sum
        public static double MST_sum = 0; //O(1)
        // Array of all edges in our MST
        public static double[] edges; //O(1)
        // Number of clusters
        public static int K; //O(1)
        // List of Clusters
        public static List<int>[] Kclusters; //O(1)
        // palette array
        public static RGBPixel[] palette; //O(1)
        // visited bool array for dfs
        public static bool[] visited; //O(1)
        // bonus
        public static RGBPixel[] updatedPallete; //O(1)

        // clear variables when opening a new image
        public static void clearOpen() //O(1)
        {
            K = 0; //O(1)
            MST_sum = 0; //O(1)
            adj = null; //O(1)
            visited = null; //O(1)
            graph = null; //O(1)
            Kclusters = null; //O(1)
            palette = null; //O(1)
            visitedColors = null; //O(1)
            colorsList.Clear(); //O(1)     
            distinctColors.Clear(); //O(1)

        }

        // clear variables after quantizing everytime
        public static void clearQuantized() //O(1)
        { 
            adj = null; //O(1)
            visited = null; //O(1)
            Kclusters = null; //O(1)
            visitedColors = null; //O(1)
            palette = null; //O(1)
           
           

        }
    }

}
