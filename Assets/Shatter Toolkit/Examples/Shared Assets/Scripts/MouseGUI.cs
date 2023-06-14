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
    [RequireComponent(typeof(MouseForce), typeof(MouseSplit), typeof(MouseShatter))]
    public class MouseGUI : MonoBehaviour
    {
        public int defaultSelection = 0;
        
        protected MouseForce mouseForce;
        protected MouseSplit mouseSplit;
        protected MouseShatter mouseShatter;
        
        protected int toolbarSelection = 0;
        protected System.String[] toolbarLabels = { "Mouse force (Click and drag)", "Mouse split (Click and drag, release)", "Mouse shatter (Click)" };
        
        public void Awake()
        {
            mouseForce = GetComponent<MouseForce>();
            mouseSplit = GetComponent<MouseSplit>();
            mouseShatter = GetComponent<MouseShatter>();
            
            toolbarSelection = defaultSelection;
            
            SelectTool();
        }
        
        public void OnGUI()
        {
            toolbarSelection = GUI.Toolbar(new Rect(10, 10, Screen.width - 20, 20), toolbarSelection, toolbarLabels);
            
            if (GUI.changed)
            {
                SelectTool();
            }
        }
        
        protected void SelectTool()
        {
            mouseForce.enabled = false;
            mouseSplit.enabled = false;
            mouseShatter.enabled = false;
            
            if (toolbarSelection == 0)
            {
                mouseForce.enabled = true;
            }
            else if (toolbarSelection == 1)
            {
                mouseSplit.enabled = true;
            }
            else if (toolbarSelection == 2)
            {
                mouseShatter.enabled = true;
            }
        }
    }
}
