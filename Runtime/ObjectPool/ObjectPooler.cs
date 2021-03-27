using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools
{
    /// <summary>
    /// The base class of an object pooler. Extends this class to create your own object pooler classes
    /// </summary>
    public class ObjectPooler : MonoBehaviour
    {
        #region CLASS_VARIABLES

        /// this object is just used to group the pooled objects
        protected GameObject _waitingPool = null;
        protected ObjectPool _objectPool;

        #endregion

        #region UNITY_METHODS

        /// <summary>
        /// On awake we fill our object pool
        /// </summary>
        protected virtual void Awake()
        {
            FillObjectPool();
        }

        #endregion

        #region CLASS_METHODS

        /// <summary>
        /// Creates the waiting pool or tries to reuse one if there's already one available
        /// </summary>
        protected virtual void CreateWaitingPool()
        {
            // we create a container that will hold all the instances we create
            _waitingPool = new GameObject(DetermineObjectPoolName());
            _objectPool = _waitingPool.AddComponent<ObjectPool>();
            _objectPool.pooledGameObjects = new List<GameObject>();
        }

        /// <summary>
        /// Determines the name of the object pool.
        /// </summary>
        /// <returns>The object pool name.</returns>
        protected virtual string DetermineObjectPoolName()
        {
            return ("[ObjectPooler] " + this.name);
        }

        /// <summary>
        /// Implement this method to fill the pool with objects
        /// </summary>
        public virtual void FillObjectPool()
        {
            return;
        }

        /// <summary>
        /// Implement this method to return a gameobject
        /// </summary>
        /// <returns>The pooled game object.</returns>
        public virtual GameObject GetPooledGameObject()
        {
            return null;
        }

        /// <summary>
        /// Destroys the object pool
        /// </summary>
        public virtual void DestroyObjectPool()
        {
            if (_waitingPool != null)
            {
                Destroy(_waitingPool.gameObject);
            }
        }

        #endregion
    }
}