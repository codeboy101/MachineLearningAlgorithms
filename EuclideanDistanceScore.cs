using System;
using System.Collections.Generic;

namespace Dev
{
    class Program
    {
        static void Main(string[] args)
        {
            // initializing dicts 
            Dictionary<string, int> critic1 = new Dictionary<string, int>();
            Dictionary<string, int> critic2 = new Dictionary<string, int>();
           
            try {

                string text = System.IO.File.ReadAllText(@"C:\Users\tony\Desktop\x.txt");
                string[] lines = text.Split('\n'); // split into individual lines

                for (int i = 0; i < lines.Length; i++)
                {
                    string movieData = lines[i];
                    string[] fields = movieData.Split(' '); // split into lists of name and ratings

                    string movie = fields[0];
                    int ratingCritic1 = int.Parse(fields[1]); // 1st rating
                    int ratingCritic2 = int.Parse(fields[2]); // 2nd rating

                    critic1.Add(movie, ratingCritic1);
                    critic2.Add(movie, ratingCritic2);

                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

            EuclideanSimilarityScore machineLearn = new EuclideanSimilarityScore(critic1, critic2);
            double getScore = machineLearn.euclideanScore();
            Console.WriteLine("Euclidean Similarity Score = " + getScore);

            // for wait time , ReadLine added.
            Console.ReadLine();
        }
    }

    class EuclideanSimilarityScore
    {
        List<string> universalList;
        List<double> movieRatingDiff;

        Dictionary<string, int> critic1;
        Dictionary<string, int> critic2;
        
        public EuclideanSimilarityScore(Dictionary<string, int> critic1, Dictionary<string, int> critic2)
        {
            this.critic1 = critic1;
            this.critic2 = critic2;

            universalList = new List<string>();

            foreach (string movieName in critic1.Keys)
            {
                if (critic2.ContainsKey(movieName)){ // check if critic2 dict has the movie
                    universalList.Add(movieName);
                }
            }
 

            movieRatingDiff = new List<double>();
        }

        public double euclideanScore()
        {
            double score = 0;
            
            foreach(string movie in universalList)
            {
                int ratingCritic1 = critic1[movie]; 
                int ratingCritic2 = critic2[movie];  // get both critic ratings for the same movie

                double diff = ratingCritic1 - ratingCritic2; // get difference between ratings
                movieRatingDiff.Add(Math.Pow(diff, 2)); // square the difference and add to list
            }

            foreach(double rating in movieRatingDiff)
            {
                score += rating;    // add all ratings
            }

            score = Math.Sqrt(score);

            score = 1 / (1 + score); // inverse for getting higher values for more similar scores

            return score;
        }
    }

}

