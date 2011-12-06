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

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ChatBox
{

    // The input bar for chat
    class DanTextInputBox
    {
        bool bIsDone;
        public bool IsDone
        {
            get { return bIsDone; }
            set { bIsDone = value; }
        }

        bool bIsEnabled;
        public bool IsEnabled
        {
            get { return bIsEnabled; }
            set { bIsEnabled = value; }
        }

        //The precious text.
        public string sText = "";

        //Location of text in textbox;
        Vector2 mFontPos;
        
        //Size of the textbox 
        public Rectangle Size;

        //For the time being, background
        private FilledRectangle mBackground;
        public FilledRectangle Background
        {
            get { return mBackground; }
            set { mBackground = value; }
        }

        //The sprite font for text
        SpriteFont sFont;

        Keys[] previousKeyArray;

        //Init textbox
        public DanTextInputBox(int x, int width)
        {

            mBackground = new FilledRectangle(x, 0, width, 0, CUtil.GraphicsDevice);  
            bIsDone = false;
            mFontPos = new Vector2(x, 0);
        }

        //Load the textbox sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager)
        {
            sFont = theContentManager.Load<SpriteFont>("ourFont");

            mBackground.Box.Height = sFont.LineSpacing;
            mBackground.LoadContent(CUtil.GraphicsDevice);
            mBackground.Color = Color.Orange;
        }

        public void Update(GameTime gameTime, KeyboardState keyState, KeyboardState prevState)
        {
            if (!bIsEnabled)
                return;

            Keys[] currentKeyArray = keyState.GetPressedKeys();
            bool ok = true;
            // Look at all the keys currently being pressed on the keyboard
            foreach (Keys key in currentKeyArray)
            {
                // Check to see if this key was already pressed
                for (int i = 0; i < previousKeyArray.Length; i++)
                {
                    // If we just pressed this key, don't count it
                    if (previousKeyArray[i].Equals(key))
                        ok = false;
                }
                if (ok)
                {
                    sText = ManipulateString(sText, key);
                }
                ok = true;
            }

            previousKeyArray = currentKeyArray;
        }


        /// <summary>
        /// Handles all the special cases for manipulating the current string.
        /// </summary>
        /// <param name="?"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string ManipulateString(string str, Keys key)
        {
            switch (key)
            {
                case Keys.Back:
                    if (str.Length > 0)
                        str = str.Remove(str.Length - 1);
                    break;
                case Keys.Space:
                    str += " ";
                    break;
                case Keys.Left: // REVISIT implement text control
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    break;
                case Keys.Enter:
                    bIsDone = true;                    
                    break;
                default:
                    str += key.ToString().ToLower();
                    break;
            }
            return str;
        }





        //Draw the sprite to the screen
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw bg
            mBackground.Draw(spriteBatch);
            //Console.WriteLine(mPosition);
            // Find the center of the string
            //Vector2 FontOrigin = sFont.MeasureString(sText) / 2;
            
            // Draw the string
            spriteBatch.DrawString(sFont, sText, mFontPos, Color.Red);

        }

        /// <summary>
        /// Tells the text input to start accepting text and preloads the character T
        /// So that it doesn't appear as the first character pressed.
        /// </summary>
        public void Enable()
        {
            previousKeyArray = new Keys[1]; // This is sort of hack-ish, but it ensures T isn't typed
            previousKeyArray[0] = Keys.T; // on the first key press
            bIsEnabled = true;
        }
        public void Disable()
        {
            bIsEnabled = false;
            //keyBuff.Enabled = false;
            //keyBuff.TranslateMessage = false;
        }

        /// <summary>
        /// Gets the text then flushes the text
        /// </summary>
        /// <returns></returns>
        public String GetText()
        {
            String temp = sText;
            sText = "";
            return temp;
        }

        public void SetHeight(int y, int height)
        {
            mBackground.Box.Height = height;
            mBackground.Box.Y = y;
            mFontPos.Y = y;
        }


        //Update and collect text using buffer(if enabled / transmitting)
        //public void Update(GameTime theGameTime) //, Vector2 theSpeed, Vector2 theDirection)
        //{
        //    if (keyBuff.Enabled)
        //    {
        //        sText += keyBuff.GetText();
        //        while (sText.Contains('\b'))
        //        {
        //            int index = sText.IndexOf('\b');
        //            if (index > 0)
        //                sText = sText.Remove(index - 1);
        //            else
        //                sText = "";

        //        }
        //        if (sText.Contains('\n'))
        //        {
        //            EnterEvent.Invoke(this, new EventArgs());
        //        }

        //    }
        //}

    }
}
