using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools
{
    /// <summary>
    /// A simple object pooler that extends the base class ObjectPooler
    /// </summary>
    public class SimpleObjectPooler : ObjectPooler
    {
        #region CLASS_VARIABLES

        /// the game object we'll instantiate, it has to be a prefab
        public GameObject gameObjectToPool;
        /// the number of objects we'll add to the pool
        public int poolSize = 20;
        /// if true, the pool will automatically add objects to the itself if needed
        public bool poolCanExpand = true;

        /// the actual object pool
        protected List<GameObject> _pooledGameObjects;

        #endregion

        #region CLASS_METHODS

        /// <summary>
        /// Fills the object pool with the gameobject type you've specified in the inspector
        /// </summary>
        public override void FillObjectPool()
        {
            if (gameObjectToPool == null)
            {
                return;
            }

            CreateWaitingPool();

            _pooledGameObjects = _objectPool.pooledGameObjects;

            for (int i = 0; i < poolSize; i++)
            {
                AddOneObjectToThePool();
            }
        }

        /// <summary>
        /// Determines the name of the object pool.
        /// </summary>
        /// <returns>The object pool name.</returns>
        protected override string DetermineObjectPoolName()
        {
            return ("[SimpleObjectPooler] " + this.name);
        }

        /// <summary>
        /// This method returns one inactive object from the pool
        /// </summary>
        /// <returns>The pooled game object.</returns>
        public override GameObject GetPooledGameObject()
        {
            // we go through the pool looking for an inactive object
            for (int i = 0; i < _pooledGameObjects.Count; i++)
            {
                if (!_pooledGameObjects[i].gameObject.activeInHierarchy)
                {
                    // if we find one, we return it
                    return _pooledGameObjects[i];
                }
            }
            // if we haven't found an inactive object (the pool is empty), and if we can extend it, we add one new object to the pool, and return it		
            if (poolCanExpand)
            {
                return AddOneObjectToThePool();
            }
            // if the pool is empty and can't grow, we return nothing.
            return null;
        }

        /// <summary>
        /// Adds one object of the specified type (in the inspector) to the pool.
        /// </summary>
        /// <returns>The one object to the pool.</returns>
        protected virtual GameObject AddOneObjectToThePool()
        {
            if (gameObjectToPool == null)
            {
                Debug.LogWarning("The " + gameObject.name + " ObjectPooler doesn't have any GameObjectToPool defined.", gameObject);
                return null;
            }

            GameObject newGameObject = (GameObject)Instantiate(gameObjectToPool);

            newGameObject.gameObject.SetActive(false);
            newGameObject.transform.SetParent(_waitingPool.transform);
            newGameObject.name = gameObjectToPool.name + "-" + _pooledGameObjects.Count;

            _pooledGameObjects.Add(newGameObject);

            return newGameObject;
        }

        #endregion
    }
}