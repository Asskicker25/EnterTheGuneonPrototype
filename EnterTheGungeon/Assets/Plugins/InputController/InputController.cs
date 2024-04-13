using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RedLabsGames.Utls.Input
{
    public class InputController : MonoBehaviour
    {
        public static InputController active;

        [SerializeField]
        public List<InputActionBase> actions = new List<InputActionBase>();

        Dictionary<string, InputActionBase> cached = new Dictionary<string, InputActionBase>();

        private void OnEnable()
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Action.Enable();
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Action.Disable();
            }
        }

        private void Awake()
        {
            active = this;

            for (int i = 0; i < actions.Count; i++)
            {
                int index = i;

                cached.Add(actions[i].name, actions[i]);

                switch (actions[i].type)
                {
                    case InputActionBase.Type.Button:
                        actions[i].Action.performed += (ctx) => 
                        {
                            actions[index].held = true;
                            actions[index].pressed = true;
                        };

                        actions[i].Action.canceled += (ctx) =>
                        {
                            actions[index].held = false;
                            actions[index].released = true;
                        };
                        break;
                    case InputActionBase.Type.Float:

                        actions[i].Action.performed += (ctx) =>
                        {
                            actions[index].fValue = ctx.ReadValue<float>();
                        };

                        actions[i].Action.canceled += (ctx) =>
                        {
                            actions[index].fValue = 0;
                        };

                        break;
                    case InputActionBase.Type.Vector2:
                        break;
                    default:
                        break;
                }

            }


        }

        private void Start()
        {
            active = this;
        }

        private void LateUpdate()
        {
            for (int i = 0; i < actions.Count; i++)
            {
                switch (actions[i].type)
                {
                    case InputActionBase.Type.Button:
                        actions[i].pressed = false;
                        actions[i].released = false;
                        break;
                    case InputActionBase.Type.Float:
                        break;
                    case InputActionBase.Type.Vector2:
                        break;
                    default:
                        break;
                }
            }
        }



        // API

        public bool GetButtonPressed(string name)
        {
            if (cached.ContainsKey(name))
            {
               return cached[name].pressed;
            }
            else
            {
                Debug.LogError($"{name} is not valid");
                return false;
            }
        }

        public bool GetButtonReleased(string name)
        {
            if (cached.ContainsKey(name))
            {
                return cached[name].released;
            }
            else
            {
                Debug.LogError($"{name} is not valid");
                return false;
            }
        }

        public bool GetButtonHeld(string name)
        {
            if (cached.ContainsKey(name))
            {
                return cached[name].held;
            }
            else
            {
                Debug.LogError($"{name} is not valid");
                return false;
            }
        }


        public float GetFloat(string name)
        {
            if (cached.ContainsKey(name))
            {
                return cached[name].fValue;
            }
            else
            {
                Debug.LogError($"{name} is not valid");
                return 0;
            }
        }
    }

    public static class ActiveInputController
    {
        public static bool GetButtonDown(string name)
        {
            return InputController.active.GetButtonPressed(name);
        }

        public static bool GetButtonUp(string name)
        {
            return InputController.active.GetButtonReleased(name);
        }

        public static bool GetButton(string name)
        {
            return InputController.active.GetButtonHeld(name);
        }


        public static float GetAxis(string name)
        {
            return InputController.active.GetFloat(name);
        }

        public static float GetAxisRaw(string name)
        {
            float v = InputController.active.GetFloat(name);
            return v==0?0:v>0?1:-1;
        }
    }


#if UNITY_EDITOR

    [CustomEditor(typeof(InputController))]
    public class InputControllerEditor : Editor
    {

        InputController input;

        private void OnEnable()
        {
            input = (InputController)target;
        }

        public override void OnInspectorGUI()
        {
           // base.OnInspectorGUI();

            GUILayout.Space(10);

            for (int i = 0; i < input.actions.Count; i++)
            {
                GUILayout.BeginHorizontal();

                input.actions[i].name = GUILayout.TextField(input.actions[i].name);
                input.actions[i].type = (InputActionBase.Type)EditorGUILayout.EnumPopup(input.actions[i].type, GUILayout.Width(100));
                input.actions[i].actionType = (InputActionBase.ActionType)EditorGUILayout.EnumPopup(input.actions[i].actionType, GUILayout.Width(100));

                if (GUILayout.Button("X",GUILayout.Width(EditorGUIUtility.singleLineHeight)))
                {
                    input.actions.RemoveAt(i);
                    return;
                }

                GUILayout.EndHorizontal();

                GUILayout.Space(8);

                if (input.actions[i].actionType == InputActionBase.ActionType.Asset)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("actions").GetArrayElementAtIndex(i).FindPropertyRelative("asset"));
                    serializedObject.FindProperty("actions").GetArrayElementAtIndex(i).FindPropertyRelative("asset").serializedObject.ApplyModifiedProperties();
                }
                else
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("actions").GetArrayElementAtIndex(i).FindPropertyRelative("action"));
                    serializedObject.FindProperty("actions").GetArrayElementAtIndex(i).FindPropertyRelative("action").serializedObject.ApplyModifiedProperties();
                }

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            }

            if (GUILayout.Button("Add Input"))
            {
                input.actions.Add(new InputActionBase());
            }

            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }

#endif

}
