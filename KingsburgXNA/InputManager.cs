#region File Description
//-----------------------------------------------------------------------------
// InputManager.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
#endregion

namespace KingsburgXNA
{
    /// <summary>
    /// This class handles all keyboard and gamepad actions in the game.
    /// </summary>
    //public static class InputManager
    //{
    //    #region Action Enumeration


    //    /// <summary>
    //    /// The actions that are possible within the game.
    //    /// </summary>
    //    public enum Action
    //    {
    //        MainMenu,
    //        Ok,
    //        Back,
    //        ExitGame,
    //        TakeView,
    //        DropUnEquip,
    //        CursorUp,
    //        CursorDown,
    //        CursorLeft,
    //        CursorRight,
    //        PageLeft,
    //        PageRight,
    //        AnyButton,

    //        // This is added last so you can create an array of size=TotalActionCount 
    //        // and always have enough slots for every action
    //        TotalActionCount,
    //    }


    //    /// <summary>
    //    /// Readable names of each action.
    //    /// </summary>
    //    private static readonly string[] actionNames = 
    //        {
    //            "Main Menu",
    //            "OK",
    //            "Back",
    //            "Exit Game",
    //            "Move Cursor - Up",
    //            "Move Cursor - Down",
    //            "Move Cursor - Left",
    //            "Move Cursor - Right",
    //            "Page Screen Left",
    //            "Page Screen Right",
    //            "Press Any Button",
    //        };

    //    /// <summary>
    //    /// Returns the readable name of the given action.
    //    /// </summary>
    //    public static string GetActionName( Action action )
    //    {
    //        int index = (int)action;

    //        if( ( index < 0 ) || ( index > actionNames.Length ) )
    //        {
    //            throw new ArgumentException( "action" );
    //        }

    //        return actionNames[index];
    //    }


    //    #endregion


    //    #region Support Types


    //    /// <summary>
    //    /// GamePad controls expressed as one type, unified with button semantics.
    //    /// </summary>
    //    public enum GamePadButtons
    //    {
    //        Start,
    //        Back,
    //        A,
    //        B,
    //        X,
    //        Y,
    //        Up,
    //        Down,
    //        Left,
    //        Right,
    //        LeftShoulder,
    //        RightShoulder,
    //        LeftTrigger,
    //        RightTrigger,
    //    }


    //    /// <summary>
    //    /// A combination of gamepad and keyboard keys mapped to a particular action.
    //    /// </summary>
    //    public class ActionMap
    //    {
    //        /// <summary>
    //        /// List of GamePad controls to be mapped to a given action.
    //        /// </summary>
    //        public List<GamePadButtons> gamePadButtons = new List<GamePadButtons>();


    //        /// <summary>
    //        /// List of Keyboard controls to be mapped to a given action.
    //        /// </summary>
    //        public List<Keys> keyboardKeys = new List<Keys>();
    //    }


    //    #endregion


    //    #region Constants


    //    /// <summary>
    //    /// The value of an analog control that reads as a "pressed button".
    //    /// </summary>
    //    const float analogLimit = 0.5f;


    //    #endregion


    //    #region Keyboard Data


    //    /// <summary>
    //    /// The state of the keyboard as of the last update.
    //    /// </summary>
    //    private static KeyboardState currentKeyboardState;

    //    /// <summary>
    //    /// The state of the keyboard as of the last update.
    //    /// </summary>
    //    public static KeyboardState CurrentKeyboardState
    //    {
    //        get
    //        {
    //            return currentKeyboardState;
    //        }
    //    }


    //    /// <summary>
    //    /// The state of the keyboard as of the previous update.
    //    /// </summary>
    //    private static KeyboardState previousKeyboardState;


    //    /// <summary>
    //    /// Check if a key is pressed.
    //    /// </summary>
    //    public static bool IsKeyPressed( Keys key )
    //    {
    //        return currentKeyboardState.IsKeyDown( key );
    //    }


    //    /// <summary>
    //    /// Check if a key was just pressed in the most recent update.
    //    /// </summary>
    //    public static bool IsKeyTriggered( Keys key )
    //    {
    //        return ( currentKeyboardState.IsKeyDown( key ) ) &&
    //            ( !previousKeyboardState.IsKeyDown( key ) );
    //    }


