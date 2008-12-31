using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace KingsburgXNA
{
    /// <summary>
    /// Helper for reading input from keyboard and gamepad. This class tracks both
    /// the current and previous state of both input devices, and implements query
    /// properties for high level input actions such as "move up through the menu"
    /// or "pause the game".
    /// </summary>
    public class InputState
    {
        #region Fields

        public const int MaxInputs = 4;

        public readonly KeyboardState[] CurrentKeyboardStates;
        public readonly GamePadState[] CurrentGamePadStates;

        public readonly KeyboardState[] LastKeyboardStates;
        public readonly GamePadState[] LastGamePadStates;

        private ActionMap[] ActionMaps = new ActionMap[(int)Action.TotalActions];

        #endregion

        #region Initialization


        /// <summary>
        /// Constructs a new input state.
        /// </summary>
        public InputState()
        {
            CurrentKeyboardStates = new KeyboardState[MaxInputs];
            CurrentGamePadStates = new GamePadState[MaxInputs];

            LastKeyboardStates = new KeyboardState[MaxInputs];
            LastGamePadStates = new GamePadState[MaxInputs];

            ActionMaps = DefaultActionMap;
        }


        #endregion

        #region Properties
        #endregion

        #region Methods


        /// <summary>
        /// Reads the latest state of the keyboard and gamepad.
        /// </summary>
        public void Update()
        {
            for( int i = 0; i < MaxInputs; i++ )
            {
                LastKeyboardStates[i] = CurrentKeyboardStates[i];
                LastGamePadStates[i] = CurrentGamePadStates[i];

                CurrentKeyboardStates[i] = Keyboard.GetState( (PlayerIndex)i );
                CurrentGamePadStates[i] = GamePad.GetState( (PlayerIndex)i );
            }
        }


        /// <summary>
        /// Helper for checking if a key was newly pressed during this update,
        /// by any player.
        /// </summary>
        public bool IsNewKeyPress( Keys key )
        {
            for( int i = 0; i < MaxInputs; i++ )
            {
                if( IsNewKeyPress( key, (PlayerIndex)i ) )
                    return true;
            }

            return false;
        }


        /// <summary>
        /// Helper for checking if a key was newly pressed during this update,
        /// by the specified player.
        /// </summary>
        public bool IsNewKeyPress( Keys key, PlayerIndex playerIndex )
        {
            return ( CurrentKeyboardStates[(int)playerIndex].IsKeyDown( key ) &&
                    LastKeyboardStates[(int)playerIndex].IsKeyUp( key ) );
        }

        /// <summary>
        /// Helper for checking if a button was newly pressed during this update,
        /// by any player.
        /// </summary>
        public bool IsNewButtonPress( Buttons button )
        {
            for( int i = 0; i < MaxInputs; i++ )
            {
                if( IsNewButtonPress( button, (PlayerIndex)i ) )
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Helper for checking if a button was newly pressed during this update,
        /// by the specified player.
        /// </summary>
        public bool IsNewButtonPress( Buttons button, PlayerIndex playerIndex )
        {
            return ( CurrentGamePadStates[(int)playerIndex].IsButtonDown( button ) &&
                    LastGamePadStates[(int)playerIndex].IsButtonUp( button ) );
        }

        #endregion

        public bool CheckAction( Action action )
        {
            for( int i = 0; i < MaxInputs; i++ )
            {
                if( CheckActionMap( this.ActionMaps[(int)action], (PlayerIndex)i ) )
                    return true;
            }
            return false;
        }

        public bool CheckAction( Action action, PlayerIndex playerIndex )
        {
            return CheckActionMap( this.ActionMaps[(int)action], playerIndex );
        }

        private bool CheckActionMap( ActionMap actionMap, PlayerIndex playerIndex )
        {
            for( int i = 0; i < actionMap.keyboardKeys.Count; i++ )
            {
                if( this.IsNewKeyPress( actionMap.keyboardKeys[i] ) )
                {
                    return true;
                }
            }
            if( this.CurrentGamePadStates[(int)playerIndex].IsConnected )
            {
                for( int i = 0; i < actionMap.gamePadButtons.Count; i++ )
                {
                    if( this.IsNewButtonPress( actionMap.gamePadButtons[i] ) )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private ActionMap[] DefaultActionMap
        {
            get
            {
                ActionMap[] toReturn = new ActionMap[(int)Action.TotalActions];

                toReturn[(int)Action.Down] = new ActionMap();
                toReturn[(int)Action.Down].AddGamePadButtons( Buttons.DPadDown, Buttons.LeftThumbstickDown );
                toReturn[(int)Action.Down].AddKeyboardKeys( Keys.Down );

                toReturn[(int)Action.Left] = new ActionMap();
                toReturn[(int)Action.Left].AddGamePadButtons( Buttons.DPadLeft, Buttons.LeftThumbstickLeft );
                toReturn[(int)Action.Left].AddKeyboardKeys( Keys.Left );

                toReturn[(int)Action.Right] = new ActionMap();
                toReturn[(int)Action.Right].AddGamePadButtons( Buttons.DPadRight, Buttons.LeftThumbstickRight );
                toReturn[(int)Action.Right].AddKeyboardKeys( Keys.Right );

                toReturn[(int)Action.Up] = new ActionMap();
                toReturn[(int)Action.Up].AddGamePadButtons( Buttons.DPadUp, Buttons.LeftThumbstickUp );
                toReturn[(int)Action.Up].AddKeyboardKeys( Keys.Up );

                toReturn[(int)Action.OK] = new ActionMap();
                toReturn[(int)Action.OK].AddGamePadButtons( Buttons.A );
                toReturn[(int)Action.OK].AddKeyboardKeys( Keys.Enter, Keys.Space );

                toReturn[(int)Action.Back] = new ActionMap();
                toReturn[(int)Action.Back].AddGamePadButtons( Buttons.B, Buttons.Back );
                toReturn[(int)Action.Back].AddKeyboardKeys( Keys.Back, Keys.Escape );

                toReturn[(int)Action.StartButton] = new ActionMap();
                toReturn[(int)Action.StartButton].AddGamePadButtons( Buttons.Start );
                toReturn[(int)Action.StartButton].AddKeyboardKeys( Keys.Enter, Keys.Space );

                toReturn[(int)Action.DebugExit] = new ActionMap();
                toReturn[(int)Action.DebugExit].AddGamePadButtons( Buttons.RightStick );
                toReturn[(int)Action.DebugExit].AddKeyboardKeys( Keys.End );
                
                return toReturn;
            }
        }
    }

    public enum Action
    {
        Left,
        Right,
        Up,
        Down,
        OK,
        Back,
        StartButton,
        DebugExit,
        TotalActions,
    }

    ///// <summary>
    ///// GamePad controls expressed as one type, unified with button semantics.
    ///// </summary>
    //public enum GamePadButtons
    //{
    //    Start,
    //    Back,
    //    A,
    //    B,
    //    X,
    //    Y,
    //    Up,
    //    Down,
    //    Left,
    //    Right,
    //    LeftShoulder,
    //    RightShoulder,
    //    LeftTrigger,
    //    RightTrigger,
    //}

    /// <summary>
    /// A combination of gamepad and keyboard keys mapped to a particular action.
    /// </summary>
    public class ActionMap
    {
        /// <summary>
        /// List of GamePad controls to be mapped to a given action.
        /// </summary>
        public List<Buttons> gamePadButtons = new List<Buttons>();


        /// <summary>
        /// List of Keyboard controls to be mapped to a given action.
        /// </summary>
        public List<Keys> keyboardKeys = new List<Keys>();

        public void AddGamePadButtons( params Buttons[] buttons )
        {
            this.gamePadButtons.AddRange( buttons );
        }

        public void AddKeyboardKeys( params Keys[] keys )
        {
            this.keyboardKeys.AddRange( keys );
        }

        //public ActionMap( List<GamePadButtons> gamePadButtons, List<Keys> keys )
        //{
        //    this.gamePadButtons = gamePadButtons;
        //    this.keyboardKeys = keys;
        //}

        //public ActionMap( params GamePadButtons[] gamePadButtons)
        //{
        //    this.AddGamePadButtons( gamePadButtons );
        //}

        //public ActionMap( params Keys[] keys )
        //{
        //    this.keyboardKeys.AddRange( keys );
        //}
    }
}
