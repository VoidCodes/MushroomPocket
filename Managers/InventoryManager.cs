using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MushroomPocket.Context;
using MushroomPocket.Models;
using Microsoft.EntityFrameworkCore;

namespace MushroomPocket.Managers
{
    // Note: Item Usage will be in class for now, 
    // but will be moved to a separate class as the project grows
    public class InventoryManager
    {
        public void AddItem()
        {
            using (var context = new Dbcontext())
            {
                try
                {
                    Console.Write("Enter item name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter item description: ");
                    string description = Console.ReadLine();

                    if (name == " " || description == " ")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception("Invalid input. Please enter valid values.");
                    }

                    // Get effect type from user (you might want to display options)
                    Console.Write("Enter item effect type (HPBoost, EXPBoost, Special): ");
                    string effectTypeString = Console.ReadLine();
                    if (!Enum.TryParse(effectTypeString, out ItemEffectType effectType))
                    {
                        throw new Exception("Invalid effect type.");
                    }

                    Console.Write("Enter effect value: ");
                    int effectValue = Convert.ToInt32(Console.ReadLine());

                    // Create the new item
                    Items newItem = new Items
                    {
                        ItemName = name,
                        Description = description,
                        EffectType = effectType,
                        EffectValue = effectValue
                    };

                    // Add the item to the database
                    context.Items.Add(newItem);
                    context.SaveChanges();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{name} added to the database.");
                    Console.ResetColor();
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter valid values.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        public void AddItemToInventory()
        {
            using (var context = new Dbcontext())
            {
                try
                {
                    // 1. Get Character Name
                    Console.Write("Enter the name of the character: ");
                    string characterName = Console.ReadLine();

                    // 2. Get Item Name
                    Console.Write("Enter the name of the item: ");
                    string itemName = Console.ReadLine();

                    // 3. Find Character and Item in Database
                    Character character = context.Character.FirstOrDefault(c => c.CharacterName == characterName);
                    Items item = context.Items.FirstOrDefault(i => i.ItemName == itemName);

                    if (character == null)
                    {
                        throw new Exception("Character not found.");
                    }
                    if (item == null)
                    {
                        throw new Exception("Item not found.");
                    }

                    // 4. Get Quantity
                    Console.Write("Enter quantity: ");
                    int quantity = Convert.ToInt32(Console.ReadLine());

                    // 5. Check Existing Inventory Entry
                    Inventory existingEntry = context.Inventory.FirstOrDefault(
                        e => e.CharacterId == character.Id && e.ItemId == item.Id);

                    if (existingEntry != null)
                    {
                        // Update quantity if entry exists
                        existingEntry.Quantity += quantity;
                    }
                    else
                    {
                        // Create a new inventory entry
                        Inventory newEntry = new Inventory(character.Id, item.Id, quantity);
                        context.Inventory.Add(newEntry);
                    }

                    // 6. Save Changes
                    context.SaveChanges();

                    Console.WriteLine($"{quantity} x {itemName} added to {characterName}'s inventory.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter valid values.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        public void ViewCharacterInventory()
        {
            using (var context = new Dbcontext())
            {
                Console.Write("Enter the name of the character: ");
                string characterName = Console.ReadLine();

                // Find the character in the database
                Character character = context.Character.FirstOrDefault(c => c.CharacterName == characterName);

                if (character == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Character not found.");
                    Console.ResetColor();
                    return;
                }

                // Retrieve the character's inventory
                var inventory = context.Inventory
                    .Where(i => i.CharacterId == character.Id)
                    .Include(i => i.Items)
                    .ToList();

                if (inventory.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{characterName} has no items in their inventory.");
                    Console.ResetColor();
                    return;
                }

                Console.WriteLine($"{characterName}'s Inventory:");
                Console.WriteLine("--------------------");

                foreach (var entry in inventory)
                {
                    Console.WriteLine($"Item: {entry.Items.ItemName}");
                    Console.WriteLine($"Description: {entry.Items.Description}");
                    Console.WriteLine($"Effect: {entry.Items.EffectType} ({entry.Items.EffectValue})");
                    Console.WriteLine($"Quantity: {entry.Quantity}");
                    Console.WriteLine("--------------------");
                }
            }
        }

        public void UseItem(Character characters, Items item, Character targetCharacter = null)
        {
            using (var context = new Dbcontext())
            {
                Console.Write("Enter the name of the character: ");
                string characterName = Console.ReadLine();

                // Find the character in the database
                Character character = context.Character.FirstOrDefault(c => c.CharacterName == characterName);

                if (character == null)
                {
                    Console.WriteLine("Character not found.");
                    return;
                }

                // Retrieve the character's inventory
                var inventory = context.Inventory
                    .Where(i => i.CharacterId == character.Id)
                    .Include(i => i.Items)
                    .ToList();

                if (inventory.Count == 0)
                {
                    Console.WriteLine($"{characterName} has no items in their inventory.");
                    return;
                }

                Console.WriteLine($"{characterName}'s Inventory:");
                Console.WriteLine("--------------------");

                foreach (var entry in inventory)
                {
                    Console.WriteLine($"Item: {entry.Items.ItemName}");
                    Console.WriteLine($"Description: {entry.Items.Description}");
                    Console.WriteLine($"Effect: {entry.Items.EffectType} ({entry.Items.EffectValue})");
                    Console.WriteLine($"Quantity: {entry.Quantity}");
                    Console.WriteLine("--------------------");
                }

                Console.Write("Enter the name of the item to use: ");
                string itemName = Console.ReadLine();

                Inventory inventoryEntry = inventory.FirstOrDefault(i => i.Items.ItemName == itemName);

                if (inventoryEntry == null)
                {
                    Console.WriteLine("Item not found in inventory.");
                    return;
                }

                // Use the item
                switch (inventoryEntry.Items.EffectType)
                {
                    case ItemEffectType.HPBoost:
                        character.Hp += inventoryEntry.Items.EffectValue;
                        break;
                    case ItemEffectType.EXPBoost:
                        character.Exp += inventoryEntry.Items.EffectValue;
                        break;
                    case ItemEffectType.Special:
                        // Implement special effect
                        break;
                }

                // Decrement quantity
                inventoryEntry.Quantity--;

                // Remove item from inventory if quantity is 0
                if (inventoryEntry.Quantity == 0)
                {
                    context.Inventory.Remove(inventoryEntry);
                }

                // Save changes
                context.SaveChanges();

                Console.WriteLine($"{characterName} used {itemName}.");
            }
        }

        public void UseItemFromMenu()
        {
            using (var context = new Dbcontext())
            {
                try
                {
                    // 1. Get Character Name
                    Console.Write("Enter the name of the character: ");
                    string characterName = Console.ReadLine();

                    // 2. Get Item Name
                    Console.Write("Enter the name of the item to use: ");
                    string itemName = Console.ReadLine();

                    // 3. Find Character and Item in Database
                    Character character = context.Character.FirstOrDefault(c => c.CharacterName == characterName);
                    Items item = context.Items.FirstOrDefault(i => i.ItemName == itemName);

                    if (character == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception("Character not found.");
                    }
                    if (item == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception("Item not found.");
                    }

                    // 4. Determine Target (Ask if it's for the character themselves or another character)
                    Console.Write($"Use {itemName} on {characterName}? (y/n): ");
                    string targetChoice = Console.ReadLine()?.ToLower();

                    if (targetChoice == "y")
                    {
                        // Use item on the character themselves (no target)
                        UseItem(character, item);
                    }
                    else if (targetChoice == "n")
                    {
                        // Prompt for the target character's name
                        Console.Write("Enter the name of the target character: ");
                        string targetCharacterName = Console.ReadLine();

                        // Find the target character
                        Character targetCharacter = context.Character.FirstOrDefault(c => c.CharacterName == targetCharacterName);
                        if (targetCharacter == null)
                        {
                            throw new Exception("Target character not found.");
                        }

                        // Use the item on the target character
                        UseItem(character, item, targetCharacter);
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter valid values.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}