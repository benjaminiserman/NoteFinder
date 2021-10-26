using System;
using System.Linq;

namespace NoteFinder
{
    class Program
    {
        //                                          0    1     2    3     4    5    6     7    8     9    10    11
        static readonly string[] sharpNotes = new string[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        static readonly string[] flatNotes = new string[] { "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab", "A", "Bb", "B" };

        static readonly Scale[] scales = new Scale[]
        {
            new("Major", new int[] {0, 2, 4, 5, 7, 9, 11}),
            new("Minor", new int[] {0, 2, 3, 5, 7, 8, 10}),
            new("Ionian", new int[] {0, 2, 4, 5, 7, 9, 11}),
            new("Dorian", new int[] {0, 2, 3, 5, 7, 9, 10}),
            new("Phrygian", new int[] {0, 1, 3, 5, 7, 8, 10}),
            new("Lydian", new int[] {0, 2, 4, 6, 7, 9, 11}),
            new("Mixolydian", new int[] { 0, 2, 4, 5, 7, 9, 10 }),
            new("Aeolian", new int[] { 0, 2, 3, 5, 7, 8, 10 }),
            new("Locrian", new int[] {0, 1, 3, 5, 6, 8, 10}),
            new("Blues", new int[] {0, 3, 5, 6, 7, 10}),
            new("Bebop", new int[] { 0, 2, 4, 5, 7, 9, 10, 11 }),
            new("Pentatonic", new int[] {0, 2, 4, 7, 9}),
        };

        static readonly string[] stopStrings = new string[] { "end", "stop", "exit", "quit", "" };

        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Type in the name of the scale you want.");

                    string scale = Console.ReadLine().Trim().ToLower();

                    bool foundFirstLetter = false;

                    bool keyFinished = false, scaleFinished = false;

                    string working = "";

                    int key = 0, scaleType = 0;

                    bool sharp = true;

                    if (stopStrings.Contains(scale.ToLower())) return;

                    for (int i = 0; i < scale.Length; i++)
                    {
                        if (char.IsWhiteSpace(scale, i))
                        {
                            if (foundFirstLetter)
                            {
                                if (!keyFinished)
                                {
                                    keyFinished = true;

                                    working = $"{char.ToUpper(working[0])}{working[1..].ToLower()}";

                                    if (working.Length > 1)
                                    {
                                        if (working.Length > 2) throw new Exception("Key value too long! should be 1 or 2 characters.");
                                        else if (working[1] == '#') sharp = true;
                                        else if (working[1] == 'b') sharp = false;
                                        else throw new Exception("Unknown sharp/flat symbol");
                                    }
                                    else
                                    {
                                        sharp = true;
                                    }

                                    if (sharp) key = Array.IndexOf(sharpNotes, working);
                                    else key = Array.IndexOf(flatNotes, working);

                                    working = string.Empty;
                                }
                                else if (!scaleFinished)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (!foundFirstLetter)
                            {
                                foundFirstLetter = true;
                            }

                            working += scale[i];
                        }
                    }

                    working = $"{char.ToUpper(working[0])}{working[1..].ToLower()}";

                    for (int i = 0; i < scales.Length; i++)
                    {
                        if (scales[i].name == working)
                        {
                            scaleType = i;
                            break;
                        }
                    }

                    PrintNotes(key, scaleType, sharp);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static void PrintNotes(int key, int scaleType, bool sharp)
        {
            Scale thisScale = scales[scaleType];

            for (int i = 0; i < thisScale.notes.Length; i++)
            {
                int index = thisScale.notes[i] + key;

                while (index >= sharpNotes.Length) index -= sharpNotes.Length;

                if (index == -1)
                {
                    Console.WriteLine("Invalid input.");
                    break;
                }

                if (sharp) Console.WriteLine(sharpNotes[index]);
                else Console.WriteLine(flatNotes[index]);
            }

            Console.WriteLine();
        }
    }
}