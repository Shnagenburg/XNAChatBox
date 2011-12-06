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
    // A DanTextBox and DanTextWindow combined
    class DanTextPack
    {
        SpriteFont mFont;

        int iLines;

        // The height of the textbox 24 ain't bad for 14 pt font
        const int INPUT_HEIGHT = 24;
        const int DEFAULT_WIDTH = 640;
        const int DEFAULT_HEIGHT = 240;

        DanTextInputBox textBox;
        DanTextDisplayWindow textWindow;

        Rectangle mRect;

        // Whether or not the user is typing
        public bool IsEnabled;
                       
        /// <summary>
        /// Create a new text input and window package.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="lines">The number of lines the window shows in the text pack</param>
        public DanTextPack(int x, int y, int width, int lines)
        {
            iLines = lines;
            textBox = new DanTextInputBox(x, width);
            textWindow = new DanTextDisplayWindow(x, y, width, lines);
        }

        /// <summary>
        /// Load the content for text pack from the content pipeline (Expensive)
        /// </summary>
        /// <param name="theContentManager"></param>
        public void LoadContent(ContentManager theContentManager)
        {
            mFont = theContentManager.Load<SpriteFont>("ourFont");
            textBox.LoadContent(theContentManager);
            textWindow.LoadContent(theContentManager);

            // Now that we know how big our spritefont file is, set the heights
            int height = mFont.LineSpacing * iLines;
            textWindow.SetHeight(height);
            textBox.SetHeight(height, mFont.LineSpacing);
        }

        /// <summary>
        /// Update both the chat window and the chat input
        /// </summary>
        public void Update(GameTime theGameTime, KeyboardState keyState, KeyboardState prevState)
        {
            
            if (textBox.IsDone)
            {
                string str = textBox.GetText();
                textBox.Disable();
                textWindow.PushString(str);
                textBox.IsDone = false;
                textBox.IsEnabled = false;
            }
            else if (keyState.IsKeyDown(Keys.T) && prevState.IsKeyUp(Keys.T) && !textBox.IsEnabled)
            {
                textBox.Enable();
            }
            else
            {
                textBox.Update(theGameTime, keyState, prevState);
            }
            
        }

        /// <summary>
        /// Draw both the chat window and chat input
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            textBox.Draw(spriteBatch);
            textWindow.Draw(spriteBatch);
        }

        /// <summary>
        /// Add a string to the chat window
        /// </summary>
        /// <param name="str"></param>
        public void PushString(String str)
        {
            textWindow.PushString(str);
        }
    }
}
