using System;
using UnityEngine;
using UnityEngine.Events;
using Logger = VEvil.Core.Logger;

namespace VEvil.GameLogic.Interactable {

    /// <summary>
    /// <see cref="APointerClickableEntity"/> is an abstract class that give the possibility to an static/dynamic object to receive pointer events from the user.
    /// </summary>
    /// <remarks>
    /// A collider (its type doesn't matter) is required in order to have this component working, and this collider needs to be right next to a <see cref="APointerClickableEntity"/> component.
    /// </remarks>
    [RequireComponent(typeof(Collider))] public abstract class APointerClickableEntity : MonoBehaviour {

        #region Attributes

        [Header("Pointer Clickable Entity - Events")]
        [SerializeField, Tooltip("Invoked when the user's pointer is entering this entity.")] private UnityEvent onPointerEnterEntityEditor = null;
        [SerializeField, Tooltip("Invoked when the user's pointer is leaving this entity.")] private UnityEvent onPointerExitEntityEditor = null;
        [SerializeField, Tooltip("Invoked when the user's pointer is clicking this entity.")] private UnityEvent onPointerClickEntityEditor = null;

        #endregion

        #region Runtime Values

        /// <summary>
        /// Can this <see cref="APointerClickableEntity"/> receive and be clicked by the user's pointer ?
        /// </summary>
        private bool isInteractable = true;

        #endregion

        #region Events

        /// <summary>
        /// Invoked when the user's pointer is entering this entity.
        /// </summary>
        private Action onPointerEnterEntity = null;
        /// <summary>
        /// Invoked when the user's pointer is leaving this entity.
        /// </summary>
        private Action onPointerExitEntity = null;
        /// <summary>
        /// Invoked when the user's pointer is clicking this entity.
        /// </summary>
        private Action onPointerClickEntity = null;

        #endregion

        #region Properties

        /// <inheritdoc cref="isInteractable"/>
        public bool IsInteractable {
            get { return isInteractable; }
            set {
                isInteractable = value;
                if (!isInteractable) OnMouseExit();
            }
        }

        /// <inheritdoc cref="onPointerEnterEntity"/>
        public event Action OnPointerEnterEntity { add { onPointerEnterEntity += value; } remove { onPointerEnterEntity -= value; } }
        /// <inheritdoc cref="onPointerExitEntity"/>
        public event Action OnPointerExitEntity { add { onPointerExitEntity += value; } remove { onPointerExitEntity -= value; } }
        /// <inheritdoc cref="onPointerClickEntity"/>
        public event Action OnPointerClickEntity { add { onPointerClickEntity += value; } remove { onPointerClickEntity -= value; } }
        
        #endregion

        #region MonoBehaviour's Methods

        private void OnMouseEnter() {
            if (!isInteractable) return; //Don't execute anything if cannot interact with this entity
            
            //Invoke the correct events
            onPointerEnterEntity?.Invoke();
            onPointerEnterEntityEditor?.Invoke();
            
            OnPointerEnter();
        }

        private void OnMouseExit() {
            if (!isInteractable) return; //Don't execute anything if cannot interact with this entity
            
            //Invoke the correct events
            onPointerExitEntity?.Invoke();
            onPointerExitEntityEditor?.Invoke();
            
            OnPointerExit();
        }

        private void OnMouseDown() {
            if (!isInteractable) return; //Don't execute anything if cannot interact with this entity
            
            //Invoke the correct events
            onPointerClickEntity?.Invoke();
            onPointerClickEntityEditor?.Invoke();
            
            OnPointerClick();
        }

        #endregion
        
        #region APointerClickableEntity's Internal Virtual Methods

        /// <inheritdoc cref="onPointerEnterEntity"/>
        protected virtual void OnPointerEnter(){}
        /// <inheritdoc cref="onPointerExitEntity"/>
        protected virtual void OnPointerExit(){}
        /// <inheritdoc cref="onPointerClickEntity"/>
        protected virtual void OnPointerClick(){}

        #endregion

    }

}