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
            // Mega Transformation
            //MushroomMaster criteria list for checking character transformation availability.   
            /*************************************************************************
                PLEASE DO NOT CHANGE THE CODES FROM LINE 15-19
            *************************************************************************/ 
            List<MushroomMaster> mushroomMasters = new List<MushroomMaster>(){
                new MushroomMaster("Daisy", 2, "Peach"),
                new MushroomMaster("Wario", 3, "Mario"),
                new MushroomMaster("Waluigi", 1, "Luigi"),
                new MushroomMaster("Abbas", 4, "Shawarma Man")
            };

            //Use "Environment.Exit(0);" if you want to implement an exit of the console program

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
                    "Transform character(s)" ,
                    "Update character"
                };

                for (int i = 0; i < options.Length; i++)
                {
                     Console.WriteLine($"({i + 1}). {options[i]}");
                }

                Console.Write("Please only enter [1,2,3,4,5] or q to quit: ");

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
                        case "5":
                            UpdateCharacter();
                            break;
                        
                        default:
                            Console.WriteLine("Invalid option, please enter a valid option");
                            break;
                    }
                }
            }

            // Main Functions
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
                            case "Abbas":
                                character = new Abbas(hp, exp);
                                //characterList.Add(character);
                                break;
                            case "Waluigi":
                                character = new Waluigi(hp, exp);
                                //characterList.Add(character);
                                //character.Skill = "Agility";
                                break;
                            case "Wario":
                                character = new Wario(hp, exp);
                                //characterList.Add(character);
                                //character.Skill = "Strength";
                                break;
                            case "Daisy":
                                character = new Daisy(hp, exp);
                                //characterList.Add(character);
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

                        Console.ForegroundColor = ConsoleColor.Green;
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
                    /*foreach (MushroomMaster master in mushroomMasters)
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
                    }*/
                    foreach (Character character in characterSort)
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
                using (var context = new Dbcontext())
                {
                    Character[] characters = context.Character.ToArray();
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
                            // Count the number of characters that can be transformed
                            int numCharacters = context.Character.Count(c => c.CharacterName == master.Name);

                            if (numCharacters >= master.NoToTransform)
                            {
                                // Find and remove the required number of characters for transformation
                                var charactersToRemove = context.Character
                                .Where(c => c.CharacterName == master.Name)
                                .Take(master.NoToTransform)
                                .ToList();

                                foreach (Character characterToRemove in charactersToRemove)
                                {
                                    context.Character.Remove(characterToRemove);
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

                                context.Character.Add(newCharacter);
                                context.SaveChanges();

                                Console.WriteLine($"{master.Name} has been transformed to {master.TransformTo}");
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

            // Update character
            void UpdateCharacter() 
            {
                while (true) 
                {
                    try 
                    {
                        using (var context = new Dbcontext()) 
                        {
                            Console.Write("Enter the character name to update: ");
                            string characterName = Console.ReadLine();
                        
                            Character characterToUpdate = context.Character.FirstOrDefault(c => c.CharacterName == characterName);
                            if (characterToUpdate == null) 
                            {
                                Console.WriteLine("Character not found");
                                break;
                            }

                            Console.WriteLine("What do you want to update?");
                            Console.WriteLine("1. HP");
                            Console.WriteLine("2. EXP");
                            Console.WriteLine("3. Both"); 

                            Console.Write("Enter your choice: ");

                            string choice = Console.ReadLine();

                            switch (choice) 
                            {
                                case "1":
                                    Console.Write("Enter the new HP: ");
                                    characterToUpdate.Hp = Convert.ToInt32(Console.ReadLine());
                                    break;
                                case "2":
                                    Console.Write("Enter the new EXP: ");
                                    characterToUpdate.Exp = Convert.ToInt32(Console.ReadLine());
                                    break;
                                case "3":
                                    Console.Write("Enter the new HP: ");
                                    characterToUpdate.Hp = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("Enter the new EXP: ");
                                    characterToUpdate.Exp = Convert.ToInt32(Console.ReadLine());
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice, please enter a valid choice");
                                    return;
                            }

                            // Save changes to the database
                            context.SaveChanges();

                            Console.WriteLine($"{characterName}'s stats have been updated");
                            break;
                        }
                    } 
                    catch (FormatException) 
                    {
                        Console.WriteLine("Invalid input, please enter a valid input");
                    } 
                    catch (Exception ex) 
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}