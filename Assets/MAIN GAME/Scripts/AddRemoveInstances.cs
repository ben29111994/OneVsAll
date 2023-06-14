using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GPUInstancer
{
    public class AddRemoveInstances : MonoBehaviour
    {
        public static AddRemoveInstances instance;
        public GPUInstancerPrefab prefab;
        public GPUInstancerPrefabManager prefabManager;

        public Transform parentTransform;
        private int instanceCount;
        public List<GPUInstancerPrefab> instancesList = new List<GPUInstancerPrefab>();
        private string bufferName = "colorBuffer";

        private void OnEnable()
        {
            instance = this;
            Setup();
        }

        public void Setup()
        {
            // No prefab registration or GPU Instancer initialization is necessary since the manager instances the GPU Instancer prefabs automatically.
            // Here we are adding these prefabs to a list to manage add/remove operations later at runtime.
            instanceCount = parentTransform.childCount;
            if (prefabManager != null && prefabManager.isActiveAndEnabled)
            {
                GPUInstancerAPI.DefinePrototypeVariationBuffer<Color>(prefabManager, prefab.prefabPrototype, bufferName);
            }
            instancesList.Clear();
            var array = parentTransform.gameObject.GetComponentsInChildren<GPUInstancerPrefab>();
            foreach (var item in array)
            {
                var color = item.GetComponent<Renderer>().material.color;
                instancesList.Add(item);
                item.AddVariation(bufferName, color);
            }
            if (prefabManager != null && prefabManager.isActiveAndEnabled)
            {
                try
                {
                    GPUInstancerAPI.RegisterPrefabInstanceList(prefabManager, instancesList);
                    GPUInstancerAPI.InitializeGPUInstancer(prefabManager);
                }
                catch { }
            }
            RefreshColor();
        }

        public void RemoveInstances(GPUInstancerPrefab instanceCount)
        {
            try
            {
                GPUInstancerAPI.RemovePrefabInstance(prefabManager, instanceCount);
            }
            catch { }
            instancesList.Remove(instanceCount);
        }

        public void RefreshColor()
        {
            foreach(var item in instancesList)
            {
                var color = item.GetComponent<Renderer>().material.color;
                GPUInstancerAPI.UpdateVariation(prefabManager, item, bufferName, color);
            }
        }
    }
}