    //    #endregion


    //    #region GamePad Data


    //    /// <summary>
    //    /// The state of the gamepad as of the last update.
    //    /// </summary>
    //    private static GamePadState currentGamePadState;

    //    /// <summary>
    //    /// The state of the gamepad as of the last update.
    //    /// </summary>
    //    public static GamePadState CurrentGamePadState
    //    {
    //        get
    //        {
    //            return currentGamePadState;
    //        }
    //    }


    //    /// <summary>
    //    /// The state of the gamepad as of the previous update.
    //    /// </summary>
    //    private static GamePadState previousGamePadState;


    //    #region GamePadButton Pressed Queries


    //    /// <summary>
    //    /// Check if the gamepad's Start button is pressed.
    //    /// </summary>
    //    public static bool IsGamePadStartPressed()
    //    {
    //        return ( currentGamePadState.Buttons.Start == ButtonState.Pressed );
    //    }


    //    /// <summary>
    //    /// Check if the gamepad's Back button is pressed.
    //    /// </summary>
    //    public static bool IsGamePadBackPressed()
    //    {
    //        return ( currentGamePadState.Buttons.Back == ButtonState.Pressed );
    //    }


    //    /// <summary>
    //    /// Check if the gamepad's A button is pressed.
    //    /// </summary>
    //    public static bool IsGamePadAPressed()
    //    {
    //        return ( currentGamePadState.Buttons.A == ButtonState.Pressed );
    //    }


    //    /// <summary>
    //    /// Check if the gamepad's B button is pressed.
    //    /// </summary>
    //    public static bool IsGamePadBPressed()
    //    {
    //        return ( currentGamePadState.Buttons.B == ButtonState.Pressed );
    //    }


    //    /// <summary>
    //    /// Check if the gamepad's X button is pressed.
    //    /// </summary>
    //    public static bool IsGamePadXPressed()
    //    {
    //        return ( currentGamePadState.Buttons.X == ButtonState.Pressed );
    //    }


    //    /// <summary>
    //    /// Check if the gamepad's Y button is pressed.
    //    /// </summary>
    //    public static bool IsGamePadYPressed()
    //    {
    //        return ( currentGamePadState.Buttons.Y == ButtonState.Pressed );
    //    }


    //    /// <summary>
    //    /// Check if the gamepad's LeftShoulder button is pressed.
    //    /// </summary>
    //    public static bool IsGamePadLeftShoulderPressed()
    //    {
    //        return ( currentGamePadState.Buttons.LeftShoulder == ButtonState.Pressed );
    //    }


    //    /// <summary>
    //    /// <summary>
    //    /// Check if the gamepad's RightShoulder button is pressed.
    //    /// </summary>
    //    public static bool IsGamePadRightShoulderPressed()
    //    {
    //        return ( currentGamePadState.Buttons.RightShoulder == ButtonState.Pressed );
    //    }


    //    /// <summary>
    //    /// Check if Up on the gamepad's directional pad is pressed.
    //    /// </summary>
    //    public static bool IsGamePadDPadUpPressed()
    //    {
    //        return ( currentGamePadState.DPad.Up == ButtonState.Pressed );
    //    }


    //    /// <summary>
    //    /// Check if Down on the gamepad's directional pad is pressed.
    //    /// </summary>
    //    public static bool IsGamePadDPadDownPressed()
    //    {
    //        return ( currentGamePadState.DPad.Down == ButtonState.Pressed );
    //    }


    //    /// <summary>
    //    /// Check if Left on the gamepad's directional pad is pressed.
    //    /// </summary>
    //    public static bool IsGamePadDPadLeftPressed()
    //    {
    //        return ( currentGamePadState.DPad.Left == ButtonState.Pressed );
    //    }


    //    /// <summary>
    //    /// Check if Right on the gamepad's directional pad is pressed.
    //    /// </summary>
    //    public static bool IsGamePadDPadRightPressed()
    //    {
    //        return ( currentGamePadState.DPad.Right == ButtonState.Pressed );
    //    }


