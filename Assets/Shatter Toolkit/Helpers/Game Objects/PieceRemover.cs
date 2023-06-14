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
    [RequireComponent(typeof(ShatterTool))]
    public class PieceRemover : MonoBehaviour
    {
        public int startAtGeneration = 3;
        public float timeDelay = 5.0f;
        public bool whenOutOfViewOnly = true;
        
        protected ShatterTool shatterTool;
        protected new Renderer renderer;
        protected float timeSinceInstantiated;

        private float collisionBoxTimer = 0.1f; //remove collision boxes after this delay
        
        public void Start()
        {
            shatterTool = GetComponent<ShatterTool>();
            renderer = GetComponent<Renderer>();
        }
        
        public void Update()
        {
            if (shatterTool.Generation >= startAtGeneration)
            {
                timeSinceInstantiated += Time.deltaTime;

                if (timeSinceInstantiated >= collisionBoxTimer)
                {
                    Destroy(this.GetComponent<Collider>());
                }

                if (timeSinceInstantiated >= timeDelay)
                {
                    if (!whenOutOfViewOnly || !renderer.isVisible)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
