using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools
{
    /// <summary>
    /// Extend this class when you need to directly access it from any other class without looking for it in the scene, 
    /// this means that you can only have one instance of this class in the scene at a time
    /// </summary>
    /// <typeparam name="T">The class who extends this</typeparam>
    public class GenericSingleton<T> : MonoBehaviour where T : Component
    {
        #region CLASS_VARIABLES

        protected static T _instance;

        #endregion

        #region UNITY_METHODS

        /// <summary>
        /// Initialize the instance. Do not forget to call base.Awake() in override if you need this method
        /// </summary>
        protected virtual void Awake()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            _instance = this as T;
        }

        #endregion

        #region GET_SET

        /// <summary>
        /// Singleton design pattern
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        #endregion
    }
}