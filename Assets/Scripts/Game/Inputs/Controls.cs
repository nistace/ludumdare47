// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace LD47
{
    public class @Controls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Controls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""60a0beff-a802-47e9-9085-73feb4d34ab6"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""cd56bc1e-cdc1-4442-a493-b4856c149b6b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""b833781d-ec1f-4d10-8dc0-400fc48d77f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""PickUp"",
                    ""type"": ""Button"",
                    ""id"": ""5711c03b-606f-4ffc-ad49-ae8eb8eaa69b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""e29f04ae-687a-449f-99cb-0f5c784b4c0d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""f1754f22-9879-4870-a8a7-bf2b284ef28f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""07c3d9de-dce3-465b-9f23-1bdb04018755"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""dffc0768-dc61-4937-a310-5fb3d7282e71"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""80fd49da-b1a0-4a21-8269-7ffa23dddac4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""320b3942-12e1-460b-88db-0e70685d56c3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""128b8e14-6678-46ff-89a3-319ad646866b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ffc78a8-45c3-4b24-830e-5555574e574b"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""75beccaa-9126-47d6-8fa9-e3b09c08ea9f"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Game"",
            ""id"": ""8df7e12a-c097-4a87-87e4-d62fd137f7f8"",
            ""actions"": [
                {
                    ""name"": ""EndLoop"",
                    ""type"": ""Button"",
                    ""id"": ""8882c00c-4773-4128-8be7-58015e1cf633"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""StartPlaying"",
                    ""type"": ""Button"",
                    ""id"": ""addf591d-ab27-4fc7-93b6-f9451efb9673"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""RestartLevel"",
                    ""type"": ""Button"",
                    ""id"": ""a68d20f0-16a3-4dfe-b603-26c54c11220e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4d265f70-1d8b-4fc5-a47d-cfe3ed28f31a"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EndLoop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77f3ce29-19b8-4efd-b1bb-fd5f3eec1d59"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StartPlaying"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""95e74ccf-f426-4844-8565-2dbea620a554"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RestartLevel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
            m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
            m_Player_PickUp = m_Player.FindAction("PickUp", throwIfNotFound: true);
            m_Player_Throw = m_Player.FindAction("Throw", throwIfNotFound: true);
            // Game
            m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
            m_Game_EndLoop = m_Game.FindAction("EndLoop", throwIfNotFound: true);
            m_Game_StartPlaying = m_Game.FindAction("StartPlaying", throwIfNotFound: true);
            m_Game_RestartLevel = m_Game.FindAction("RestartLevel", throwIfNotFound: true);
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

        // Player
        private readonly InputActionMap m_Player;
        private IPlayerActions m_PlayerActionsCallbackInterface;
        private readonly InputAction m_Player_Movement;
        private readonly InputAction m_Player_Jump;
        private readonly InputAction m_Player_PickUp;
        private readonly InputAction m_Player_Throw;
        public struct PlayerActions
        {
            private @Controls m_Wrapper;
            public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_Player_Movement;
            public InputAction @Jump => m_Wrapper.m_Player_Jump;
            public InputAction @PickUp => m_Wrapper.m_Player_PickUp;
            public InputAction @Throw => m_Wrapper.m_Player_Throw;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                    @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                    @PickUp.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickUp;
                    @PickUp.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickUp;
                    @PickUp.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickUp;
                    @Throw.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThrow;
                    @Throw.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThrow;
                    @Throw.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThrow;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @PickUp.started += instance.OnPickUp;
                    @PickUp.performed += instance.OnPickUp;
                    @PickUp.canceled += instance.OnPickUp;
                    @Throw.started += instance.OnThrow;
                    @Throw.performed += instance.OnThrow;
                    @Throw.canceled += instance.OnThrow;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);

        // Game
        private readonly InputActionMap m_Game;
        private IGameActions m_GameActionsCallbackInterface;
        private readonly InputAction m_Game_EndLoop;
        private readonly InputAction m_Game_StartPlaying;
        private readonly InputAction m_Game_RestartLevel;
        public struct GameActions
        {
            private @Controls m_Wrapper;
            public GameActions(@Controls wrapper) { m_Wrapper = wrapper; }
            public InputAction @EndLoop => m_Wrapper.m_Game_EndLoop;
            public InputAction @StartPlaying => m_Wrapper.m_Game_StartPlaying;
            public InputAction @RestartLevel => m_Wrapper.m_Game_RestartLevel;
            public InputActionMap Get() { return m_Wrapper.m_Game; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
            public void SetCallbacks(IGameActions instance)
            {
                if (m_Wrapper.m_GameActionsCallbackInterface != null)
                {
                    @EndLoop.started -= m_Wrapper.m_GameActionsCallbackInterface.OnEndLoop;
                    @EndLoop.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnEndLoop;
                    @EndLoop.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnEndLoop;
                    @StartPlaying.started -= m_Wrapper.m_GameActionsCallbackInterface.OnStartPlaying;
                    @StartPlaying.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnStartPlaying;
                    @StartPlaying.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnStartPlaying;
                    @RestartLevel.started -= m_Wrapper.m_GameActionsCallbackInterface.OnRestartLevel;
                    @RestartLevel.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnRestartLevel;
                    @RestartLevel.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnRestartLevel;
                }
                m_Wrapper.m_GameActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @EndLoop.started += instance.OnEndLoop;
                    @EndLoop.performed += instance.OnEndLoop;
                    @EndLoop.canceled += instance.OnEndLoop;
                    @StartPlaying.started += instance.OnStartPlaying;
                    @StartPlaying.performed += instance.OnStartPlaying;
                    @StartPlaying.canceled += instance.OnStartPlaying;
                    @RestartLevel.started += instance.OnRestartLevel;
                    @RestartLevel.performed += instance.OnRestartLevel;
                    @RestartLevel.canceled += instance.OnRestartLevel;
                }
            }
        }
        public GameActions @Game => new GameActions(this);
        public interface IPlayerActions
        {
            void OnMovement(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnPickUp(InputAction.CallbackContext context);
            void OnThrow(InputAction.CallbackContext context);
        }
        public interface IGameActions
        {
            void OnEndLoop(InputAction.CallbackContext context);
            void OnStartPlaying(InputAction.CallbackContext context);
            void OnRestartLevel(InputAction.CallbackContext context);
        }
    }
}
