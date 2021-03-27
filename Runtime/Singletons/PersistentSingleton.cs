using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools
{
    /// <summary>
    /// It works the same as the Generic Singleton, but this prevents the object from being destroyed when changing scenes
    /// </summary>
    /// <typeparam name="T">The class who extends this</typeparam>
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        #region CLASS_VARIABLES

        protected static T _instance;
        protected bool _enabled;

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

            if (_instance == null)
            {
                //If this is the first instance of the class, make it the Singleton
                _instance = this as T;
                DontDestroyOnLoad(transform.gameObject);
                _enabled = true;
            }
            else
            {
                //If a Singleton instance of this class already exists in the scene, destroy this instance
                if (this != _instance)
                {
                    Destroy(this.gameObject);
                }
            }
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
