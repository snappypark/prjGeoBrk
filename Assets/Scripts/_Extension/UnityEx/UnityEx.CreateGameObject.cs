using UnityEngine;

namespace UnityEngineEx
{
    public static class CreateGameObject
    {
        public static GameObject With(string name, Transform parent)
        {
            GameObject go = new GameObject(name + "(Clone)");
            go.transform.SetParent(parent);
            return go;
        }

        public static T With<T>(string name, Transform parent) where T : Component
        {
            GameObject go = new GameObject(name + "(Clone)");
            go.transform.SetParent(parent);
            return go.AddComponent<T>();
        }


        public static T With<T>(byte preload_idx, Transform preloads, Transform parent) where T : Component
        {
            return GameObject.Instantiate<T>(
               preloads.GetChild<T>(preload_idx),
               VectorEx.Huge, Quaternion.identity, parent);
        }

    }
}
