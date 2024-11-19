using System.Collections.Concurrent;

namespace NextStop.Core.Tools;

public static class Fuzzy {
    public static List<Tuple<T, double>> SearchWithScore<T>(string searchString, T[] items,
        Func<T, string?> extractValue,
        double cutoff = 0) {
        Console.WriteLine($"Starting search for {searchString}...");
        List<Tuple<T, double>> results = [];

        foreach (T item in items) {
            //Grab the value and make sure that it is not null
            string? value = extractValue(item);
            if (value == null) {
                continue;
            }

            // Get the score and trow out all the values that are too far off
            // double score = LevenshteinScore(searchString, value);
            double score = WeightedLevenshteinScore(searchString, value);
            if (score < cutoff) {
                continue;
            }

            results.Add(new Tuple<T, double>(item, score));
        }

        return results.OrderByDescending(r => r.Item2).ToList();
    }


    public static List<T> Search<T>(string searchString, T[] items, Func<T, string?> extractValue, double cutoff = 0) {
        List<Tuple<T, double>> results = [];

        foreach (T item in items) {
            // Grab the value and make sure that it is not null
            string? value = extractValue(item);
            if (value == null) {
                continue;
            }

            // Get the score and throw out all the values that are too far off
            double score = WeightedLevenshteinScore(searchString, value);
            if (score < cutoff) {
                continue;
            }

            // Add the item along with its score to the results list
            results.Add(new Tuple<T, double>(item, score));
        }

        // Return just the list of items sorted by the score in descending order
        return results.OrderByDescending(r => r.Item2).Select(r => r.Item1).ToList();
    }


    public static List<T> MultiSearch<T>(string searchString, T[] items, Func<T, string?> extractValue,
        double cutoff = 0) {
        ConcurrentQueue<Tuple<T, double>> results = [];

        Parallel.ForEach(items, item => {
            string? value = extractValue(item);
            if (value == null) {
                return;
            }

            // Get the score and throw out all the values that are too far off
            double score = WeightedLevenshteinScore(searchString, value);
            if (score < cutoff) {
                return;
            }

            // Add the item along with its score to the results list
            results.Enqueue(new Tuple<T, double>(item, score));
        });


        // Return just the list of items sorted by the score in descending order
        // return results.Select(r => r.Item1).ToList();
        return results.OrderByDescending(r => r.Item2).Select(r => r.Item1).ToList();
    }

    public static List<Tuple<T, double>> MultiSearchWithScore<T>(string searchString, T[] items,
        Func<T, string?> extractValue,
        double cutoff = 0) {
        ConcurrentQueue<Tuple<T, double>> results = [];

        Parallel.ForEach(items, item => {
            string? value = extractValue(item);
            if (value == null) {
                return;
            }

            // Get the score and throw out all the values that are too far off
            double score = WeightedLevenshteinScore(searchString, value);
            if (score < cutoff) {
                return;
            }

            // Add the item along with its score to the results list
            results.Enqueue(new Tuple<T, double>(item, score));
        });


        // Return just the list of items sorted by the score in descending order
        // return results.Select(r => r.Item1).ToList();
        return results.OrderByDescending(r => r.Item2).ToList();
    }

    public static int LevenshteinDistance(string s, string t) {
        if (s == t) return 0;
        if (s.Length == 0) return t.Length;
        if (t.Length == 0) return s.Length;
        int[,] distance = new int[s.Length + 1, t.Length + 1];
        for (int i = 0; i <= s.Length; i++) {
            distance[i, 0] = i;
        }

        for (int j = 0; j <= t.Length; j++) {
            distance[0, j] = j;
        }

        for (int i = 1; i <= s.Length; i++) {
            for (int j = 1; j <= t.Length; j++) {
                int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                    distance[i - 1, j - 1] + cost);
            }
        }

        return distance[s.Length, t.Length];
    }

    public static double WeightedLevenshteinScore(string s, string t) {
        string[] tokensS = s.Split(' ');
        string[] tokensT = t.Split(' ');

        double totalScore = 0;
        double totalWeight = 0;

        int minLength = Math.Min(tokensS.Length, tokensT.Length);
        for (int i = 0; i < minLength; i++) {
            double weight = (i == 0) ? 2 : 1;

            double tokenDistance = LevenshteinScore(tokensS[i], tokensT[i]);
            totalScore += tokenDistance * weight;
            totalWeight += weight;
        }

        return totalScore / totalWeight;
    }

    public static double LevenshteinScore(string s, string t) {
        return 1 - (double)LevenshteinDistance(s, t) / Math.Max(s.Length, t.Length);
    }
}