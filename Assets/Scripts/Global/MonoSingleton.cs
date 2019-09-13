using UnityEngine;
using System.Collections;


    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance = null;
        public static T Instance {
            get {
                if (instance == null)
                {
                    instance = FindObjectOfType(typeof(T)) as T;
                    if (instance == null)
                    {
                        instance = new GameObject("_" + typeof(T).Name).AddComponent<T>();
                        DontDestroyOnLoad(instance);
                    }
                    if (instance == null)
                        Debug.LogError("Failed to create instance of " + typeof(T).FullName + ".");
                }
                return instance;
            }
        }
}