    //    /// <summary>
    //    /// Check if the gamepad's left trigger is pressed.
    //    /// </summary>
    //    public static bool IsGamePadLeftTriggerPressed()
    //    {
    //        return ( currentGamePadState.Triggers.Left > analogLimit );
    //    }


    //    /// <summary>
    //    /// Check if the gamepad's right trigger is pressed.
    //    /// </summary>
    //    public static bool IsGamePadRightTriggerPressed()
    //    {
    //        return ( currentGamePadState.Triggers.Right > analogLimit );
    //    }


    //    /// <summary>
    //    /// Check if Up on the gamepad's left analog stick is pressed.
    //    /// </summary>
    //    public static bool IsGamePadLeftStickUpPressed()
    //    {
    //        return ( currentGamePadState.ThumbSticks.Left.Y > analogLimit );
    //    }


    //    /// <summary>
    //    /// Check if Down on the gamepad's left analog stick is pressed.
    //    /// </summary>
    //    public static bool IsGamePadLeftStickDownPressed()
    //    {
    //        return ( -1f * currentGamePadState.ThumbSticks.Left.Y > analogLimit );
    //    }


    //    /// <summary>
    //    /// Check if Left on the gamepad's left analog stick is pressed.
    //    /// </summary>
    //    public static bool IsGamePadLeftStickLeftPressed()
    //    {
    //        return ( -1f * currentGamePadState.ThumbSticks.Left.X > analogLimit );
    //    }


    //    /// <summary>
    //    /// Check if Right on the gamepad's left analog stick is pressed.
    //    /// </summary>
    //    public static bool IsGamePadLeftStickRightPressed()
    //    {
    //        return ( currentGamePadState.ThumbSticks.Left.X > analogLimit );
    //    }


    //    /// <summary>
    //    /// Check if the GamePadKey value specified is pressed.
    //    /// </summary>
    //    private static bool IsGamePadButtonPressed( GamePadButtons gamePadKey )
    //    {
    //        switch( gamePadKey )
    //        {
    //            case GamePadButtons.Start:
    //                return IsGamePadStartPressed();

    //            case GamePadButtons.Back:
    //                return IsGamePadBackPressed();

    //            case GamePadButtons.A:
    //                return IsGamePadAPressed();

    //            case GamePadButtons.B:
    //                return IsGamePadBPressed();

    //            case GamePadButtons.X:
    //                return IsGamePadXPressed();

    //            case GamePadButtons.Y:
    //                return IsGamePadYPressed();

    //            case GamePadButtons.LeftShoulder:
    //                return IsGamePadLeftShoulderPressed();

    //            case GamePadButtons.RightShoulder:
    //                return IsGamePadRightShoulderPressed();

    //            case GamePadButtons.LeftTrigger:
    //                return IsGamePadLeftTriggerPressed();

    //            case GamePadButtons.RightTrigger:
    //                return IsGamePadRightTriggerPressed();

    //            case GamePadButtons.Up:
    //                return IsGamePadDPadUpPressed() ||
    //                    IsGamePadLeftStickUpPressed();

    //            case GamePadButtons.Down:
    //                return IsGamePadDPadDownPressed() ||
    //                    IsGamePadLeftStickDownPressed();

    //            case GamePadButtons.Left:
    //                return IsGamePadDPadLeftPressed() ||
    //                    IsGamePadLeftStickLeftPressed();

    //            case GamePadButtons.Right:
    //                return IsGamePadDPadRightPressed() ||
    //                    IsGamePadLeftStickRightPressed();
    //        }

    //        return false;
    //    }


    //    #endregion


    //    #region GamePadButton Triggered Queries


    //    /// <summary>
    //    /// Check if the gamepad's Start button was just pressed.
    //    /// </summary>
    //    public static bool IsGamePadStartTriggered()
    //    {
    //        return ( ( currentGamePadState.Buttons.Start == ButtonState.Pressed ) &&
    //          ( previousGamePadState.Buttons.Start == ButtonState.Released ) );
    //    }


    //    /// <summary>
    //    /// Check if the gamepad's Back button was just pressed.
    //    /// </summary>
    //    public static bool IsGamePadBackTriggered()
    //    {
    //        return ( ( currentGamePadState.Buttons.Back == ButtonState.Pressed ) &&
    //          ( previousGamePadState.Buttons.Back == ButtonState.Released ) );
    //    }


