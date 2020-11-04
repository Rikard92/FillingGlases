using System;
using System.Collections.Generic;
using System.Text;

namespace FillingGlases
{
    public class Glas
    {
        public int r;

        public int g;

        public Glas GlasToLeft;
        public Glas GlasToRight;

        public Glas BelowRight;

        public Glas BelowLeft;

        public bool notVisited;

        public double ContainWatter;

        public int Overflowlimit;

        public bool isNotFull;

        public Glas(int row, int glassfromleft, int o)
        {
            notVisited = true;
            GlasToLeft = null;
            GlasToRight = null;
            BelowRight = null;
            BelowLeft = null;
            r = row;
            g = glassfromleft;
            ContainWatter = 0;
            Overflowlimit = o;
            isNotFull = true;

        }

        internal void addLiquid(double l)
        {
            ContainWatter = ContainWatter +  l;

            if (ContainWatter> Overflowlimit)
            {
                isNotFull = false;
                double thespill = ContainWatter - Overflowlimit;
                //Console.WriteLine("Spill:  " +thespill + " half of it: " + thespill/2);

                if (BelowLeft != null)
                {
                    BelowLeft.addLiquid(thespill / 2);
                }
                if (BelowRight != null)
                {
                    BelowRight.addLiquid(thespill / 2);
                }
                ContainWatter = Overflowlimit;
            }



            //if (ContainWatter> Overflow)
            //{
            //    isNotFull = false;
            //    double Theoverflow = (Overflow - ContainWatter) / 2;
            //    Console.WriteLine(Theoverflow);
            //    ContainWatter = ContainWatter - Theoverflow;

            //    if (BelowLeft != null)
            //    {
            //        BelowLeft.addLiquid(Theoverflow);
            //    }
            //    if(BelowRight != null)
            //    {
            //        BelowRight.addLiquid(Theoverflow);
            //    }
                
            //}
        }
    }
}
