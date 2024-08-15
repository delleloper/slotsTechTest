using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PatternChecker
{

    private static string[] patternA = { "XXXXX", "00000", "00000" };
    private static string[] patternB = { "00000", "XXXXX", "00000" };
    private static string[] patternC = { "00000", "00000", "XXXXX" };
    // private static string[] patternD = { "0X0X0", "00000", "X0X0X" };
    // private static string[] patternE = { "X0X0X", "00000", "0X0X0" };
    // private static string[] patternF = { "X000X", "0X0X0", "00X00" };
    // private static string[] patternG = { "00X00", "0X0X0", "X000X" };
    // private static string[] patternH = { "XX000", "00X00", "000XX" };
    // private static string[] patternI = { "000XX", "00X00", "XX000" };

    private List<string[]> allPatterns = new List<string[]> {
        patternA,patternB,patternC,//patternD,patternE,patternF,patternG,patternH, patternI
    };
    private bool matched;
    private int matchingSymbols;
    private char symbolMatching;
    // public int CheckLinePattern(string[] symbols, string[] PatternToCheck)
    // {
    //     matchingSymbols = 0;
    //     symbolMatching = '0';
    //     matched = false;

    //     for (int i = 0; i < symbols.Length; i++)
    //     {
    //         string currentRow = symbols[i];
    //         string currentPaternRow = PatternToCheck[i];

    //         Debug.Log("-----------------Start Section" + (i + 1) + "---------------------");

    //         Debug.Log("currentRow: " + currentRow);
    //         Debug.Log("currentPaternToCHeck: " + currentPaternRow);

    //         symbolMatching = currentRow[0];
    //         currentPaternRow = currentPaternRow.Replace('X', symbolMatching);

    //         Debug.Log("currentPaternRow: " + currentPaternRow);

    //         for (int j = 0; j < currentPaternRow.Length; j++)
    //         {

    //             if (currentPaternRow[j] == currentRow[j])
    //             {
    //                 matchingSymbols++;
    //             }
    //             else
    //             {
    //                 if (matchingSymbols < 2)
    //                 {
    //                     Debug.Log("-----------------END Section" + (i + 1) + "---------------------");
    //                     return matchingSymbols;

    //                 }
    //             }
    //         }
    //         Debug.Log("-----------------END Section" + (i + 1) + "---------------------");
    //     }
    //     Debug.Log("found " + symbolMatching + " " + matchingSymbols + " times");

    //     return matchingSymbols;

    // }

    public bool CheckLinePattern(string[] symbols, int row)
    {
        bool stop = false;
        var simbol = symbols[row][0];
        int matchingSymbols = 0;
        for (int x = 0; x < 5; x++)
        {
            if (symbols[row][x] == simbol)
            {
                matchingSymbols += 1;
            }
            else
            {
                stop = true;
                break;
            }
        }

        if (stop && matchingSymbols < 2)
        {
            Debug.Log("LINE NOT FOUND");
        }
        else
        {
            Debug.Log("FOUND LINE of " + matchingSymbols + " #" + simbol);
        }
        return matchingSymbols > 2;
    }


    // public int CheckAllPatterns(string[] symbols)
    // {
    //     int Count = 0;
    //     foreach (string[] pattern in allPatterns)
    //     {
    //         if (CheckLinePattern(symbols, pattern) > 2)
    //         {
    //             Debug.Log("Match");

    //         }
    //     }

    //     return 0;
    // }
}
