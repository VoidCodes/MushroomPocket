using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MushroomPocket.Context;
using MushroomPocket.Models;

namespace MushroomPocket
{
    class Program
    {
        static void Main(string[] args)
        {   
            //MushroomMaster criteria list for checking character transformation availability.   
            /*************************************************************************
                PLEASE DO NOT CHANGE THE CODES FROM LINE 15-19
            *************************************************************************/ 
            List<MushroomMaster> mushroomMasters = new List<MushroomMaster>(){
                new MushroomMaster("Daisy", 2, "Peach"),
                new MushroomMaster("Wario", 3, "Mario"),
                new MushroomMaster("Waluigi", 1, "Luigi")
            };

            //Use "Environment.Exit(0);" if you want to implement an exit of the console program

            //List of characters in the pocket
            List<Character> characterList = new List<Character>();

            //Dictionary to keep track of the number of characters that can be transformed
            Dictionary<string, int> transformCriteria = new Dictionary<string, int>();

            Console.Title = "Mushroom Pocket";
            Console.WriteLine("**************************************************");
            Console.WriteLine("Welcome to Mushroom Pocket App");
            Console.WriteLine("**************************************************");

            while (true)
            {
                string[] options = 
                { 
                    "Add Mushroom's character to my pocket", 
                    "List character(s) in my Pocket", 
                    "Check if I can transform my characters", 
                    "Transform character(s)" 
                };

                for (int i = 0; i < options.Length; i++)
                {
                     Console.WriteLine($"({i + 1}). {options[i]}");
                }

                Console.Write("Please only enter [1,2,3,4] or q to quit: ");

                string option = Console.ReadLine();
                if (option != null)
                {
                    switch (option.ToLower())
                    {
                        case "q":
                            //Environment.Exit(0);
                            Console.WriteLine("Thank you for using Mushroom Pocket App");
                            Console.WriteLine("Exiting the program...");
                            Thread.Sleep(2000);
                            Environment.Exit(0);
                            break;
                        case "1":
                            AddMushroomCharacter();
                            break;
                        case "2":
                            ListCharacters();
                            break;
                        case "3":
                            CheckTransformCharacter();
                            break;
                        case "4":
                            TransformCharacter();
                            break;
                        
                        default:
                            Console.WriteLine("Invalid option, please enter a valid option");
                            break;
                    }
                }
            }

            // Functions
            void AddMushroomCharacter()
            {
                while (true)
                {
                    try
                    {
                        // Handle adding mushroom character to the pocket
                        Console.Write("Enter the character name: ");
                        string characterName = Console.ReadLine();
                        Console.Write("Enter the character HP: ");
                        int hp = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter the character Exp: ");
                        int exp = Convert.ToInt32(Console.ReadLine());

                        // Instantiate the character
                        Character character = new Character(hp, exp);

                        switch (characterName)
                        {
                            /*case "Abbas":
                                character = new Abbas(hp, exp);
                                characterList.Add(character);
                                break;*/
                            case "Waluigi":
                                character = new Waluigi(hp, exp);
                                characterList.Add(character);
                                //character.Skill = "Agility";
                                break;
                            case "Wario":
                                character = new Wario(hp, exp);
                                characterList.Add(character);
                                //character.Skill = "Strength";
                                break;
                            case "Daisy":
                                character = new Daisy(hp, exp);
                                characterList.Add(character);
                                //character.Skill = "Leadership";
                                break;
                            default:
                                throw new Exception("Invalid character name");
                        }

                        if (transformCriteria.ContainsKey(characterName))
                        {
                            transformCriteria[characterName]++;
                        }
                        else
                        {
                            transformCriteria.Add(characterName, 1);
                        }

                        using (var context = new Dbcontext())
                        {
                            context.Character.Add(character);
                            context.SaveChanges();
                        }

                        Console.WriteLine($"{characterName} has been added to the pocket");
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input, please enter a valid input");
                        //continue;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        //continue;
                    }
                }
            }

            void ListCharacters()
            {
                using (var context = new Dbcontext())
                {
                    // Get all characters in the pocket from the database
                    var characterListNew = context.Character.ToList();
                    // Check if there are any characters in the pocket
                    if (characterListNew.Count == 0)
                    {
                        Console.WriteLine("No characters in the pocket");
                        Console.WriteLine("\n");
                        return;
                    }

                    Console.WriteLine("--------------------");

                    var characterSort = characterListNew.OrderByDescending(c => c.Hp);

                    // Show transformed characters first
                    foreach (MushroomMaster master in mushroomMasters)
                    {
                        if (transformCriteria.ContainsKey(master.TransformTo) && transformCriteria[master.TransformTo] > 0)
                        {
                            // Find the transformed character in the list (you might have multiple)
                            foreach (Character character in characterSort.Where(c => c.CharacterName == master.TransformTo))
                            {
                                Console.WriteLine($"Character Name: {character.CharacterName}");
                                Console.WriteLine($"HP: {character.Hp}");
                                Console.WriteLine($"Exp: {character.Exp}");
                                Console.WriteLine($"Skill: {character.Skill}");
                                Console.WriteLine("--------------------");
                            }
                        }
                    }

                    // Show non-transformed characters
                    foreach (Character character in characterSort)
                    {
                        // Skip transformed characters (they are already displayed)
                        if (mushroomMasters.Any(m => m.TransformTo == character.CharacterName))
                        {
                            continue;
                        }

                        Console.WriteLine($"Character Name: {character.CharacterName}");
                        Console.WriteLine($"HP: {character.Hp}");
                        Console.WriteLine($"Exp: {character.Exp}");
                        Console.WriteLine($"Skill: {character.Skill}");
                        Console.WriteLine("--------------------");
                    }
                }
            }

            void CheckTransformCharacter()
            {
                using (var context = new Dbcontext())
                {
                    // Get all characters in the pocket
                    Character[] characters = context.Character.ToArray();

                    if (characters.Length == 0)
                    {
                        Console.WriteLine("No characters in the pocket to transform");
                        Console.WriteLine("\n");
                    }
                    else
                    {
                        // Flag to check if any character can be transformed
                        bool canTransform = false;

                        foreach (MushroomMaster master in mushroomMasters)
                        {
                            // Count the number of characters that can be transformed
                            int numCharacters = context.Character.Count(c => c.CharacterName == master.Name);

                            if (numCharacters >= master.NoToTransform)
                            {
                                Console.WriteLine($"{master.Name} --> {master.TransformTo}");
                                canTransform = true;
                            }
                        }
                        if (!canTransform)
                        {
                            Console.WriteLine("No characters can be transformed, please add more characters");
                        }
                    }
                }
            }

            void TransformCharacter()
            {
                // Get all characters in the pocket
                Character[] characters = characterList.ToArray();
                if (characters.Length == 0)
                {
                    Console.WriteLine("No characters in the pocket to transform");
                    Console.WriteLine("\n");
                }
                else
                {
                    // Flag to check if any transformation has been done
                    bool hasTransformed = false;

                    foreach (MushroomMaster master in mushroomMasters)
                    {
                        if (transformCriteria.ContainsKey(master.Name) &&  transformCriteria[master.Name] >= master.NoToTransform)
                        {
                            // Calculate how many transformations can be done
                            int numTransformations = transformCriteria[master.Name] / master.NoToTransform;

                            // Transform the characters
                            for (int i = 0; i < numTransformations; i++)
                            {
                                // Find and remove the required number of characters for transformation
                                for (int j = 0; j < master.NoToTransform; j++)
                                {
                                    Character characterToRemove = characterList.FirstOrDefault(c => c.CharacterName == master.Name);
                                    if (characterToRemove != null)
                                    {
                                        characterList.Remove(characterToRemove);
                                    }
                                }

                                // Create and add the transformed character
                                Character newCharacter = new Character(100, 0);
                                newCharacter.CharacterName = master.TransformTo;

                                switch (master.TransformTo)
                                {
                                    case "Peach":
                                        newCharacter.Skill = "Magic Abilities";
                                        break;
                                    case "Mario":
                                        newCharacter.Skill = "Combat Skills";
                                        break;
                                    case "Luigi":
                                        newCharacter.Skill = "Precision and Accuracy";
                                        break;
                                    default:
                                        newCharacter.Skill = "Transformed"; // Default skill
                                        break;
                                }

                                characterList.Add(newCharacter);
                                Console.WriteLine($"{master.Name} has been transformed to {master.TransformTo}");
                            }

                            // Update character counts
                            transformCriteria[master.Name] %= master.NoToTransform;

                            if (transformCriteria.ContainsKey(master.TransformTo))
                            {
                                transformCriteria[master.TransformTo] += numTransformations;
                            }
                            else
                            {
                                transformCriteria.Add(master.TransformTo, numTransformations);
                            }

                            hasTransformed = true;
                        }
                    }

                    if (!hasTransformed)
                    {
                        Console.WriteLine("No characters can be transformed, please add more characters");
                    }
                }   
            }
        }
    }
}
