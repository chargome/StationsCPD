using System;
namespace Stations.Helper
{
    public static class DistanceCalculator
    {
        // returns distance between two poits in km 
        public static double DistanceBetweenPoints(double lon1, double lat1, double lon2, double lat2)
        {
            // coverts deg to rad
            double Radians(double x)
            {
                return x * Math.PI / 180;
            }


            double R = 6371; // km

            double sLat1 = Math.Sin(Radians(lat1));
            double sLat2 = Math.Sin(Radians(lat2));
            double cLat1 = Math.Cos(Radians(lat1));
            double cLat2 = Math.Cos(Radians(lat2));
            double cLon = Math.Cos(Radians(lon1) - Radians(lon2));

            double cosD = sLat1 * sLat2 + cLat1 * cLat2 * cLon;

            double d = Math.Acos(cosD);

            double dist = R * d;

            return dist;


        }


    }
}
