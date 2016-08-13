using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UDPFlicListener : MonoBehaviour {

    public UDP_Receiver receiver;

    public void Start()
    {
        receiver.onPackageReceived += CheckForFlicUDPCommand;
    }
    public void OnDestroy()
    {
        receiver.onPackageReceived -= CheckForFlicUDPCommand;
    }

    private void CheckForFlicUDPCommand(UDP_Receiver from, string message, string adresse, int port)
    {

        if(message.IndexOf("FlicAction:")==0)
        {

            string [] tokens = message.Split(':');
            if(tokens.Length==4){
                string buttonId = tokens[1];
                string action = tokens[2];
                string actionMessage = tokens[3];
                Flics.Button button = Flics.GetButton(buttonId);
                Flics.FlicButtonAction buttonAction = Flics.GetFlicAction(action);
                if (buttonAction == Flics.FlicButtonAction.Unknown) return;

                if (buttonAction == Flics.FlicButtonAction.Down || buttonAction == Flics.FlicButtonAction.Up)
                    button.SetState(buttonAction == Flics.FlicButtonAction.Down);
                button.NotifyAction(buttonAction, actionMessage);
                
            }
            
 

        }
    }
	
	
}

public class Flics {

    public static  Dictionary<string, Button> buttonsRegistered = new Dictionary<string,Button>();


    public delegate void OnNewButtonDetected(Button button, string id);
    public static OnNewButtonDetected onNewButtonDetected;

    public delegate void OnButtonStateChange(Button button, string id, bool isDown);
    public static OnButtonStateChange onButtonStateChange;
    public delegate void OnButtonActionDetected(Button button, string id, FlicButtonAction action, string message);
    public static OnButtonActionDetected onButtonActionDetected;

    public enum FlicButtonAction { Up,Down, SingleClick,DoubleClick, LongClick, Unknown}
    public static FlicButtonAction GetFlicAction(string text)
    {
        if (text == "Up") return FlicButtonAction.Up;
        if (text == "Down") return FlicButtonAction.Down;
        if (text == "SingleClick") return FlicButtonAction.SingleClick;
        if (text == "DoubleClick") return FlicButtonAction.DoubleClick;
        if (text == "LongClick") return FlicButtonAction.LongClick;
        return FlicButtonAction.Unknown;
    }
    public class Button {

        public OnButtonStateChange onStateChange;
        public OnButtonActionDetected onActionDetected;

        public Button(string _id){
            this._buttonID=_id;
        }

        private bool _active;
        private string _buttonID;



        public string Id { get{return _buttonID;} }
        public bool IsDown
        {
            get { return _active; }
            set {
                bool oldValue = _active;
                bool newValue = value;
                _active = value;
                if(oldValue!=newValue)
                {
                    if(onButtonStateChange!=null)
                       onButtonStateChange(this, Id, newValue);
                    if(onStateChange!=null)
                      onStateChange(this, Id, newValue); 
                }
            }
        }



        internal void SetState(bool _isDown)
        {
            IsDown = _isDown;
        }

        internal void NotifyAction(FlicButtonAction buttonAction, string actionMessage)
        {
            if (onButtonActionDetected != null)
                onButtonActionDetected(this,this.Id,buttonAction, actionMessage);
            if (onActionDetected != null)
                onActionDetected(this, this.Id, buttonAction, actionMessage);
        }
    }


    public static Button GetButton(string buttonId, bool withAutoCreator=true)
    {
        if (buttonsRegistered.ContainsKey(buttonId))
            return buttonsRegistered[buttonId];
        if (!withAutoCreator) return null;
        return AddButtonToRegister(buttonId);
    }

    public static Button AddButtonToRegister(string buttonId)
    {
        
        if (buttonsRegistered.ContainsKey(buttonId))
            return buttonsRegistered[buttonId];

        Button button = new Button(buttonId);
        buttonsRegistered.Add(buttonId, button);

        if (onNewButtonDetected != null)
            onNewButtonDetected(button, buttonId);

        return button;
    }

    public static Button RemoveButtonOfRegister(string buttonId) {

        if (!buttonsRegistered.ContainsKey(buttonId))
            return null;
        Button buttonToRemove = buttonsRegistered[buttonId];
        buttonsRegistered.Remove(buttonId);
        return buttonToRemove;
    }
}


