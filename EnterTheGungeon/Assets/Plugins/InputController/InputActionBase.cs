using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



namespace RedLabsGames.Utls.Input
{
    [System.Serializable]
    public class InputActionBase 
    {

        public string name;

        public enum Type
        {
            Button,Float,Vector2
        }

        public enum ActionType
        {
            Asset,Local
        }

        public Type type;
        public ActionType actionType;

        public InputAction Action => actionType == ActionType.Local ? action : asset.action;

        [SerializeField]
        InputAction action;
        [SerializeField]
        InputActionReference asset;

        //Button
        [HideInInspector]
        public bool pressed, held, released;

        //Float 
        [HideInInspector]
        public float fValue;
    }
}
