using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DGG
{
    namespace SceneLoader
    {
        public class LoaderObject : MonoBehaviour
        {
            private void Awake()
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}