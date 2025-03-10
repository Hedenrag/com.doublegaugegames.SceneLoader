using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DGG
{
    namespace SceneLoader
    {
        public class ExitGame : MonoBehaviour
        {
            public void Quit()
            {
#if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
#else
                Application.Quit();
#endif
            }
        }
    }
}