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
using UnityEngine;

using ShatterToolkit.Helpers;

namespace ShatterToolkit.Examples
{
    public class SceneGUI : MonoBehaviour
    {
        protected int toolbarSelection = 0;
        protected System.String[] toolbarLabels = { "Basic scene", "UvMapping scene", "Wall scene" };
        
        public void Awake()
        {
            toolbarSelection = Application.loadedLevel;
        }
        
        public void OnGUI()
        {
            toolbarSelection = GUI.Toolbar(new Rect(10, Screen.height - 30, Screen.width - 20, 20), toolbarSelection, toolbarLabels);
            
            if (GUI.changed)
            {
                Application.LoadLevel(toolbarSelection);
            }
        }
    }
}
