//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Inputs/GameplayInputProvider.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace VEvil.Inputs.Generated
{
    public partial class @GameplayInputProvider: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @GameplayInputProvider()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameplayInputProvider"",
    ""maps"": [
        {
            ""name"": ""GameCamera"",
            ""id"": ""5e3409c0-3da3-4131-86c4-cf1e4227dafa"",
            ""actions"": [
                {
                    ""name"": ""MoveUp"",
                    ""type"": ""Value"",
                    ""id"": ""40e168fc-df04-4798-9143-569161031fab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MoveDown"",
                    ""type"": ""Button"",
                    ""id"": ""3f38cdf6-c3aa-4d49-be03-29c5c513acdd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""Button"",
                    ""id"": ""9034fd06-512d-445f-9a13-95a3cc0bada6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveLeft"",
                    ""type"": ""Button"",
                    ""id"": ""9d4b8674-e239-47a6-ae3b-152a52508876"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DetachTarget"",
                    ""type"": ""Button"",
                    ""id"": ""f32edfc6-799c-437a-94c2-03f25f3f67dd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5232cef7-a42e-4479-b387-1106748ab0ef"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71414851-4853-456a-9ade-d7448f725916"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3b9043f-8e07-43ec-b425-ffde51d7f69e"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8d6dae9-2bf8-4712-bbb6-1d61d35603fb"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""10744a5c-3806-4f5f-93f9-5daea5a19dfe"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DetachTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // GameCamera
            m_GameCamera = asset.FindActionMap("GameCamera", throwIfNotFound: true);
            m_GameCamera_MoveUp = m_GameCamera.FindAction("MoveUp", throwIfNotFound: true);
            m_GameCamera_MoveDown = m_GameCamera.FindAction("MoveDown", throwIfNotFound: true);
            m_GameCamera_MoveRight = m_GameCamera.FindAction("MoveRight", throwIfNotFound: true);
            m_GameCamera_MoveLeft = m_GameCamera.FindAction("MoveLeft", throwIfNotFound: true);
            m_GameCamera_DetachTarget = m_GameCamera.FindAction("DetachTarget", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // GameCamera
        private readonly InputActionMap m_GameCamera;
        private List<IGameCameraActions> m_GameCameraActionsCallbackInterfaces = new List<IGameCameraActions>();
        private readonly InputAction m_GameCamera_MoveUp;
        private readonly InputAction m_GameCamera_MoveDown;
        private readonly InputAction m_GameCamera_MoveRight;
        private readonly InputAction m_GameCamera_MoveLeft;
        private readonly InputAction m_GameCamera_DetachTarget;
        public struct GameCameraActions
        {
            private @GameplayInputProvider m_Wrapper;
            public GameCameraActions(@GameplayInputProvider wrapper) { m_Wrapper = wrapper; }
            public InputAction @MoveUp => m_Wrapper.m_GameCamera_MoveUp;
            public InputAction @MoveDown => m_Wrapper.m_GameCamera_MoveDown;
            public InputAction @MoveRight => m_Wrapper.m_GameCamera_MoveRight;
            public InputAction @MoveLeft => m_Wrapper.m_GameCamera_MoveLeft;
            public InputAction @DetachTarget => m_Wrapper.m_GameCamera_DetachTarget;
            public InputActionMap Get() { return m_Wrapper.m_GameCamera; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameCameraActions set) { return set.Get(); }
            public void AddCallbacks(IGameCameraActions instance)
            {
                if (instance == null || m_Wrapper.m_GameCameraActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_GameCameraActionsCallbackInterfaces.Add(instance);
                @MoveUp.started += instance.OnMoveUp;
                @MoveUp.performed += instance.OnMoveUp;
                @MoveUp.canceled += instance.OnMoveUp;
                @MoveDown.started += instance.OnMoveDown;
                @MoveDown.performed += instance.OnMoveDown;
                @MoveDown.canceled += instance.OnMoveDown;
                @MoveRight.started += instance.OnMoveRight;
                @MoveRight.performed += instance.OnMoveRight;
                @MoveRight.canceled += instance.OnMoveRight;
                @MoveLeft.started += instance.OnMoveLeft;
                @MoveLeft.performed += instance.OnMoveLeft;
                @MoveLeft.canceled += instance.OnMoveLeft;
                @DetachTarget.started += instance.OnDetachTarget;
                @DetachTarget.performed += instance.OnDetachTarget;
                @DetachTarget.canceled += instance.OnDetachTarget;
            }

            private void UnregisterCallbacks(IGameCameraActions instance)
            {
                @MoveUp.started -= instance.OnMoveUp;
                @MoveUp.performed -= instance.OnMoveUp;
                @MoveUp.canceled -= instance.OnMoveUp;
                @MoveDown.started -= instance.OnMoveDown;
                @MoveDown.performed -= instance.OnMoveDown;
                @MoveDown.canceled -= instance.OnMoveDown;
                @MoveRight.started -= instance.OnMoveRight;
                @MoveRight.performed -= instance.OnMoveRight;
                @MoveRight.canceled -= instance.OnMoveRight;
                @MoveLeft.started -= instance.OnMoveLeft;
                @MoveLeft.performed -= instance.OnMoveLeft;
                @MoveLeft.canceled -= instance.OnMoveLeft;
                @DetachTarget.started -= instance.OnDetachTarget;
                @DetachTarget.performed -= instance.OnDetachTarget;
                @DetachTarget.canceled -= instance.OnDetachTarget;
            }

            public void RemoveCallbacks(IGameCameraActions instance)
            {
                if (m_Wrapper.m_GameCameraActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IGameCameraActions instance)
            {
                foreach (var item in m_Wrapper.m_GameCameraActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_GameCameraActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public GameCameraActions @GameCamera => new GameCameraActions(this);
        public interface IGameCameraActions
        {
            void OnMoveUp(InputAction.CallbackContext context);
            void OnMoveDown(InputAction.CallbackContext context);
            void OnMoveRight(InputAction.CallbackContext context);
            void OnMoveLeft(InputAction.CallbackContext context);
            void OnDetachTarget(InputAction.CallbackContext context);
        }
    }
}