    //    /// <summary>
    //    /// Check if the gamepad's A button was just pressed.
    //    /// </summary>
    //    public static bool IsGamePadATriggered()
    //    {
    //        return ( ( currentGamePadState.Buttons.A == ButtonState.Pressed ) &&
    //          ( previousGamePadState.Buttons.A == ButtonState.Released ) );
    //    }


    //    /// <summary>
    //    /// Check if the gamepad's B button was just pressed.
    //    /// </summary>
    //    public static bool IsGamePadBTriggered()
    //    {
    //        return ( ( currentGamePadState.Buttons.B == ButtonState.Pressed ) &&
    //          ( previousGamePadState.Buttons.B == ButtonState.Released ) );
    //    }


    //    /// <summary>
    //    /// Check if the gamepad's X button was just pressed.
    //    /// </summary>
    //    public static bool IsGamePadXTriggered()
    //    {
    //        return ( ( currentGamePadState.Buttons.X == ButtonState.Pressed ) &&
    //          ( previousGamePadState.Buttons.X == ButtonState.Released ) );
    //    }


    //    /// <summary>
    //    /// Check if the gamepad's Y button was just pressed.
    //    /// </summary>
    //    public static bool IsGamePadYTriggered()
    //    {
    //        return ( ( currentGamePadState.Buttons.Y == ButtonState.Pressed ) &&
    //          ( previousGamePadState.Buttons.Y == ButtonState.Released ) );
    //    }


    //    /// <summary>
    //    /// Check if the gamepad's LeftShoulder button was just pressed.
    //    /// </summary>
    //    public static bool IsGamePadLeftShoulderTriggered()
    //    {
    //        return (
    //            ( currentGamePadState.Buttons.LeftShoulder == ButtonState.Pressed ) &&
    //            ( previousGamePadState.Buttons.LeftShoulder == ButtonState.Released ) );
    //    }


    //    /// <summary>
    //    /// Check if the gamepad's RightShoulder button was just pressed.
    //    /// </summary>
    //    public static bool IsGamePadRightShoulderTriggered()
    //    {
    //        return (
    //            ( currentGamePadState.Buttons.RightShoulder == ButtonState.Pressed ) &&
    //            ( previousGamePadState.Buttons.RightShoulder == ButtonState.Released ) );
    //    }


    //    /// <summary>
    //    /// Check if Up on the gamepad's directional pad was just pressed.
    //    /// </summary>
    //    public static bool IsGamePadDPadUpTriggered()
    //    {
    //        return ( ( currentGamePadState.DPad.Up == ButtonState.Pressed ) &&
    //          ( previousGamePadState.DPad.Up == ButtonState.Released ) );
    //    }


    //    /// <summary>
    //    /// Check if Down on the gamepad's directional pad was just pressed.
    //    /// </summary>
    //    public static bool IsGamePadDPadDownTriggered()
    //    {
    //        return ( ( currentGamePadState.DPad.Down == ButtonState.Pressed ) &&
    //          ( previousGamePadState.DPad.Down == ButtonState.Released ) );
    //    }


    //    /// <summary>
    //    /// Check if Left on the gamepad's directional pad was just pressed.
    //    /// </summary>
    //    public static bool IsGamePadDPadLeftTriggered()
    //    {
    //        return ( ( currentGamePadState.DPad.Left == ButtonState.Pressed ) &&
    //          ( previousGamePadState.DPad.Left == ButtonState.Released ) );
    //    }


    //    /// <summary>
    //    /// Check if Right on the gamepad's directional pad was just pressed.
    //    /// </summary>
    //    public static bool IsGamePadDPadRightTriggered()
    //    {
    //        return ( ( currentGamePadState.DPad.Right == ButtonState.Pressed ) &&
    //          ( previousGamePadState.DPad.Right == ButtonState.Released ) );
    //    }


    //    /// <summary>
    //    /// Check if the gamepad's left trigger was just pressed.
    //    /// </summary>
    //    public static bool IsGamePadLeftTriggerTriggered()
    //    {
    //        return ( ( currentGamePadState.Triggers.Left > analogLimit ) &&
    //            ( previousGamePadState.Triggers.Left < analogLimit ) );
    //    }


