using System;
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
    class GameObject
    {
        private Texture2D sprite;
        private Vector2 position;
        private Vector2 center;
        private float rotation;

        public GameObject(Texture2D loadedTexture)
        {
            Position = Vector2.Zero;
            Rotation = 0f;
            Sprite = loadedTexture;
            Center = new Vector2(sprite.Width / 2, Sprite.Height / 2);
        }

        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Center
        {
            get { return center; }
            set { center = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
    }
}