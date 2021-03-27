using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools
{
    /// <summary>
    /// Add this class to an object you want to store in a pool. This object cannot be destroyed, instead it will be disabled
    /// </summary>
    public class PoolableObject : MonoBehaviour
    {
        #region CLASS_VARIABLES

        [Header("Poolable Object")]
        /// The life time of this object, in seconds. If the initial value is positive when it reaches 0 the object will be disabled, 
        /// if not the object will live for ever unless you deactivate it manually
        public float lifeTime = 0f;

        #endregion

        #region UNITY_METHODS

        /// <summary>
        /// When the objects get enabled the countdown starts
        /// </summary>
        protected virtual void OnEnable()
        {
            if (lifeTime > 0f)
            {
                Invoke("Destroy", lifeTime);
            }
        }

        /// <summary>
        /// When the object gets disabled before its programed "death" the invoke is cancelled
        /// </summary>
        protected virtual void OnDisable()
        {
            CancelInvoke();
        }

        /// <summary>
        /// Turns the instance inactive, in order to eventually reuse it.
        /// </summary>
        public virtual void Destroy()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}