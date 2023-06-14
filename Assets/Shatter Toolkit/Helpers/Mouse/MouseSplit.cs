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

namespace ShatterToolkit.Helpers
{
    public class MouseSplit : MonoBehaviour
    {
        public int raycastCount = 5;
        
        protected bool started = false;
        protected Vector3 start, end;
        
        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                start = Input.mousePosition;
                
                started = true;
            }
            
            if (Input.GetMouseButtonUp(0) && started)
            {
                end = Input.mousePosition;
                
                // Calculate the world-space line
                Camera mainCamera = Camera.main;
                
                float near = mainCamera.nearClipPlane;
                
                Vector3 line = mainCamera.ScreenToWorldPoint(new Vector3(end.x, end.y, near)) - mainCamera.ScreenToWorldPoint(new Vector3(start.x, start.y, near));
                
                // Find game objects to split by raycasting at points along the line
                for (int i = 0; i < raycastCount; i++)
                {
                    Ray ray = mainCamera.ScreenPointToRay(Vector3.Lerp(start, end, (float)i / raycastCount));
                    
                    RaycastHit hit;
                    
                    if (Physics.Raycast(ray, out hit))
                    {
                        Plane splitPlane = new Plane(Vector3.Normalize(Vector3.Cross(line, ray.direction)), hit.point);

                        hit.collider.SendMessage("Split", new Plane[] { splitPlane }, SendMessageOptions.DontRequireReceiver);
                        
                        break;
                    }
                }
                
                started = false;
            }
        }
    }
}
