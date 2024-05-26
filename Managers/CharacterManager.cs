using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MushroomPocket.Context;
using MushroomPocket.Models;
using Microsoft.EntityFrameworkCore;

namespace MushroomPocket.Managers
{
    public class CharacterManager
    {
        //Dictionary to keep track of the number of characters that can be transformed
        Dictionary<string, int> transformCriteria = new Dictionary<string, int>();

        // Main Functions
        public void AddMushroomCharacter()
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
                            break;
                        case "Protobean":
                            character = new Protobean(hp, exp);
                            break;
                        case "Waluigi":
                            character = new Waluigi(hp, exp);
                            break;
                        case "Wario":
                            character = new Wario(hp, exp);
                            break;
                        case "Daisy":
                            character = new Daisy(hp, exp);
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
                    Console.ResetColor();
                    break;
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input, please enter a valid input");
                    Console.ResetColor();
                    //continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //continue;
                }
            }
        }

        public void ListCharacters()
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

        // Update character
        public void UpdateCharacter()
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
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Character not found");
                            Console.ResetColor();
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
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid choice, please enter a valid choice");
                                Console.ResetColor();
                                return;
                        }

                        // Save changes to the database
                        context.SaveChanges();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{characterName}'s stats have been updated");
                        Console.ResetColor();
                        break;
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input, please enter a valid input");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}