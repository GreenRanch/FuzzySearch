using FuzzySearch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FuzzySearch.Service
{
    public class FuzzySearchHelper : IFuzzySearchHelper
    {
        private const int MaxTolerance = 55;

        public IEnumerable<AccountModel> FindMatchingAccounts(string sourceAccountName, IEnumerable<AccountModel> accounts)
        {
            return accounts?.Where(acc => acc.Name.ToLowerInvariant().
            Contains(sourceAccountName.ToLowerInvariant()) || 
            LevenshteinDistance(sourceAccountName.ToLowerInvariant(), acc.Name.ToLowerInvariant()) <= MaxTolerance)?.
            ToList(); ;
        }


        //Source of below algorithm: https://www.codeproject.com/Articles/13525/Fast-memory-efficient-Levenshtein-algorithm
        private int LevenshteinDistance(String sNew, String sOld)
        {
            int[,] matrix;              // matrix
            int sNewLen = sNew.Length;  // length of sNew
            int sOldLen = sOld.Length;  // length of sOld
            int sNewIdx; // iterates through sNew
            int sOldIdx; // iterates through sOld
            char sNew_i; // ith character of sNew
            char sOld_j; // jth character of sOld
            int cost; // cost

            // Step 1

            if (sNewLen == 0)
            {
                return sOldLen;
            }

            if (sOldLen == 0)
            {
                return sNewLen;
            }

            matrix = new int[sNewLen + 1, sOldLen + 1];

            // Step 2

            for (sNewIdx = 0; sNewIdx <= sNewLen; sNewIdx++)
            {
                matrix[sNewIdx, 0] = sNewIdx;
            }

            for (sOldIdx = 0; sOldIdx <= sOldLen; sOldIdx++)
            {
                matrix[0, sOldIdx] = sOldIdx;
            }

            // Step 3

            for (sNewIdx = 1; sNewIdx <= sNewLen; sNewIdx++)
            {
                sNew_i = sNew[sNewIdx - 1];

                // Step 4

                for (sOldIdx = 1; sOldIdx <= sOldLen; sOldIdx++)
                {
                    sOld_j = sOld[sOldIdx - 1];

                    // Step 5

                    if (sNew_i == sOld_j)
                    {
                        cost = 0;
                    }
                    else
                    {
                        cost = 1;
                    }

                    // Step 6

                    matrix[sNewIdx, sOldIdx] = Minimum(matrix[sNewIdx - 1, sOldIdx] + 1, matrix[sNewIdx, sOldIdx - 1] + 1, matrix[sNewIdx - 1, sOldIdx - 1] + cost);

                }
            }

            // Step 7

            /// Value between 0 - 100
            /// 0==perfect match 100==totaly different
            int max = System.Math.Max(sNewLen, sOldLen);
            return (100 * matrix[sNewLen, sOldLen]) / max;
        }

        private int Minimum(int a, int b, int c)
        {
            int mi = a;

            if (b < mi)
            {
                mi = b;
            }
            if (c < mi)
            {
                mi = c;
            }

            return mi;
        }

    }
}
