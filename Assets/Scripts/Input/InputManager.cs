﻿using System;
using System.Collections.Generic;
using SOMStudio.BASE.InputManagement.Interfaces;
using UnityEngine;

namespace SOMStudio.BASE.InputManagement
{
    public class InputManager : IInputManager
    {
        protected InputBindings inputBindings;
        protected IMouseInputHandler mouseInputHandler;
        protected Dictionary<string, Action> actionMap = new Dictionary<string, Action>();
        
        public InputManager(InputBindings inputBindings, IMouseInputHandler mouseInputHandler)
        {
            this.inputBindings = inputBindings;
            this.mouseInputHandler = mouseInputHandler;
        }

        public void AddActionToBinding(string binding, Action action)
        {
            actionMap.Add(binding, action);
        }

        public float GetAxis(string axisName)
        {
            return Input.GetAxis(axisName);
        }

        public bool GetButton(string buttonName)
        {
            return Input.GetButton(buttonName);
        }

        public Vector2 GetMouseVector(Vector2 relativePosition)
        {
            return mouseInputHandler.GetInput(relativePosition);
        }

        public void CheckForInput()
        {
            foreach (var kvp in inputBindings.KeyBindings)
            {
                if (Input.GetKeyUp(kvp.Value))
                {
                    Action action;
                    actionMap.TryGetValue(kvp.Key, out action);
                    if (action != null)
                    {
                        action.Invoke();
                    }
                }
            }
        }
    }
}