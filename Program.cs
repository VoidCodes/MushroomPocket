using System;
using System.Collections.Generic;
using System.Linq;
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

            List<Character> characterList = new List<Character>();

            Dictionary<string, int> transformCriteria = new Dictionary<string, int>();

            Console.Title = "Mushroom Pocket";
            while (true)
            {
                Console.WriteLine("**************************************************");
                Console.WriteLine("Welcome to Mushroom Pocket App");
                Console.WriteLine("**************************************************");
                Console.WriteLine("Please only enter [1,2,3,4] or Q to quit:");
                
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

                string option = Console.ReadLine();
                if (option != null)
                {
                    switch (option)
                    {
                        case "Q":
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
                                break;
                            case "Wario":
                                character = new Wario(hp, exp);
                                characterList.Add(character);
                                break;
                            case "Daisy":
                                character = new Daisy(hp, exp);
                                characterList.Add(character);
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
                // List all characters in the pocket
                Character[] characters = characterList.ToArray();
                if (characters.Length == 0)
                {
                    Console.WriteLine("No characters in the pocket");
                }
                else
                {
                    Console.WriteLine("--------------------");
                    foreach (Character character in characters)
                    {
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
                // Get all characters in the pocket
                Character[] characters = characterList.ToArray();

                if (characters.Length == 0)
                {
                    Console.WriteLine("No characters in the pocket to transform");
                }
                else
                {
                    foreach (MushroomMaster master in mushroomMasters)
                    {
                        if (transformCriteria.ContainsKey(master.Name) &&
                            transformCriteria[master.Name] >= master.NoToTransform)
                        {
                            Console.WriteLine($"{master.Name} --> {master.TransformTo}");
                        }
                        // TODO: Fix bug where the message is displayed multiple times
                        else
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
                }
                else
                {
                    foreach (MushroomMaster master in mushroomMasters)
                    {
                        if (transformCriteria.ContainsKey(master.Name) &&  transformCriteria[master.Name] >= master.NoToTransform)
                        {
                            // Transform the character
                            foreach (Character character in characters)
                            {
                                if (character.CharacterName == master.Name)
                                {
                                    character.CharacterName = master.TransformTo;
                                    character.Hp = 100;
                                    character.Exp = 0;

                                    // Assign a unique skill based on the transformed character
                                    switch (master.TransformTo)
                                    {
                                        case "Peach":
                                            character.Skill = "Magic Abilities";
                                            break;
                                        case "Mario":
                                            character.Skill = "Combat Skills";
                                            break;
                                        case "Luigi":
                                            character.Skill = "Precision and Accuracy";
                                            break;
                                        // Add more cases for other transformed characters and their skills
                                        default:
                                            character.Skill = "Transformed"; // Default skill
                                            break;
                                    }

                                    Console.WriteLine($"{master.Name} has been transformed to {master.TransformTo}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No characters can be transformed, please add more characters");
                        }
                    }
                }   
            }
        }
    }
}
