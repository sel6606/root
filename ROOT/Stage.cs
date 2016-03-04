using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace ROOT
{
    class Stage
    {

        //The list that will contain all the rectangle objects that make the stage.
        //May change to a gameobject list to allow placement of players and orb as well
        //Will definately change to at least be a platform list when platform class is made.
        List<Rectangle> stageBounds = new List<Rectangle>();

        SpriteBatch sb;

        Texture2D tileTex;

        public SpriteBatch SB
        {
            set { sb = value; }
        }

        public Texture2D TileTex
        {
            set { tileTex = value; }
        }

        public Stage(SpriteBatch sb, Texture2D tileTex)
        {
            this.sb = sb;
            this.tileTex = tileTex;
        }

        public void Draw()
        {
            for(int i=0; i<stageBounds.Count; i++)
            {
                sb.Draw(tileTex, stageBounds[i], Color.White);
            }
        }


        /// <summary>
        /// Reads in a stage from a textfile, parameter is String of file location
        /// </summary>
        public void ReadStage(String fileName)
        {
            //sets the initial y position for the platforms
            int ypos = 0;

            StreamReader readStage = new StreamReader(fileName);


            //List to hold all lines of the file.
            List<String> inputStrings = new List<string>();


            //should add each line of the file as a string to the list.
            while(readStage.ReadLine() != null)
            {
                inputStrings.Add(readStage.ReadLine());
            }


            
            for(int x = 0; x < inputStrings.Count; x++)
            {
                //resets the xposition for each new line
                int xpos = 0;


                String[] subHolder = inputStrings[x].Split();


                //checks to see if a platform should be made in that position
                for(int y = 0; y < subHolder.Length; y++)
                {

                    if(subHolder[y] == "x")
                    {
                        //makes a platform in the current position then shifts the position to the right.
                        //will add more if else statments if including player stars and orb starts
                        Rectangle added = new Rectangle(xpos, ypos, 20, 20);
                        stageBounds.Add(added);

                        xpos = xpos + 20;
                    }
                }
                //shifts the position down
                ypos = ypos + 20;
            }

        }
    }
}
