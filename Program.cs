using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MushroomPocket.Context;
using MushroomPocket.Models;
using MushroomPocket.Managers;
using Microsoft.EntityFrameworkCore;

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
                new MushroomMaster("Waluigi", 1, "Luigi"),
                new MushroomMaster("Abbas", 3, "Shawarma Man"),
                new MushroomMaster("Primagen", 2, "Protogen"),
            };

            //Use "Environment.Exit(0);" if you want to implement an exit of the console program

            // Note: Since Checking Transformation Eligibility and Transforming 
            // requires MushroomMaster which is in this file, it is implemented here.

            // Instantiate Manager Classes here
            CharacterManager cm = new CharacterManager();
            InventoryManager im = new InventoryManager();
            BattleManager bm = new BattleManager();

            Console.Title = "Mushroom Pocket";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("**************************************************");
            Console.WriteLine("Welcome to Mushroom Pocket App");
            Console.WriteLine("**************************************************");
            Console.ResetColor();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("CHARACTER MANAGEMENT:");
                Console.ResetColor();
                Console.WriteLine("1. Add Mushroom Character");
                Console.WriteLine("2. List Characters");
                Console.WriteLine("3. Check Transformation Eligibility");
                Console.WriteLine("4. Transform Character");
                Console.WriteLine("5. Update Character");
                Console.WriteLine("--------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("ITEM MANAGEMENT:");
                Console.ResetColor();
                Console.WriteLine("6. Add Item");
                Console.WriteLine("7. Add Item to Inventory");
                Console.WriteLine("8. View Character Inventory");
                Console.WriteLine("9.  Use Item");
                Console.WriteLine("--------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("BATTLE SYSTEM:");
                Console.ResetColor();
                Console.WriteLine("10. Start Battle");

                Console.Write("Please only enter [1,2,3,4,5,6,7,8,9,10] or q to quit: ");

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
                            cm.AddMushroomCharacter();
                            break;
                        case "2":
                            cm.ListCharacters();
                            break;
                        case "3":
                            CheckTransformCharacter();
                            break;
                        case "4":
                            TransformCharacter();
                            break;
                        case "5":
                            cm.UpdateCharacter();
                            break;
                        case "6":
                            im.AddItem();
                            break;
                        case "7":
                            im.AddItemToInventory();
                            break;
                        case "8":
                            im.ViewCharacterInventory();
                            break;
                        case "9":
                            im.UseItemFromMenu();
                            break;
                        case "10":
                            bm.StartBattle();
                            break;

                        default:
                            Console.WriteLine("Invalid option, please enter a valid option");
                            break;
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
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No characters can be transformed, please add more characters");
                            Console.ResetColor();
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
                                    case "Shawarma Man":
                                        newCharacter.Skill = "BEBSI";
                                        break;
                                    case "Protogen":
                                        newCharacter.Skill = "Advanced Technology";
                                        break;
                                    default:
                                        newCharacter.Skill = "Transformed"; // Default skill
                                        break;
                                }

                                context.Character.Add(newCharacter);
                                context.SaveChanges();

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"{master.Name} has been transformed to {master.TransformTo}");
                                Console.ResetColor();
                                hasTransformed = true;
                            }
                        }

                        if (!hasTransformed)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No characters can be transformed, please add more characters");
                            Console.ResetColor();
                        }
                    }
                }
            }
        }
    }
}