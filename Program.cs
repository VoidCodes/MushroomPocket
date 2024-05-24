using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MushroomPocket.Context;
using MushroomPocket.Models;
using Microsoft.EntityFrameworkCore;

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
                new MushroomMaster("Abbas", 4, "Shawarma Man"),
                new MushroomMaster("Primagen", 2, "Protogen"),
            };

            //Use "Environment.Exit(0);" if you want to implement an exit of the console program

            //Dictionary to keep track of the number of characters that can be transformed
            Dictionary<string, int> transformCriteria = new Dictionary<string, int>();

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
                        case "6":
                            AddItem();
                            break;
                        case "7":
                            AddItemToInventory();
                            break;
                        case "8":
                            ViewCharacterInventory();
                            break;
                        case "9":
                            UseItem();
                            break;
                        case "10":
                            StartBattle();
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
                                break;
                            case "Primagen":
                                character = new Primagen(hp, exp);
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
                                        newCharacter.Skill = "NO KETCHUP ON ZA SHAWARMA";
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

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{characterName}'s stats have been updated");
                            Console.ResetColor();
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

            /* Extra Features goes here */
            void AddItem()
            {
                using (var context = new Dbcontext())
                {
                    try
                    {
                        Console.Write("Enter item name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter item description: ");
                        string description = Console.ReadLine();

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

            void AddItemToInventory()
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
                            /*Inventory newEntry = new Inventory
                            {
                                CharacterId = character.Id,
                                ItemId = item.Id,
                                Quantity = quantity
                            };*/
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

            void ViewCharacterInventory()
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

            void UseItem()
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

            void StartBattle()
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
                            throw new Exception("Character not found.");
                        }

                        // Battle logic
                        // 2. Battle Loop
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
                                    UseItem();
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
                                        UseItem();
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

                        Console.WriteLine("Battle over!");
                        Console.WriteLine($"{character1Name} has {character1.Hp} HP left.");
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
                    // Radiant Burst skill: Increase damage by 5
                    damage += 5;
                    Console.WriteLine($"{attacker.CharacterName} used Radiant Burst and increased damage by 5.");
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
}