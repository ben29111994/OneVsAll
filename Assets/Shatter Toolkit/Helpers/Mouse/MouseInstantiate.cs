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
    public class MouseInstantiate : MonoBehaviour
    {
        public GameObject prefabToInstantiate;
        
        public float speed = 7.0f;
        
        public void Update()
        {
            if (Input.GetMouseButtonDown(0) && prefabToInstantiate != null)
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                GameObject newGameObject = (GameObject)Instantiate(prefabToInstantiate, mouseRay.origin, Quaternion.identity);
                
                Rigidbody rb = newGameObject.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.velocity = mouseRay.direction * speed;
                }
            }
        }
    }
}
