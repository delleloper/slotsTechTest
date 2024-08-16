using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;


public struct Result
{
    public char symbol;
    public int length;
    public string[] pattern;
    public Result(char symbol, int length, string[] pattern)
    {
        this.symbol = symbol;
        this.length = length;
        this.pattern = pattern;
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

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (pattern[i][j] == 'X')
                {
                    lineBuilder.Append(symbols[i][j]);
                }
            }
        }
        string result = lineBuilder.ToString();
        Debug.Log(result);
        char simbol = result[0];
        int matchingSymbols = 0;
        bool stop = false;
        for (int i = 0; i < 5; i++)
        {
            if (result[i] == simbol)
            {
                matchingSymbols += 1;
            }
            else
            {
                stop = true;
                break;
            }
        }

        if (stop || matchingSymbols < 2)
        {
            Debug.Log("LINE NOT FOUND");
        }
        else
        {
            Debug.Log("FOUND LINE of " + matchingSymbols + " #" + simbol);

            results.Add(new Result(simbol, matchingSymbols, pattern));
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

            results.Add(new Result(simbol, matchingSymbols, GenerateWinningPattern(row, matchingSymbols)));
        }
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
