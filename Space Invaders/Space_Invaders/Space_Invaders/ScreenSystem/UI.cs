using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Space_Invaders
{
    class UI :GameObj
    {
        string text;

        public UI(GameObjName inName, Vector2 inPos, GameSprite inSprite)
        {
            Name = inName;
            Position = inPos;
            sprite = inSprite;

            sprite.setPosition(Position.X, Position.Y);
        }

        public override void Update(GameTime gametime)
        {

        }

        public void ChangeText(int inINT)
        {
            String X = inINT.ToString();

            text = X;

            ((FontSprite)sprite).changeText(X);
        }
    }
}
