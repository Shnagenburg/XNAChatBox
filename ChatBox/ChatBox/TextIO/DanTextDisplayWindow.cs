using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ChatBox
{
    // A block of messages, like a chat window
    class DanTextDisplayWindow
    {
        private int iLines;
        List<String> mChatLog;

        // Position of textwindow
        private Vector2 mPosition;
        public Vector2 Position
        {
            get 
            {
                return mPosition;
            }
        }
               

        //For the time being, background
        private FilledRectangle mBackground;

        //The sprite font for text
        SpriteFont mFont;

        /// <summary>
        /// Create a new DanTextWindow
        /// </summary>
        public DanTextDisplayWindow(int x, int y, int width, int lines)
        {
            mChatLog = new List<String>();
            mBackground = new FilledRectangle(x, y, width, 0, CUtil.GraphicsDevice);
            mPosition = new Vector2(x, y);

            iLines = lines;
        }

        /// <summary>
        /// Load the textwindow sprite using the Content Pipeline
        /// </summary>
        /// <param name="theContentManager"></param>
        /// <param name="rect"></param>
        public void LoadContent(ContentManager theContentManager)
        {
            mBackground.LoadContent(CUtil.GraphicsDevice);

            Color []cols = {Color.Yellow, Color.LightYellow, Color.LightYellow, Color.Tan};
            mBackground.SetColors(cols);

            mFont = theContentManager.Load<SpriteFont>("ourFont");

            //test some strings!
            mChatLog.Insert(0, "Window Constructed. Like a boss.");
            //chatLog.Insert(0, "-this shouldnt overlap---\n--with anything---\n---fo srsly-----");
        }

        /// <summary>
        /// Draw the text window and recent messages!
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw bg
            mBackground.Draw(spriteBatch);

            for (int i = 0; i < iLines; i++)
            {
                if ( i < mChatLog.Count)
                {
                    //REVISIT un-hardcodeify!
                    Vector2 offset = new Vector2(0, ((iLines - i) * mFont.LineSpacing) 
                        + (mBackground.Box.Height - ((iLines + 1) * mFont.LineSpacing)));
                    spriteBatch.DrawString(mFont, mChatLog[i], mPosition + offset, Color.ForestGreen);
                }
            }

            // Find the center of the string
            //Vector2 FontOrigin = sFont.MeasureString(sText) / 2;
            // Draw the string
            //spriteBatch.DrawString(sFont, sText, FontPos, Color.Red);

        }

        public void PushString(string str)
        {
            mChatLog.Insert(0, str);
        }

        public void SetHeight(int height)
        {
            mBackground.Box.Height = height;
        }

    }
}
