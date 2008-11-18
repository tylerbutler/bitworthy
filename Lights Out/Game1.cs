using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Lights_Out
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class LightsOutGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D background;
        Rectangle viewportRect;
        Gameboard board;
        Selector selection;
        GamePadState previousGamePadState = GamePad.GetState(PlayerIndex.One);

        public LightsOutGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //Initialize the board array
            board = new Gameboard(Constants.boardWidth, Constants.boardHeight,
                Content.Load<Texture2D>(Constants.unlitBlockName),
                Content.Load<Texture2D>(Constants.litBlockName), Content);//new LightButton[Constants.boardWidth, Constants.boardHeight];

            Vector2 startPosition = Constants.boardStartPosition;
            float posX = startPosition.X, posY = startPosition.Y;
            board.tiles[0, 0].Position = startPosition;

            Dimensions d = new Dimensions();
            d.width = (int)(((float)Constants.tileWidth) * Constants.scaleFactor);
            d.height = (int)(((float)Constants.tileHeight) * Constants.scaleFactor);
            int scaledOffset = (int)(((float)Constants.tileOffsetTop) * Constants.scaleFactor);

            for (int i = 0; i < Constants.boardWidth; i++)
            {
                for (int j = 0; j < Constants.boardHeight; j++)
                {
                    float Y = posY + (d.height * j);
                    float X = posX + (d.width * i);
                    board.tiles[i, j].Position = new Vector2(X, Y);
                }
            }

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>(Constants.backgroundName);

            // Load the selection sprite
            selection = new Selector(Content.Load<Texture2D>(Constants.selectorName));
            viewportRect = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
        
            // Load a puzzle
            board.LoadBoard("Puzzles\\Puzzles.xml", 1);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            InputManager.Update();

            if (InputManager.IsGamePadATriggered())
            {
                board.Update(selection.getIndex()[0], selection.getIndex()[1]);
            }

            if (InputManager.IsGamePadDPadDownTriggered() ||
                InputManager.IsGamePadLeftStickDownTriggered()) selection.moveDown();
            if (InputManager.IsGamePadDPadLeftTriggered() ||
                InputManager.IsGamePadLeftStickLeftTriggered()) selection.moveLeft();
            if (InputManager.IsGamePadDPadRightTriggered() ||
                InputManager.IsGamePadLeftStickRightTriggered()) selection.moveRight();
            if (InputManager.IsGamePadDPadUpTriggered() ||
                InputManager.IsGamePadLeftStickUpTriggered()) selection.moveUp();

            // Serialize the gameboard out if both triggers and Y is pressed
            if (InputManager.IsGamePadRightTriggerPressed() &&
                InputManager.IsGamePadLeftTriggerPressed() &&
                InputManager.IsGamePadYTriggered())
            {
                board.SaveBoard("Puzzles\\foo.xml");
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            spriteBatch.Draw(background, viewportRect, Color.White);
            board.Draw(spriteBatch);
            spriteBatch.Draw(selection.Sprite, selection.getOffsetPosition(board.tiles[selection.getIndex()[0], selection.getIndex()[1]].Position),
                null, Color.White, selection.Rotation, selection.Center, Constants.scaleFactor, SpriteEffects.None, 0);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
