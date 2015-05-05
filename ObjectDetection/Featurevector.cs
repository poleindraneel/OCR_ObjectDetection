using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classification
{
    public class Featurevector
    {
        
        private int dimension = 0;
        private double[] features = null;

        // Constructor
        public Featurevector(int numberoffeatures)
        {
            features = new double[numberoffeatures];
            dimension = numberoffeatures;
        }

        // Constructor
        public Featurevector(int numberoffeatures, int[] featurearray)
        {
            features = new double[numberoffeatures];
            int i = 0;
            foreach (int integer in featurearray)
            {
                this.addfeature(i,featurearray[i]);
                i++;
            }
            dimension = numberoffeatures;
        }


        public string[] getFeaturesasStringArray()
        {
            string[] s = new string[features.Length];
            for (int i = 0; i < features.Length; i++)
            {
                s[i] = features[i].ToString();
            }
            return s;
        }


        /// <summary>
        /// returns an integer which represents the dimension of the Featurevector
        /// </summary>
        /// <returns></returns>
        public int getDimension()
        {
            return dimension;
        }
        

        /// <summary>
        /// adds a feature to the specified index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void addfeature(int index, double value) 
        {
            features[index] = value;
        }

        /// <summary>
        /// returns the feature at the spezified index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double getfeature(int index) 
        {
            return features[index];
        }

        /// <summary>
        /// returns the length of the featurevector
        /// </summary>
        /// <returns></returns>
        public double length()
        {
            double length = 0;
            for (int i = 0; i < dimension; i++)
            {
                length = length + (features[i] * features[i]);
            }
            length =  Math.Sqrt(length);
            return length;

        }

        /// <summary>
        /// returns the distance of the featurevector to another featurevector
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public double getDistance(Featurevector vector) 
        {
            Featurevector distancevector = new Featurevector(dimension);

            for(int i=0;i<dimension;i++)
            {
               distancevector.addfeature(i, this.features[i] - vector.getfeature(i));
            }

            return distancevector.length();
        }

        

    }
}
