using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets._PrIMD.VirtualController;

[System.Serializable]
/// <summary>
/// The class inherit from MonoBehaviour only to be easy to see in the Inpsector but not for the Start/Update/...
/// </summary>
        public class VirtualController 
        {
            public List<Joystick> _joysticks = new List<Joystick>();
            public List<Button> _buttons = new List<Button>();
            public List<Trigger> _triggers = new List<Trigger>();
            public List<TouchPad> _touchpads = new List<TouchPad>();
            public List<Orientation> _orientations = new List<Orientation>();
            public List<Positioning> _positions = new List<Positioning>();
            public Requests _requests = new Requests(20);

            private interface Indexable
            {
                void SetIndex(int indexNumber);
            }
            public class VirtualControllerElement : Indexable
                {
                    public int index = -1;
                    public void SetIndex(int index) { this.index = index; }
                    public int GetIndex() { return index; }

                    public VirtualControllerElement(int index=0) {
                        SetIndex(index);
                    }
                }
            [System.Serializable]
            public class Joystick :VirtualControllerElement 
            {
                public Vector3 _value;
                public delegate void OnValueHasChange(int index, Vector3 oldValue, Vector3 newValue);
                public OnValueHasChange onValueChanged;
                public delegate void OnValueAffected(int index, Vector3 oldValue, Vector3 newValue);
                public OnValueAffected onValueAffected;

                public Vector3 Value
                {
                    get { return _value; }
                    set
                    {
                        value.x = Mathf.Clamp(value.x, -1f, 1f);
                        value.y = Mathf.Clamp(value.y, -1f, 1f);
                        value.z = Mathf.Clamp(value.z, -1f, 1f);
                        if (onValueChanged != null && _value != value)
                        {
                            onValueChanged(GetIndex(), _value, value);
                        }
                        if (onValueAffected != null)
                        {
                            onValueAffected(GetIndex(), _value, value);
                        }
                        _value = value;
                    }
                }
            }

            [System.Serializable]
            public class Button:VirtualControllerElement
            {
                public bool _value;
                public delegate void OnValueHasChange(int index, bool oldValue, bool newValue);
                public OnValueHasChange onValueChanged;
                public delegate void OnValueAffected(int index, bool oldValue, bool newValue);
                public OnValueAffected onValueAffected;
                public bool Value
                {
                    get { return _value; }
                    set
                    {
                        if (onValueChanged != null && _value != value)
                        {
                            onValueChanged(GetIndex(), _value, value);
                        }
                        if (onValueAffected != null)
                        {
                            onValueAffected(GetIndex(), _value, value);
                        }
                        _value = value;
                    }
                }
            }
            [System.Serializable]
            public class Trigger:VirtualControllerElement
            {
                public float _value;
                public delegate void OnValueHasChange(int index, float oldValue, float newValue);
                public OnValueHasChange onValueChanged;
                public delegate void OnValueAffected(int index, float oldValue, float newValue);
                public OnValueAffected onValueAffected;
                public float Value
                {
                    get { return _value; }
                    set
                    {
                        value = Mathf.Clamp(value, -1f, 1f);

                        if (onValueChanged != null && _value != value)
                        {
                            onValueChanged(GetIndex(), _value, value);
                        }
                        if (onValueAffected != null)
                        {
                            onValueAffected(GetIndex(), _value, value);
                        }
                        _value = value;
                    }
                }
            }
            [System.Serializable]
            public class TouchPad:VirtualControllerElement
            {
                public Vector2 _value;
                public delegate void OnValueHasChange(int index, Vector2 oldValue, Vector2 newValue);
                public OnValueHasChange onValueChanged;
                public delegate void OnValueAffected(int index, Vector2 oldValue, Vector2 newValue);
                public OnValueAffected onValueAffected;

                public Vector2 Value
                {
                    get { return _value; }
                    set
                    {
                        value.x = Mathf.Clamp(value.x, 0f, 1f);
                        value.y = Mathf.Clamp(value.y, 0f, 1f); 
                        if (onValueChanged != null && _value != value)
                        {
                            onValueChanged(GetIndex(), _value, value);
                        }
                        if (onValueAffected != null)
                        {
                            onValueAffected(GetIndex(), _value, value);
                        }
                        _value = value;
                    }
                }
            }

            [System.Serializable]
            public class Orientation:VirtualControllerElement
            {
                //Euleur value
                public Quaternion _value;
                public delegate void OnValueHasChange(int index, Quaternion oldValue, Quaternion newValue);
                public OnValueHasChange onValueChanged;
                public delegate void OnValueAffected(int index, Quaternion oldValue, Quaternion newValue);
                public OnValueAffected onValueAffected;

                public Quaternion Value
                {
                    get { return _value; }
                    set
                    {
                        if (onValueChanged != null && _value != value)
                        {
                            onValueChanged(GetIndex(), _value, value);
                        }
                        if (onValueAffected != null)
                        {
                            onValueAffected(GetIndex(), _value, value);
                        }
                        _value = value;
                    }
                }
            }
            [System.Serializable]
            public class Positioning:VirtualControllerElement
            {
                //in meter
                public Vector3 _value;
                public delegate void OnValueHasChange(int index, Vector3 oldValue, Vector3 newValue);
                public OnValueHasChange onValueChanged;
                public delegate void OnValueAffected(int index, Vector3 oldValue, Vector3 newValue);
                public OnValueAffected onValueAffected;

                public Vector3 Value
                {
                    get { return _value; }
                    set
                    {
                        if (onValueChanged != null && _value != value)
                        {
                            onValueChanged(GetIndex(), _value, value);
                        }
                        if (onValueAffected != null)
                        {
                            onValueAffected(GetIndex(), _value, value);
                        }
                        _value = value;
                    }
                }
            }

            [System.Serializable]
            public class Requests :VirtualControllerElement
            {
                private int maxValueRecorded = 50;
                public Queue<string> _values = new Queue<string>();
          
                public delegate void OnValueHasChange(int index, string oldValue, string newValue);
                public OnValueHasChange onValueChanged;
                public delegate void OnValueAffected(int index, string oldValue, string newValue);
                public OnValueAffected onValueAffected;

                public  Requests(int numberMax, int index=0):base(index) {
                    maxValueRecorded = numberMax;
                }
                public string _lastRequest = "";
                public string _requestBefore = "";
                public string LastRequest {
                    get
                    {
                        if (_values.Count <= 0) 
                            return "";
                        return _values.Peek(); }
                    set {
                        string oldValue = _values.Count<=0?"": _values.Peek();
                        _values.Enqueue(value);
                        if (onValueChanged != null && oldValue != value)
                        {
                            onValueChanged(GetIndex(), oldValue, value);
                        }
                        if (onValueAffected != null )
                        {
                            onValueAffected(GetIndex(), oldValue, value);
                        }
                        if (_values.Count >= maxValueRecorded)
                        {_values.Dequeue();}

                        _lastRequest = value;
                        _requestBefore = oldValue;
                    }
                }
            }




            public Vector3 SetJoystickValue(int index, Vector3 value)
            {
                CheckAndResizeIfOutOfIndex<Joystick>(ref index, _joysticks);
                Vector3 oldValue = _joysticks[index].Value;
                _joysticks[index].Value = value;
                return oldValue;
            }

            public bool SetButtonValue(int index, bool value)
            {
                CheckAndResizeIfOutOfIndex<Button>(ref index, _buttons);
                bool oldValue = _buttons[index].Value;
                _buttons[index].Value = value;
                return oldValue;
            }
            public float SetTriggerValue(int index, float value)
            {
                CheckAndResizeIfOutOfIndex<Trigger>(ref index, _triggers);
                float oldValue = _triggers[index].Value;
                _triggers[index].Value = value;
                return oldValue;
            }


            public Vector2 SetTouchPadValue(int index, Vector2 value)
            {
                CheckAndResizeIfOutOfIndex<TouchPad>(ref index, _touchpads);
                Vector2 oldValue = _touchpads[index].Value;
                _touchpads[index].Value = value;
                return oldValue;
            }
            public Quaternion SetOrientationValue(int index, Quaternion value)
            {
                CheckAndResizeIfOutOfIndex<Orientation>(ref index, _orientations);
                Quaternion oldValue = _orientations[index].Value;
                _orientations[index].Value = value;
                return oldValue;
            }
            public Vector3 SetPositionValue(int index, Vector3 value)
            {
                CheckAndResizeIfOutOfIndex<Positioning>(ref index, _positions);
                Vector3 oldValue = _positions[index].Value;
                _positions[index].Value = value;
                return oldValue;
            }
            public string SetRequestValue(string value)
            {
                string oldValue = _requests.LastRequest;
                if (!string.IsNullOrEmpty(value)) {
                    _requests.LastRequest = value;
                }
                return oldValue;
            }


            public Vector3 GetJoystickValue(int index)
            {
                if (index < 0) return Vector3.zero;
                if (index >= _joysticks.Count) return Vector3.zero;
                return _joysticks[index].Value;
            }

            public bool GetButtonValue(int index)
            {
                if (index < 0) return false;
                if (index >= _buttons.Count) return false;
                return _buttons[index].Value;
            }
            public float GetTriggerValue(int index)
            {
                if (index < 0) return 0f;
                if (index >= _triggers.Count) return 0f;
                return _triggers[index].Value;
            }

            public Vector2 GetTouchPadValue(int index)
            {
                if (index < 0) return Vector2.zero;
                if (index >= _touchpads.Count) return Vector2.zero;
                return _touchpads[index].Value;
            }

            public Quaternion GetOrientationValue(int index)
            {
                if (index < 0) return Quaternion.identity;
                if (index >= _orientations.Count) return Quaternion.identity;
                return _orientations[index].Value;
            }

            public Vector3 GetPositionValue(int index)
            {
                if (index < 0) return Vector3.zero;
                if (index >= _positions.Count) return Vector3.zero;
                return _positions[index].Value;
            }
            public string GetLastRequestValue(int index)
            {
                if (_requests == null) return "";
                return _requests.LastRequest;
            }

            private T [] CheckAndResizeIfOutOfIndex<T>(ref int elementIndex, List<T> elementList) where T : VirtualControllerElement,new()
            {
                //index = Mathf.Clamp(index, 0, int.MaxValue);
                // I want the joystick 4 when there are only 3 --> (4+1) -3 = 2
                int missing = (elementIndex + 1) - elementList.Count;
                if (missing > 0)
                {
                    T[] values = new T[missing];
                    for (int i = missing; i > 0; i--)
                    {
                        values[i - 1] = new T() { index = (elementIndex+1-i)};
                        elementList.Add(values[i - 1]);
                    }
                    return values;
                }
                return null;
            }


            public Joystick GetJoystick(int index)
            {
                CheckAndResizeIfOutOfIndex<Joystick>(ref index, _joysticks);
                return _joysticks[index];
            }
            public Button GetButton(int index)
            {
                CheckAndResizeIfOutOfIndex<Button>(ref index, _buttons);
                return _buttons[index];
            }
            public Trigger GetTrigger(int index)
            {
                CheckAndResizeIfOutOfIndex<Trigger>(ref index, _triggers);
                return _triggers[index];
            }
            public TouchPad GetTouchPad(int index)
            {
                CheckAndResizeIfOutOfIndex<TouchPad>(ref index, _touchpads);
                return _touchpads[index];
            }
            public Orientation GetOrientation(int index)
            {
                CheckAndResizeIfOutOfIndex<Orientation>(ref index, _orientations);
                return _orientations[index];
            }
            public Positioning GetPosition(int index)
            {
                CheckAndResizeIfOutOfIndex<Positioning>(ref index, _positions);
                return _positions[index];
            }
            public Requests GetRequests()
            {
                return _requests;
            }

            
            public override string ToString()
            {
                string description = "";
                for (int i = 0; i < _joysticks.Count; i++)
                    if (_joysticks[i].Value != Vector3.zero)
                        description += string.Format("  J [{0}] : {1}  ", i, _joysticks[i].Value);
                for (int i = 0; i < _buttons.Count; i++)
                    if (_buttons[i].Value != false)
                        description += string.Format("  B {0}  ", i);
                for (int i = 0; i < _triggers.Count; i++)
                    if (_triggers[i].Value != 0f)
                        description += string.Format("  T [{0}] : {1}  ", i, _triggers[i].Value);
             
                for (int i = 0; i < _touchpads.Count; i++)
                    if (_touchpads[i].Value != Vector2.zero)
                        description += string.Format("  Pad [{0}] : {1}  ", i, _touchpads[i].Value);
                for (int i = 0; i < _orientations.Count; i++)
                    if (_orientations[i].Value != Quaternion.identity)
                        description += string.Format("  Quat [{0}] : {1}  ", i, _orientations[i].Value);
                for (int i = 0; i < _positions.Count; i++)
                    if (_positions[i].Value != Vector3.zero)
                        description += string.Format("  Pos [{0}] : {1}  ", i, _positions[i].Value);
                if (!string.IsNullOrEmpty(_requests.LastRequest)) {
                    description += string.Format("  Request : {0}  ",_requests.LastRequest);
                }


                if (string.IsNullOrEmpty(description))
                    description = "No Information yet";
                return description;


            }


            public static Dictionary<string, VirtualController> registeredControllers = new Dictionary<string, VirtualController>();

            public static VirtualController Get(string controllerName)
            {
                VirtualController result=null;
                if (registeredControllers.ContainsKey(controllerName))
                {
                    result = registeredControllers[controllerName];
                    if (result == null)
                    { 
                        result = new VirtualController();
                          registeredControllers[controllerName]= result;
                    }
                }
                else {
                    result = new VirtualController();
                    registeredControllers.Add(controllerName, result);
                }

                
                return result;
                   
            }
            public static  string[] GetAllControllerName() {
                string[] controllersName = new string[registeredControllers.Keys.Count];
                registeredControllers.Keys.CopyTo(controllersName, 0);
                return controllersName;
            }
            public static VirtualController[] GetAllControllers()
            {
                VirtualController[] controllers = new VirtualController[registeredControllers.Values.Count];
                registeredControllers.Values.CopyTo(controllers, 0);
                return controllers;
            }
       
          //  public static VirtualController Get(string controllerName, string indexName)
          
        }

    