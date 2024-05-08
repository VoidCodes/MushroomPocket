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
            /*List<MushroomMaster> mushroomMasters = new List<MushroomMaster>(){
                new MushroomMaster("Daisy", 2, "Peach"),
                new MushroomMaster("Wario", 3, "Mario"),
                new MushroomMaster("Waluigi", 1, "Luigi")
            };*/

            //Use "Environment.Exit(0);" if you want to implement an exit of the console program
            // Test comment from my Mac

            List<Character> characterList = new List<Character>();

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
                // Handle adding mushroom character to the pocket
                Console.WriteLine("Enter the character name");
                string characterName = Console.ReadLine();
                Console.WriteLine("Enter the character HP");
                int hp = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the character Exp");
                int exp = Convert.ToInt32(Console.ReadLine());

                // Instantiate the character
                Character character = new Character();

                switch (characterName)
                {
                    case "Abbas":
                        character = new Abbas(hp, exp);
                        characterList.Add(character);
                        break;
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
                        Console.WriteLine("Invalid character name");
                        break;
                }
                Console.WriteLine($"{characterName} has been added to the pocket");
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
                    foreach (Character character in characters)
                    {
                        Console.WriteLine($"Character Name: {character.CharacterName}");
                        Console.WriteLine($"HP: {character.Hp}");
                        Console.WriteLine($"Exp: {character.Exp}");
                        Console.WriteLine($"Skill: {character.Skill}");
                    }
                }   
            }

            void CheckTransformCharacter()
            {
                // Get all characters in the pocket
                Character[] characters = characterList.ToArray();

                if (characters.Length == 0)
                {
                    Console.WriteLine("No characters in the pocket");
                }
                else
                {
                    foreach (Character character in characters)
                    {
                        Console.WriteLine($"{character.CharacterName} --> {character.TransformTo}");
                    }
                }
            }

            void TransformCharacter()
            {
                Console.WriteLine("Character Transform");
            }
        }
    }
}
