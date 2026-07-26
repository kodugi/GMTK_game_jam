using System;
using System.Collections.Generic;

namespace RegistrationNameSpace
{
    public static class MathUtils
    {
        public static List<double> ExtractMultipleLogNormal(double gamma, double mu, double sigma, int count)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < count; i++)
            {
                result.Add(gamma + Math.Exp(ExtractNormal(mu, sigma)));
            }

            return result;
        }

        public static double ExtractNormal(double mu, double sigma)
        {
            Random random = new Random();
            double u1 = 1 - random.NextDouble();
            double u2 = 1 - random.NextDouble();
            return mu + sigma * Math.Sqrt(-2 * Math.Log(u1)) * Math.Sin(2 * Math.PI * u2);
        }
    }
}