    //    /// <summary>
    //    /// Check if the gamepad's right trigger was just pressed.
    //    /// </summary>
    //    public static bool IsGamePadRightTriggerTriggered()
    //    {
    //        return ( ( currentGamePadState.Triggers.Right > analogLimit ) &&
    //            ( previousGamePadState.Triggers.Right < analogLimit ) );
    //    }


    //    /// <summary>
    //    /// Check if Up on the gamepad's left analog stick was just pressed.
    //    /// </summary>
    //    public static bool IsGamePadLeftStickUpTriggered()
    //    {
    //        return ( ( currentGamePadState.ThumbSticks.Left.Y > analogLimit ) &&
    //            ( previousGamePadState.ThumbSticks.Left.Y < analogLimit ) );
    //    }


    //    /// <summary>
    //    /// Check if Down on the gamepad's left analog stick was just pressed.
    //    /// </summary>
    //    public static bool IsGamePadLeftStickDownTriggered()
    //    {
    //        return ( ( -1f * currentGamePadState.ThumbSticks.Left.Y > analogLimit ) &&
    //            ( -1f * previousGamePadState.ThumbSticks.Left.Y < analogLimit ) );
    //    }


    //    /// <summary>
    //    /// Check if Left on the gamepad's left analog stick was just pressed.
    //    /// </summary>
    //    public static bool IsGamePadLeftStickLeftTriggered()
    //    {
    //        return ( ( -1f * currentGamePadState.ThumbSticks.Left.X > analogLimit ) &&
    //            ( -1f * previousGamePadState.ThumbSticks.Left.X < analogLimit ) );
    //    }


    //    /// <summary>
    //    /// Check if Right on the gamepad's left analog stick was just pressed.
    //    /// </summary>
    //    public static bool IsGamePadLeftStickRightTriggered()
    //    {
    //        return ( ( currentGamePadState.ThumbSticks.Left.X > analogLimit ) &&
    //            ( previousGamePadState.ThumbSticks.Left.X < analogLimit ) );
    //    }


    //    /// <summary>
    //    /// Check if the GamePadKey value specified was pressed this frame.
    //    /// </summary>
    //    private static bool IsGamePadButtonTriggered( GamePadButtons gamePadKey )
    //    {
    //        switch( gamePadKey )
    //        {
    //            case GamePadButtons.Start:
    //                return IsGamePadStartTriggered();

    //            case GamePadButtons.Back:
    //                return IsGamePadBackTriggered();

    //            case GamePadButtons.A:
    //                return IsGamePadATriggered();

    //            case GamePadButtons.B:
    //                return IsGamePadBTriggered();

    //            case GamePadButtons.X:
    //                return IsGamePadXTriggered();

    //            case GamePadButtons.Y:
    //                return IsGamePadYTriggered();

    //            case GamePadButtons.LeftShoulder:
    //                return IsGamePadLeftShoulderTriggered();

    //            case GamePadButtons.RightShoulder:
    //                return IsGamePadRightShoulderTriggered();

    //            case GamePadButtons.LeftTrigger:
    //                return IsGamePadLeftTriggerTriggered();

    //            case GamePadButtons.RightTrigger:
    //                return IsGamePadRightTriggerTriggered();

    //            case GamePadButtons.Up:
    //                return IsGamePadDPadUpTriggered() ||
    //                    IsGamePadLeftStickUpTriggered();

    //            case GamePadButtons.Down:
    //                return IsGamePadDPadDownTriggered() ||
    //                    IsGamePadLeftStickDownTriggered();

    //            case GamePadButtons.Left:
    //                return IsGamePadDPadLeftTriggered() ||
    //                    IsGamePadLeftStickLeftTriggered();

    //            case GamePadButtons.Right:
    //                return IsGamePadDPadRightTriggered() ||
    //                    IsGamePadLeftStickRightTriggered();
    //        }

    //        return false;
    //    }


    //    #endregion


    //    #endregion


    //    #region Action Mapping


