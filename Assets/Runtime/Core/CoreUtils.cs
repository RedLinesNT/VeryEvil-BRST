using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

namespace VEvil.Core {

    /// <summary>
    /// Contain utils methods related to the infrastructure for UnityEngine or even VeryEvil. 
    /// </summary>
    public static class CoreUtils {
        
        /// <summary>
        /// Give the awaiter of a <see cref="AsyncOperation"/>.
        /// </summary>
        /// <param name="_asyncOp">The <see cref="AsyncOperation"/>.</param>
        /// <returns>The awaiter of the <see cref="_asyncOp"/> given.</returns>
        public static TaskAwaiter GetAwaiter(this AsyncOperation _asyncOp) {
            TaskCompletionSource<AsyncOperation> _tcs = new TaskCompletionSource<AsyncOperation>();
            _asyncOp.completed += operation => { _tcs.SetResult(operation); };
            return ((Task)_tcs.Task).GetAwaiter();
        }
        
        /// <summary>
        /// Activates-Deactivates a array of <see cref="GameObject"/>s, depending on the value true-false
        /// </summary>
        public static void SetActive(this GameObject[] _objects, bool _value) {
            for (int i=0; i<_objects.Length; i++) {
                _objects[i].SetActive(_value);
            }
        }

        /// <summary>
        /// Activates-Deactivates a array of <see cref="Collider"/>s, depending on the value true-false
        /// </summary>
        public static void SetActive(this Collider[] _objects, bool _value) {
            for (int i = 0; i < _objects.Length; i++) {
                _objects[i].enabled = _value;
            }
        }

        /// <summary>
        /// Set the parent for an array of <see cref="GameObject"/>s
        /// </summary>
        /// <param name="_newParent">The new parent for the array of objects</param>
        public static void SetParent(this GameObject[] _objects, Transform _newParent) {
            for (int i=0; i<_objects.Length; i++) { 
                _objects[i].transform.parent = _newParent;
            }
        }
        
    }

}