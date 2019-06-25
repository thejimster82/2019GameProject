using System;
namespace world
{
    public class worldLocation
    {
        private int xCoord;
        private int zCoord;
        public worldLocation()
        {
            xCoord = 0;
            zCoord = 0;
        }
        public worldLocation(int xLocation, int zLocation)
        {
            xCoord = xLocation;
            zCoord = zLocation;
        }
        public int x
        {
            get
            {
                return xCoord;
            }
            set
            {
                if (xCoord == value)
                {
                    return;
                }
                xCoord = value;
            }
        }

        public int z
        {
            get
            {
                return zCoord;
            }
            set
            {
                if (zCoord == value)
                {
                    return;
                }
                zCoord = value;
            }

        }

    }
}