    //    /// <summary>
    //    /// The action mappings for the game.
    //    /// </summary>
    //    private static ActionMap[] actionMaps;


    //    public static ActionMap[] ActionMaps
    //    {
    //        get
    //        {
    //            return actionMaps;
    //        }
    //    }


    //    /// <summary>
    //    /// Reset the action maps to their default values.
    //    /// </summary>
    //    private static void ResetActionMaps()
    //    {
    //        actionMaps = new ActionMap[(int)Action.TotalActionCount];

    //        actionMaps[(int)Action.MainMenu] = new ActionMap();
    //        actionMaps[(int)Action.MainMenu].keyboardKeys.Add(
    //            Keys.Tab );
    //        actionMaps[(int)Action.MainMenu].gamePadButtons.Add(
    //            GamePadButtons.Start );

    //        actionMaps[(int)Action.Ok] = new ActionMap();
    //        actionMaps[(int)Action.Ok].keyboardKeys.Add(
    //            Keys.Enter );
    //        actionMaps[(int)Action.Ok].gamePadButtons.Add(
    //            GamePadButtons.A );

    //        actionMaps[(int)Action.Back] = new ActionMap();
    //        actionMaps[(int)Action.Back].keyboardKeys.Add(
    //            Keys.Escape );
    //        actionMaps[(int)Action.Back].gamePadButtons.Add(
    //            GamePadButtons.B );

    //        actionMaps[(int)Action.ExitGame] = new ActionMap();
    //        actionMaps[(int)Action.ExitGame].keyboardKeys.Add(
    //            Keys.Escape );
    //        actionMaps[(int)Action.ExitGame].keyboardKeys.Add(
    //            Keys.Q );
    //        actionMaps[(int)Action.ExitGame].gamePadButtons.Add(
    //            GamePadButtons.Back );

    //        actionMaps[(int)Action.TakeView] = new ActionMap();
    //        actionMaps[(int)Action.TakeView].keyboardKeys.Add(
    //            Keys.LeftControl );
    //        actionMaps[(int)Action.TakeView].gamePadButtons.Add(
    //            GamePadButtons.Y );

    //        actionMaps[(int)Action.CursorUp] = new ActionMap();
    //        actionMaps[(int)Action.CursorUp].keyboardKeys.Add(
    //            Keys.Up );
    //        actionMaps[(int)Action.CursorUp].gamePadButtons.Add(
    //            GamePadButtons.Up );

    //        actionMaps[(int)Action.CursorDown] = new ActionMap();
    //        actionMaps[(int)Action.CursorDown].keyboardKeys.Add(
    //            Keys.Down );
    //        actionMaps[(int)Action.CursorDown].gamePadButtons.Add(
    //            GamePadButtons.Down );

    //        actionMaps[(int)Action.CursorLeft] = new ActionMap();
    //        actionMaps[(int)Action.CursorLeft].keyboardKeys.Add(
    //            Keys.Left );
    //        actionMaps[(int)Action.CursorLeft].gamePadButtons.Add(
    //            GamePadButtons.Left );

    //        actionMaps[(int)Action.CursorRight] = new ActionMap();
    //        actionMaps[(int)Action.CursorRight].keyboardKeys.Add(
    //            Keys.Right );
    //        actionMaps[(int)Action.CursorRight].gamePadButtons.Add(
    //            GamePadButtons.Right );

    //        actionMaps[(int)Action.PageLeft] = new ActionMap();
    //        actionMaps[(int)Action.PageLeft].keyboardKeys.Add(
    //            Keys.LeftShift );
    //        actionMaps[(int)Action.PageLeft].gamePadButtons.Add(
    //            GamePadButtons.LeftTrigger );

    //        actionMaps[(int)Action.PageRight] = new ActionMap();
    //        actionMaps[(int)Action.PageRight].keyboardKeys.Add(
    //            Keys.RightShift );
    //        actionMaps[(int)Action.PageRight].gamePadButtons.Add(
    //            GamePadButtons.RightTrigger );

