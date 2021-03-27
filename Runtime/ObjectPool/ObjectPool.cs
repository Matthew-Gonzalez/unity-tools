using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools
{
    /// <summary>
    /// This class is used to store the objects of the pool
    /// </summary>
    public class ObjectPool : MonoBehaviour
    {
        #region CLASS_VARIABLES

        [ReadOnly]
        public List<GameObject> pooledGameObjects;

        #endregion
    }
}