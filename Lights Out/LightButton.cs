using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Lights_Out
{
    class LightButton : GameObject
    {
        private bool isLit;
        private Texture2D litTexture, unlitTexture;

        public LightButton(Texture2D texture, Texture2D litTextureIn)
            : base(texture)
        {
            isLit = false;
            unlitTexture = texture;
            litTexture = litTextureIn;
            setSprite();
        }

        public bool toggleLight()
        {
            IsLit = !IsLit;
            return IsLit;
        }

        public bool IsLit
        {
            get { return isLit; }
            set
            {
                isLit = value;
                setSprite();
            }
        }

        private void setSprite()
        {
            if (isLit)
            {
                Sprite = litTexture;
            }
            else
            {
                Sprite = unlitTexture;
            }
        }
    }
}