    //        actionMaps[(int)Action.AnyButton] = new ActionMap();
    //        actionMaps[(int)Action.AnyButton].gamePadButtons.Add( GamePadButtons.A );
    //        actionMaps[(int)Action.AnyButton].gamePadButtons.Add( GamePadButtons.B );
    //        actionMaps[(int)Action.AnyButton].gamePadButtons.Add( GamePadButtons.X );
    //        actionMaps[(int)Action.AnyButton].gamePadButtons.Add( GamePadButtons.Y );
    //        actionMaps[(int)Action.AnyButton].keyboardKeys.Add( Keys.Enter );
    //        actionMaps[(int)Action.AnyButton].keyboardKeys.Add( Keys.Space );
    //    }


    //    /// <summary>
    //    /// Check if an action has been pressed.
    //    /// </summary>
    //    public static bool IsActionPressed( Action action )
    //    {
    //        return IsActionMapPressed( actionMaps[(int)action] );
    //    }


    //    /// <summary>
    //    /// Check if an action was just performed in the most recent update.
    //    /// </summary>
    //    public static bool IsActionTriggered( Action action )
    //    {
    //        return IsActionMapTriggered( actionMaps[(int)action] );
    //    }


    //    /// <summary>
    //    /// Check if an action map has been pressed.
    //    /// </summary>
    //    private static bool IsActionMapPressed( ActionMap actionMap )
    //    {
    //        for( int i = 0; i < actionMap.keyboardKeys.Count; i++ )
    //        {
    //            if( IsKeyPressed( actionMap.keyboardKeys[i] ) )
    //            {
    //                return true;
    //            }
    //        }
    //        if( currentGamePadState.IsConnected )
    //        {
    //            for( int i = 0; i < actionMap.gamePadButtons.Count; i++ )
    //            {
    //                if( IsGamePadButtonPressed( actionMap.gamePadButtons[i] ) )
    //                {
    //                    return true;
    //                }
    //            }
    //        }
    //        return false;
    //    }


    //    /// <summary>
    //    /// Check if an action map has been triggered this frame.
    //    /// </summary>
    //    private static bool IsActionMapTriggered( ActionMap actionMap )
    //    {
    //        for( int i = 0; i < actionMap.keyboardKeys.Count; i++ )
    //        {
    //            if( IsKeyTriggered( actionMap.keyboardKeys[i] ) )
    //            {
    //                return true;
    //            }
    //        }
    //        if( currentGamePadState.IsConnected )
    //        {
    //            for( int i = 0; i < actionMap.gamePadButtons.Count; i++ )
    //            {
    //                if( IsGamePadButtonTriggered( actionMap.gamePadButtons[i] ) )
    //                {
    //                    return true;
    //                }
    //            }
    //        }
    //        return false;
    //    }


    //    #endregion


    //    #region Initialization


    //    /// <summary>
    //    /// Initializes the default control keys for all actions.
    //    /// </summary>
    //    public static void Initialize()
    //    {
    //        ResetActionMaps();
    //    }


    //    #endregion


    //    #region Updating


    //    /// <summary>
    //    /// Updates the keyboard and gamepad control states.
    //    /// </summary>
    //    public static void Update()
    //    {
    //        // update the keyboard state
    //        previousKeyboardState = currentKeyboardState;
    //        currentKeyboardState = Keyboard.GetState();

    //        // update the gamepad state
    //        previousGamePadState = currentGamePadState;
    //        currentGamePadState = GamePad.GetState( PlayerIndex.One );
    //    }


    //    #endregion
    //}

    public class InputManager : GameComponent
    {
        Game1 game;

        public InputManager( Game1 game )
            : base( game )
        {
            this.game = game;
        }

        public override void Update( GameTime gameTime )
        {
            //PlayerIndex player2Controller;
            //if( GamePad.GetState( game.Data.Player1.Controller ).IsConnected )
            //{
            //    CheckPlayerTwoStart( game.Data.Player1.Controller, out player2Controller );
            //    game.InitializePlayer2( player2Controller );
            //}
            
            base.Update( gameTime );
        }

