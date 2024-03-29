﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace FillingGlases
{
    class Program
    {
        public static List<Glas> AllGlases;

        public static Glas TopGlas;
        static void Main(string[] args)
        {
            Console.WriteLine("Hur många rader av glas? 2 mellan 50");
            int row = Convert.ToInt32(Console.ReadLine());
            if (row < 2)
            {
                row = 2;
            }else if (row > 50)
            {
                row = 50;
            }
            Console.WriteLine("Vilken rad vill du observera?");
            int r = Convert.ToInt32(Console.ReadLine());
            if (r > row)
            {
                r = row;
            }

            Console.WriteLine("Vilket glas från vänster vill du observera?");
            int g = Convert.ToInt32(Console.ReadLine());
            if (g > row)
            {
                g = row;
            }

            AllGlases = new List<Glas>();


            TopGlas = GenerateGlases(row, 1, 1, null);
            Console.WriteLine("");
            fillTheGlases(r, g);
            PrintPyramid(TopGlas, row, r, g);
            Console.ReadLine();
        }

        private static void fillTheGlases(int r, int g)
        {
            Glas theGlas = TopGlas;
            double MillSec = 0;
            while (theGlas.BelowRight != null)
            {
                if (theGlas.r == r)
                {
                    break;
                }
                else
                {
                    //Console.WriteLine(theGlas.r);
                    theGlas = theGlas.BelowRight;
                }
                
            }
            while (theGlas.GlasToLeft!=null)
            {
                if (theGlas.g == g)
                {
                    break;
                }
                else
                {
                    //Console.WriteLine(theGlas.g);
                    theGlas = theGlas.GlasToLeft;
                }
                              
            }
            Console.WriteLine("Glaset på den "+theGlas.r + " raden, "+ theGlas.g + " från vänster");
            //10.000 millisekunder
            while (theGlas.isNotFull)
            {
                TopGlas.addLiquid(1);
                MillSec = MillSec + 100;
                

            }



            Console.WriteLine("Det tar "+ MillSec / 100000 + " sekunder tills den är fyld");
        }

        private static Glas GenerateGlases(int row, int r, int g, Glas GoTrough)
        {
            Glas TheGlas = null;
            if (r>row)
            {
                return TheGlas;
            }
            else
            {
                TheGlas = new Glas(r, g, 10000);

                if (GoTrough != null)
                {
                    if (GoTrough.BelowLeft != null)
                    {
                        TheGlas.GlasToLeft = GoTrough.BelowLeft;
                        TheGlas.GlasToLeft.GlasToRight = TheGlas;
                    }

                    if (GoTrough.GlasToLeft != null)
                    {
                        if (GoTrough.GlasToLeft.BelowRight.g == g && GoTrough.GlasToLeft.BelowRight.r == r)
                        {
                            return GoTrough.GlasToLeft.BelowRight;
                        }
                    }
                }



                Glas LowerLeft = GenerateGlases(row, r + 1, g, TheGlas);
                TheGlas.BelowLeft = LowerLeft;


                Glas LowerRight = GenerateGlases(row, r + 1, g + 1, TheGlas);
                TheGlas.BelowRight = LowerRight;
                

                //Console.Write(TheGlas.r + "," + TheGlas.g);
                //Console.WriteLine("");
                return TheGlas;
            }

            
        }



        private static void PrintPyramid(Glas TheGlas, int row, int r, int g)
        {
            
            if (TheGlas != null && TheGlas.notVisited)
            {
                
                TheGlas.notVisited = false;
                //if (TheGlas.BelowLeft != null && TheGlas.BelowRight != null)
                //{
                //    Console.Write(" Stand on L:" + TheGlas.BelowLeft.g + " Stand on R:" + TheGlas.BelowRight.g);
                //}
                //if (TheGlas.GlasToLeft != null)
                //{
                //    Console.Write(" Is next to " + TheGlas.GlasToLeft.r + "," + TheGlas.GlasToLeft.g);
                //}
                for (int x = 0; x < row; x++)
                {
                    Console.Write("\t");
                }
                string paintglas = "G:" + TheGlas.r + "," + TheGlas.g;
                if (r == TheGlas.r && g == TheGlas.g)
                {
                    paintglas = "[G:" + TheGlas.r + "," + TheGlas.g+"]";
                }
                

                Glas pointer = TheGlas.GlasToLeft;
                while (pointer != null)
                {                    
                    string tmp = paintglas;
                    
                    if (r == pointer.r && g == pointer.g)
                    {
                        paintglas = "[G:" + pointer.r + "," + pointer.g + "]" + "\t\t" + tmp;
                    }
                    else
                    {
                        paintglas = "G:" + pointer.r + "," + pointer.g + "\t\t" + tmp;
                    }
                    pointer = pointer.GlasToLeft;
                }

                Console.WriteLine(paintglas);

                string paintfill = TheGlas.ContainWatter.ToString();
                
                for (int x = 0; x < row; x++)
                {
                    Console.Write("\t");
                }
                pointer = TheGlas.GlasToLeft;
                while (pointer != null)
                {
                    string tmp = paintfill;
                    if (pointer.ContainWatter.ToString().Length > 7)
                    {
                        paintfill = pointer.ContainWatter.ToString() + "\t" + tmp;
                    }
                    else
                    {
                        paintfill = pointer.ContainWatter.ToString() + "\t\t" + tmp;
                    }
                    
                    pointer = pointer.GlasToLeft;
                }
                Console.WriteLine(paintfill);
                PrintPyramid(TheGlas.BelowRight, row - 1, r, g);

            }
            
        }
    }
}
