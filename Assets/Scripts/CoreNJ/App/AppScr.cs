using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace nj
{ 
public static class AppScr
{
    public static float AspectMin = 1.5f;  //  for height
    public static float Aspect3_2 = 0.6666666667f;  //  for height
    public static float AspectW_H = 0.0f;  // for width
    public static float AspectH_W = 0.0f;  //  for height

    public static int Width = 0;
    public static int Height = 0;
    public static float OverWidth = 0;
    public static float OverHeight = 0;
    public static float HalfWidth = 0;
    public static float HalfHeight = 0;
    public static float HalfOverWidth = 0;
    public static float HalfOverHeight = 0;

    
    public static void Awake()
    {
        Width = Screen.width;
        Height = Screen.height;
        OverWidth = 1 / Width;
        OverHeight = 1 / Height;
        HalfWidth = Screen.width * 0.5f;
        HalfHeight = Screen.height * 0.5f;
        HalfOverWidth = 1 / HalfWidth;
        HalfOverHeight = 1 / HalfHeight;

        AspectW_H = (float)Screen.width / (float)Screen.height;
        AspectH_W = (float)Screen.height / (float)Screen.width;
    }
}
}