        /// <summary>
        /// This method checks to see if any GamePads have pressed start.
        /// </summary>
        /// <param name="playerOne">The index of player one.</param>
        /// <returns>True if a player pressed Start, false otherwise.</returns>
        public static bool CheckPlayerOneStart( out PlayerIndex playerOne )
        {
            playerOne = PlayerIndex.One;
            if( IsSupported( GamePad.GetCapabilities( PlayerIndex.One ).GamePadType ) &&
                ( GamePad.GetState( PlayerIndex.One ).Buttons.Start == ButtonState.Pressed ) )
            {
                playerOne = PlayerIndex.One;
                return true;
            }
            if( IsSupported( GamePad.GetCapabilities( PlayerIndex.Two ).GamePadType ) &&
                ( GamePad.GetState( PlayerIndex.Two ).Buttons.Start == ButtonState.Pressed ) )
            {
                playerOne = PlayerIndex.Two;
                return true;
            }
            if( IsSupported( GamePad.GetCapabilities( PlayerIndex.Three ).GamePadType ) &&
                ( GamePad.GetState( PlayerIndex.Three ).Buttons.Start == ButtonState.Pressed ) )
            {
                playerOne = PlayerIndex.Three;
                return true;
            }
            if( IsSupported( GamePad.GetCapabilities( PlayerIndex.Four ).GamePadType ) &&
                ( GamePad.GetState( PlayerIndex.Four ).Buttons.Start == ButtonState.Pressed ) )
            {
                playerOne = PlayerIndex.Four;
                return true;
            }
            return false;
        }

        /// <summary>
        /// This method checks to see if any GamePads other than Player One have pressed start.
        /// </summary>
        /// <param name="playerOne">The index of player one.</param>
        /// <param name="playerTwo">The returned index of the lowest player who has pressed start.</param>
        /// <returns>True if player two pressed start, otherwise false.</returns>
        //public static bool CheckPlayerTwoStart( PlayerIndex playerOne, out PlayerIndex playerTwo )
        //{
        //    playerTwo = playerOne;
        //    if( IsSupported( GamePad.GetCapabilities( PlayerIndex.One ).GamePadType ) &&
        //        ( GamePad.GetState( PlayerIndex.One ).Buttons.Start == ButtonState.Pressed ) &&
        //        ( playerOne != PlayerIndex.One ) )
        //    {
        //        playerTwo = PlayerIndex.One;
        //        return true;
        //    }
        //    if( IsSupported( GamePad.GetCapabilities( PlayerIndex.Two ).GamePadType ) &&
        //        ( GamePad.GetState( PlayerIndex.Two ).Buttons.Start == ButtonState.Pressed ) &&
        //        ( playerOne != PlayerIndex.Two ) )
        //    {
        //        playerTwo = PlayerIndex.Two;
        //        return true;
        //    }
        //    if( IsSupported( GamePad.GetCapabilities( PlayerIndex.Three ).GamePadType ) &&
        //        ( GamePad.GetState( PlayerIndex.Three ).Buttons.Start == ButtonState.Pressed ) &&
        //        ( playerOne != PlayerIndex.Three ) )
        //    {
        //        playerTwo = PlayerIndex.Three;
        //        return true;
        //    }
        //    if( IsSupported( GamePad.GetCapabilities( PlayerIndex.Four ).GamePadType ) &&
        //        ( GamePad.GetState( PlayerIndex.Four ).Buttons.Start == ButtonState.Pressed ) &&
        //        ( playerOne != PlayerIndex.Four ) )
        //    {
        //        playerTwo = PlayerIndex.Four;
        //        return true;
        //    }
        //    return false;
        //}

        /// <summary>
        /// This function determines if a controller is a type we support.
        /// We want to ignore any drums, guitars, etc. that might be
        /// plugged in.
        /// </summary>
        /// <param name="type">The type of controller being tested</param>
        /// <returns>true if the controller is supported by this game.</returns>
        private static bool IsSupported( GamePadType type )
        {
            //return ( ( type == GamePadType.GamePad ) ||
            //        ( type == GamePadType.ArcadeStick ) ||
            //        ( type == GamePadType.FlightStick ) );
            return ( type == GamePadType.GamePad );
        }

        [Conditional( "DEBUG" )]
        public static void DebugExit( InputState input, Game game )
        {
            if( input.CheckAction( Action.DebugExit ) )
            {
                Trace.WriteLine( "Debug Exit was triggered, exiting game." );
                game.Exit();
            }
        }
    }
}