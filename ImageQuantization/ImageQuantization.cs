using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class Image_Quantization
    {
        // To store the final image
        public static RGBPixel[,] finalPicture; //O(1)
        // To store the updated colors after clutsering
        public static RGBPixel[,,] updatedColors; //O(1)

        public static RGBPixel[,] ImageQuantization(RGBPixel[,] ImageMatrix) //O(N^2)
        {
            int height = ImageOperations.GetHeight(ImageMatrix);
            int width = ImageOperations.GetWidth(ImageMatrix);

            finalPicture = new RGBPixel[height, width]; //O(1)
            updatedColors = new RGBPixel[256, 256, 256]; //O(1)


            if (Global_Variables.K > 0)
            {
                for (int i = 0; i < Global_Variables.Kclusters.Length; i++) //O(N)
                {
                    for (int j = 0; j < Global_Variables.Kclusters[i].Count; j++) //O(N)
                    {
                        // get the updated color from all clusters
                        updatedColors[Global_Variables.distinctColors[Global_Variables.Kclusters[i][j]].red,
                                       Global_Variables.distinctColors[Global_Variables.Kclusters[i][j]].green,
                                       Global_Variables.distinctColors[Global_Variables.Kclusters[i][j]].blue] = Global_Variables.palette[i]; //O(1)
                    }
                }
            }

            for (int i = 0; i < height; i++) //O(N)
            {
                for (int j = 0; j < width; j++) //O(N)
                {
                    // store the final image with the new colors
                    if (Global_Variables.K != 0)
                        finalPicture[i, j] = updatedColors[ImageMatrix[i, j].red, ImageMatrix[i, j].green, ImageMatrix[i, j].blue]; //O(1)
                    else
                        finalPicture[i, j] = updatedColors[0, 0, 0]; //O(1)

                }

            }
            return finalPicture; //O(1)

        }
    }

}
