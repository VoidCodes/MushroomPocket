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
            //Start your assignment 1 requirements below.
            // Test data

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
                            Console.WriteLine("Enter the character you want to check for transformation");
                            string characterToCheck = Console.ReadLine();
                            CheckTransformCharacter(characterToCheck);
                            break;
                        case "4":
                            Console.WriteLine("Enter the character you want to transform");
                            string characterToTransform = Console.ReadLine();
                            TransformCharacter(characterToTransform);
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
                Console.WriteLine("Add Mushroom Character");
                Console.WriteLine("Enter the character name");
                string characterName = Console.ReadLine();
                Console.WriteLine("Enter the character HP");
                int hp = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the character Exp");
                int exp = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the character Skill");
                string skill = Console.ReadLine();

                // Create a new character object
                Character character = new Character
                {
                    CharacterName = characterName,
                    Hp = hp,
                    Exp = exp,
                    Skill = skill
                };
                characterList.Add(character);
                Console.WriteLine("Character added to the pocket");
            }

            void ListCharacters()
            {
                Console.WriteLine("List Character");
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

            void CheckTransformCharacter(string character)
            {
                Console.WriteLine("checkTransformCharacter");
            }

            void TransformCharacter(string character)
            {
                Console.WriteLine("Character Transform");
            }
        }
    }
}
