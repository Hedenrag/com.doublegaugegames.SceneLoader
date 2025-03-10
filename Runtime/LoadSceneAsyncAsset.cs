using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DGG
{
    namespace SceneLoader
    {
        [CreateAssetMenu(fileName = "LoadSceneAsset", menuName = "Scriptable Objects/Scene Loader/Load Scene Asset")]
        public class LoadSceneAsyncAsset : ScriptableObject
        {
#if UNITY_EDITOR
#pragma warning disable IDE0052
#pragma warning disable CS0414
            [SerializeField] bool Working = false;
#pragma warning restore CS0414
#pragma warning restore IDE0052
            [SerializeField] public SceneAsset[] sceneAssets;
#endif
            [SerializeField, HideInInspector] int[] sceneIndex;
            [SerializeField, HideInInspector] string[] sceneNames;

            public int[] SceneIndex => sceneIndex;
            public string[] SceneNames => sceneNames;

#if UNITY_EDITOR
            private void OnValidate()
            {
                sceneIndex = new int[sceneAssets.Length];
                sceneNames = new string[sceneAssets.Length];
                bool work = true;
                for (int i = 0; i < sceneAssets.Length; i++)
                {
                    if (sceneAssets[i] == null) { Working = false;  work = false; continue; }
                    sceneIndex[i] = SceneUtility.GetBuildIndexByScenePath(AssetDatabase.GetAssetPath(sceneAssets[i]));
                    if (SceneIndex[i] == -1) { Working =false; Debug.LogWarning($"Scene {i}: {sceneAssets[i].name} has not been added to build settings",this); work = false; }
                    sceneNames[i] = sceneAssets[i].name;
                }
                Working = work;
            }

            [MenuItem("Hedenrag/SceneManager/LinkScenes")]
            public static void LinkSceneLoaders()
            {
                LoadSceneAsyncAsset[] loadSceneAsyncAssets = DGG.GetItemsOfType.Editor.GetItemsOfType.GetAllInstances<LoadSceneAsyncAsset>();
                foreach (LoadSceneAsyncAsset asyncAsset in loadSceneAsyncAssets)
                {
                    asyncAsset.Relink();
                }
                SceneLoaderManager[] loaderManagers = DGG.GetItemsOfType.Editor.GetItemsOfType.GetAllInstances<SceneLoaderManager>();
                if(loaderManagers.Length == 0) { Debug.LogError("Never Unloading scenes asset has been deleted! Please reimport the asset!"); return; }
                loaderManagers[0].ReLink();
            }

            public void Relink()
            {
                OnValidate();
            }

#endif
        }
    }
}