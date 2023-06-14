/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// Shatter Toolkit
// Copyright 2015 Gustav Olsson
using System.Collections.Generic;
using UnityEngine;

namespace ShatterToolkit
{
    public class SolidColorMapper : ColorMapper
    {
        /// <summary>
        /// Determines the vertex color to be used for the cut area
        /// </summary>
        public Color32 fillColor = Color.cyan;
        
        public override void Map(IList<Vector3> points, Vector3 planeNormal, out Color32[] colorsA, out Color32[] colorsB)
        {
            Color32[] colors = new Color32[points.Count];
            
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = fillColor;
            }
            
            colorsA = colors;
            colorsB = colors;
        }
    }
}
