﻿namespace NoteFinder;
using System;
using System.Linq;

internal class Program
{
    //   IDs for each note                                        0    1     2    3     4    5    6     7    8     9    10    11
    private static readonly string[] _sharpNotes = new string[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
    private static readonly string[] _flatNotes = new string[] { "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab", "A", "Bb", "B" };
    private static readonly Scale[] _scales = new Scale[]
    {
            new("Major", new int[] { 0, 2, 4, 5, 7, 9, 11 }),
            new("Minor", new int[] { 0, 2, 3, 5, 7, 8, 10 }),
            new("Ionian", new int[] { 0, 2, 4, 5, 7, 9, 11 }),
            new("Dorian", new int[] { 0, 2, 3, 5, 7, 9, 10 }),
            new("Phrygian", new int[] { 0, 1, 3, 5, 7, 8, 10 }),
            new("Lydian", new int[] { 0, 2, 4, 6, 7, 9, 11 }),
            new("Mixolydian", new int[] { 0, 2, 4, 5, 7, 9, 10 }),
            new("Aeolian", new int[] { 0, 2, 3, 5, 7, 8, 10 }),
            new("Locrian", new int[] { 0, 1, 3, 5, 6, 8, 10 }),
            new("Blues", new int[] { 0, 3, 5, 6, 7, 10 }),
            new("Bebop", new int[] { 0, 2, 4, 5, 7, 9, 10, 11 }),
            new("Pentatonic", new int[] { 0, 2, 4, 7, 9 }),
    };
    private static readonly string[] _stopStrings = new string[] { "end", "stop", "exit", "quit", "" };

    private static void Main()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Type in the name of the scale you want.");

                string input = Console.ReadLine().ToLower();

                if (_stopStrings.Contains(input)) return;

                string[] split = input.Split();

                if (split[0].Length > 2) throw new Exception("Key value too long! should be 1 or 2 characters.");
                if (!"abcdefg".Contains(split[0][0])) throw new Exception("Invalid key!");
                if (split[0].Length == 2 && !"#b".Contains(split[0][1])) throw new Exception("Invalid key! Second character must be # or b.");

                bool? isSharp = split[0].Length == 1
                    ? null
                    : split[0][1] == '#';

                int key = isSharp == true
                    ? Array.FindIndex(_sharpNotes, x => x.ToLower() == split[0])
                    : Array.FindIndex(_flatNotes, x => x.ToLower() == split[0]);

                Scale scale = _scales.First(x => x.name.ToLower() == split[1]);

                PrintNotes(key, scale, isSharp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    private static void PrintNotes(int key, Scale scale, bool? isSharp)
    {
        for (int i = 0; i < scale.notes.Length; i++)
        {
            int index = scale.notes[i] + key;

            while (index >= _sharpNotes.Length) index -= _sharpNotes.Length;

            if (index == -1)
            {
                Console.WriteLine("Invalid input.");
                break;
            }

            Console.WriteLine(isSharp switch
            {
                true => _sharpNotes[index],
                false => _flatNotes[index],
                null => 
                    _sharpNotes[index] == _flatNotes[index]
                        ? _sharpNotes[index]
                        : $"{_sharpNotes[index]}/{_flatNotes[index]}",
            });
        }

        Console.WriteLine();
    }
}
