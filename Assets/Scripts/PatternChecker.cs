using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;


public struct Result
{
    public char symbol;
    public int length;
    public string[] pattern;
    public int score;
    public Result(char symbol, int length, string[] pattern, int score)
    {
        this.symbol = symbol;
        this.length = length;
        this.pattern = pattern;
        this.score = score;

    }
}


public class PatternChecker
{
    public List<Result> results = new List<Result>();
    private static string[] patternA = { "0X0X0", "00000", "X0X0X" };
    private static string[] patternB = { "X0X0X", "00000", "0X0X0" };
    private static string[] patternC = { "X000X", "0X0X0", "00X00" };
    private static string[] patternD = { "00X00", "0X0X0", "X000X" };
    private static string[] patternE = { "XX000", "00X00", "000XX" };
    private static string[] patternF = { "000XX", "00X00", "XX000" };
    //You can add new patterns and add them to the list!
    private List<string[]> allPatterns = new List<string[]> {
        patternA,patternB,patternC,patternD,patternE, patternF
    };

    public List<Result> CheckAllPatterns(string[] symbols)
    {
        CheckLines(symbols);
        foreach (string[] pattern in allPatterns)
        {
            CheckLinePattern(symbols, pattern);
        }
        if (results.Count > 0)
        {
            Debug.Log("fgffgdff");
        }
        return results;
    }
    public void CheckLinePattern(string[] symbols, string[] pattern)
    {
        StringBuilder lineBuilder = new StringBuilder();
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (pattern[j][i] == 'X')
                {
                    lineBuilder.Append(symbols[j][i]);
                }
            }
        }
        string result = lineBuilder.ToString();
        char simbol = result[0];
        int matchingSymbols = 0;

        for (int i = 0; i < 5; i++)
        {
            if (result[i] == simbol)
            {
                matchingSymbols += 1;
            }
            else
            {
                break;
            }
        }


        if (matchingSymbols >= 2)
        {

            List<string> newPattern = new List<string>();
            foreach (string item in pattern)
            {
                string newLine = item.Substring(0, matchingSymbols);
                newLine += new string('0', 5 - newLine.Length);
                newPattern.Add(newLine);
            }

            Debug.Log(result);
            Debug.Log(string.Join("|", newPattern));
            Debug.Log("FOUND LINE of " + matchingSymbols + " #" + simbol);
            results.Add(new Result(simbol, matchingSymbols, newPattern.ToArray(), CalculateScore(simbol, matchingSymbols)));
        }
    }



    public List<Result> CheckLines(string[] symbols)
    {
        results = new List<Result>();
        for (int i = 0; i < 3; i++)
        {
            CheckLine(symbols, i);
        }
        return results;
    }


    public void CheckLine(string[] symbols, int row)
    {
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
                break;
            }
        }

        if (matchingSymbols >= 2)
        {

            Debug.Log("FOUND LINE of " + matchingSymbols + " #" + simbol);

            results.Add(new Result(simbol, matchingSymbols, GenerateWinningPattern(row, matchingSymbols), CalculateScore(simbol, matchingSymbols)));
        }
    }

    Dictionary<char, int[]> symbolScores = new Dictionary<char, int[]>
    {
        { '1', new[] { 25, 50, 75, 100 } },
        { '2', new[] { 10, 20, 30, 60 } },
        { '3', new[] { 5, 10, 20, 50 } },
        { '4', new[] { 5, 10, 20, 40 } },
        { '5', new[] { 5, 10, 15, 30 } },
        { '6', new[] { 2, 5, 10, 20 } },
        { '7', new[] { 1, 2, 5, 10 } }
    };


    int CalculateScore(char symbol, int amount)
    {
        return symbolScores[symbol][amount - 2];
    }



    string[] GenerateWinningPattern(int row, int matchingSymbols)
    {
        string[] pattern = { "00000", "00000", "00000" };

        char[] rows = pattern[row].ToCharArray();
        int replaced = 0;

        for (int j = 0; j < rows.Length && replaced < matchingSymbols; j++)
        {
            if (rows[j] == '0')
            {
                rows[j] = 'X';
                replaced++;
            }
        }

        pattern[row] = new string(rows);

        return pattern;
    }



}
