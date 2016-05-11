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
        //fields to hold new starting positions for the player or orbs
        private int p1startX;
        private int p2startX;
        private int p3startX;
        private int p4startX;
        private int orbstartX;
        private int p1startY;
        private int p2startY;
        private int p3startY;
        private int p4startY;
        private int orbstartY;

        public int P1startX { get { return p1startX; } }
        public int P2startX { get { return p2startX; } }
        public int P3startX { get { return p3startX; } }
        public int P4startX { get { return p4startX; } }
        public int OrbstartX { get { return orbstartX; } }
        public int P1startY { get { return p1startY; } }
        public int P2startY { get { return p2startY; } }
        public int P3startY { get { return p3startY; } }
        public int P4startY { get { return p4startY; } }
        public int OrbstartY { get { return orbstartY; } }


        //List of the tiles that make up the stage
        List<Tile> stageBounds = new List<Tile>();

        //SpriteBatch needed for the draw method
        //Will be the SpriteBatch defined in Game1
        SpriteBatch sb;

        //Texture of the tiles
        Texture2D tileTex;

        public List<Tile> StageBounds
        {
            get { return stageBounds; }
        }

        public Stage(SpriteBatch sb, Texture2D tileTex)
        {
            this.sb = sb;
            this.tileTex = tileTex;
        }

        public void Draw()
        {
            for (int i = 0; i < stageBounds.Count; i++)
            {
                stageBounds[i].Draw(sb);
            }
        }


        /// <summary>
        /// Reads in a stage from a textfile, parameter is String of file location
        /// </summary>
        public void ReadStage(String fileName)
        {
            //sets the initial y position for the platforms
            int ypos = 0;

            //to check whether to change player 1 or player 2
            int playCount = 0;

            StreamReader readStage = new StreamReader(fileName);


            //List to hold all lines of the file.
            List<String> inputStrings = new List<string>();


            //should add each line of the file as a string to the list.
            while (readStage.Peek() != -1)
            {
                inputStrings.Add(readStage.ReadLine());
            }



            for (int x = 0; x < inputStrings.Count; x++)
            {
                //resets the xposition for each new line
                int xpos = 0;


                String[] subHolder = inputStrings[x].Split('.');


                //checks to see if a platform should be made in that position
                for (int y = 0; y < subHolder.Length; y++)
                {

                    if (subHolder[y] == "x")
                    {
                        //makes a platform in the current position then shifts the position to the right.
                        //will add more if else statments if including player stars and orb starts
                        Tile added = new Tile(xpos, ypos, 50, 50, tileTex);
                        stageBounds.Add(added);

                        xpos = xpos + 50;
                    }

                    //Changes the players starting positions
                    else if (subHolder[y] == "p")
                    {

                        //checks to see whether to set position of player 1 or 2
                        //now also checks and sets positions for players 3 and 4
                        if (playCount == 0)
                        {
                            p1startX = xpos;
                            p1startY = ypos;
                            playCount++;
                        }
                        else if(playCount == 1)
                        {
                            p2startX = xpos;
                            p2startY = ypos;
                            playCount++;
                        }
                        else if(playCount == 2)
                        {
                            p3startX = xpos;
                            p3startY = ypos;
                            playCount++;
                        }
                        else if(playCount == 3)
                        {
                            p4startX = xpos;
                            p4startY = ypos;
                            playCount++;
                        }
                        xpos = xpos + 50;
                    }

                    //changes the orbs starting position
                    else if(subHolder[y] == "o")
                     {
                       orbstartX = xpos;
                       orbstartY = ypos;
                       xpos = xpos + 50;
                    }
                    else
                    {
                        xpos = xpos + 50;
                    }

                }
                //shifts the position down
                ypos = ypos + 50;
            }

        }
    }
}
