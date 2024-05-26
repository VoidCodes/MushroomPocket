using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MushroomPocket.Context;
using MushroomPocket.Models;
using Microsoft.EntityFrameworkCore;

namespace MushroomPocket.Managers
{
    public class BattleManager
    {
        InventoryManager im = new InventoryManager();
        public void StartBattle()
        {
            using (var context = new Dbcontext())
            {
                try
                {
                    // Get character name
                    Console.Write("Enter the name of the first character: ");
                    string character1Name = Console.ReadLine();
                    Console.Write("Enter the name of the second character: ");
                    string character2Name = Console.ReadLine();

                    // Find characters in database
                    Character character1 = context.Character.FirstOrDefault(c => c.CharacterName == character1Name);
                    Character character2 = context.Character.FirstOrDefault(c => c.CharacterName == character2Name);

                    if (character1 == null || character2 == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception("Character not found.");
                    }

                    // Check if character can battle
                    if (character1.Hp <= 0 || character2.Hp <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception("Character is unable to battle.");
                    }

                    // Battle logic
                    int round = 1;
                    Random random = new Random();
                    while (character1.Hp > 0 && character2.Hp > 0)
                    {
                        Console.WriteLine("\n--------------------");
                        Console.WriteLine($"Round: {round}"); // Track the round number
                        Console.WriteLine("--------------------");

                        // 2.1. Player 1 Turn
                        Console.WriteLine($"\n{character1.CharacterName}'s turn (HP: {character1.Hp})");
                        Console.WriteLine("1. Attack");
                        Console.WriteLine("2. Use Item");
                        Console.WriteLine("3. Defend");
                        Console.Write("Enter your choice: ");

                        string choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "1":
                                Attack(character1, character2);
                                break;
                            case "2":
                                // Implement Item Usage Logic
                                im.UseItem(character1, null); // Use item on self
                                break;
                            case "3":
                                Defend(character1);
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please enter a valid option.");
                                break;
                        }

                        // Check if Character 2 is defeated after Player 1's turn
                        if (character2.Hp <= 0)
                        {
                            break; // Exit the loop if Character 2 is defeated
                        }

                        // 2.2. Player 2 (AI) Turn
                        Console.WriteLine($"\n{character2.CharacterName}'s turn (HP: {character2.Hp})");

                        // P2 Turn: Random AI Choice
                        int aiChoice = random.Next(1, 4); // Choose a random action (1-3)

                        switch (aiChoice)
                        {
                            case 1:
                                Attack(character2, character1);
                                break;
                            case 2:
                                // 1. Check AI Inventory
                                var aiInventory = context.Inventory
                                    .Where(i => i.CharacterId == character2.Id)
                                    .Include(i => i.Items)
                                    .ToList();

                                // 2. Use a random item from AI inventory
                                if (aiInventory.Count > 0)
                                {
                                    Inventory aiItem = aiInventory[random.Next(0, aiInventory.Count)];
                                    Console.WriteLine($"{character2.CharacterName} used {aiItem.Items.ItemName}.");
                                    im.UseItem(character2, aiItem.Items); // Use a random item
                                }
                                else
                                {
                                    Console.WriteLine($"{character2.CharacterName} has no items to use.");
                                }
                                break;
                            case 3:
                                Defend(character2);
                                break;
                        }

                        // Display Round Results
                        Console.WriteLine("\n--- Round Results ---");
                        Console.WriteLine($"{character1.CharacterName} HP: {character1.Hp}");
                        Console.WriteLine($"{character2.CharacterName} HP: {character2.Hp}");

                        round++; // Increment the round counter
                    }

                    if (character1.Hp > 0)
                    {
                        Console.WriteLine($"\n{character1.CharacterName} wins!");
                        character1.Exp += 10;
                        Console.WriteLine($"{character1.CharacterName} gained 10 EXP.");

                        // Item Drop: Random chance to drop an item
                        if (random.Next(1, 11) <= 3) // 30% chance
                        {
                            Console.WriteLine("Item dropped: Health Potion");
                            // Add Health Potion to the winner's inventory
                            Items healthPotion = context.Items.FirstOrDefault(i => i.ItemName == "Health Potion");
                            Inventory newInventoryEntry = new Inventory(character1.Id, healthPotion.Id, 1);
                            context.Inventory.Add(newInventoryEntry);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\n{character2.CharacterName} wins!");
                        character2.Exp += 10;
                        Console.WriteLine($"{character2.CharacterName} gained 10 EXP.");
                        // Item Drop: Random chance to drop an item
                        if (random.Next(1, 11) <= 3) // 30% chance
                        {
                            Console.WriteLine("Item dropped: Health Potion");
                            // Add Health Potion to the winner's inventory
                            Items healthPotion = context.Items.FirstOrDefault(i => i.ItemName == "Health Potion");
                            Inventory newInventoryEntry = new Inventory(character2.Id, healthPotion.Id, 1);
                            context.Inventory.Add(newInventoryEntry);
                        }
                    }

                    // Save changes to the database
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        void Attack(Character attacker, Character defender)
        {
            Random random = new Random();
            int damage = attacker.Attack + random.Next(1, 4); // Base damage + random bonus

            // Implement Attack Logic
            if (attacker.Skill == "Radiant Burst")
            {
                // Radiant Burst skill: Increase damage by 7
                damage += 7;
                Console.WriteLine($"{attacker.CharacterName} used Radiant Burst and increased damage by 7.");
            }

            if (attacker.Skill == "Advanced Technology")
            {
                // Advanced Technology skill: Increase damage by 7
                damage += 7;
                Console.WriteLine($"{attacker.CharacterName} used Advanced Technology and increased damage by 7.");
            }

            if (attacker.Skill == "Neon Beam")
            {
                // Neon Beam skill: Increase damage by 7
                damage += 7;
                Console.WriteLine($"{attacker.CharacterName} used Neon Beam and increased damage by 5.");
            }

            // Reduce defender's HP
            defender.Hp -= damage;
            Console.WriteLine($"{attacker.CharacterName} attacked {defender.CharacterName} and dealt {damage} damage.");

            // Check if defender is defeated
            if (defender.Hp < 0)
            {
                defender.Hp = 0; // Set HP to 0 if negative
            }
        }

        void Defend(Character defender)
        {
            // Basic defense mechanism: Reduces incoming damage by a small amount
            defender.Defence += 2; // Increase defense by 2 (you can adjust this value)
            Console.WriteLine($"{defender.CharacterName} defends, increasing their defense by 2!");
        }
    }

}