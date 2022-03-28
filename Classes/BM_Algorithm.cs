namespace SemesterWork;

internal sealed class BM_Algorithm
{
    /// <summary>
    /// BM Algorithm
    /// </summary>
    /// <param name="source">string where "find" is searched for</param>
    /// <param name="find">string to be found</param>
    /// <returns>array[index of occurence, spended iterations]</returns>
    public static int[] BM_Search(string source, string find)
    {
        int iteratioins = 0;

        int sourceLength = source.Length;
        int findLength = find.Length;

        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(find) || findLength > sourceLength) return new int[] { -1, iteratioins };

        if (source == find || source[0..findLength] == find) return new int[] { 0, iteratioins };

        var badchar = Enumerable.Repeat(-1, 256).ToArray();

        for (var i = 0; i < findLength; i++)
            badchar[find[i]] = i;

        int shift = 0;
        while (shift <= sourceLength - findLength)
        {
            iteratioins++;
            int j = findLength - 1;

            while (j >= 0 && find[j] == source[shift + j]) { j--; iteratioins++; }

            if (j < 0) return new int[] { shift, iteratioins };

            shift += Max(1, j - badchar[source[shift + j]]);
        }
        return new int[] { -1, iteratioins };

        int Max(int a, int b) => a > b ? a : b;

        #region Variation with GetAllOccurances
        /*      //Find all occurences of "find":
         *      Instead returning the index of first occurance, we can return the list of valid indexes
         *      
                var occurences = new List<int>();

                ******

                if (j < 0)
                {
                    occurences.Add(shift);
                    shift += (shift + findLength < sourceLength) ? findLength - badchar[source[shift + findLength]] : 1;
                }

                else shift += Max(1, j - badchar[source[shift + j]]);

                ******

                return occurences;

         */
        #endregion
    }
}

