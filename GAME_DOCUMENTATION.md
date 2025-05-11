# Mad Batters Game Documentation

## Introduction

Welcome to the Mad Batters game project! This document serves as a comprehensive guide to understanding the game's structure, components, and logic. It's designed especially for developers who are new to game development, explaining not just what each component does, but why it's designed that way and how everything connects together.

## Table of Contents

1. [Game Overview](#game-overview)
2. [Project Structure](#project-structure)
3. [Core Game Systems](#core-game-systems)
   - [Game Manager](#game-manager)
   - [Player System](#player-system)
   - [Kitchen Objects System](#kitchen-objects-system)
   - [Sound System](#sound-system)
   - [Order System](#order-system)
4. [Unity Concepts for Beginners](#unity-concepts-for-beginners)
   - [GameObjects and Components](#gameobjects-and-components)
   - [Events and Event Systems](#events-and-event-systems)
   - [Scriptable Objects](#scriptable-objects)
5. [How to Extend the Game](#how-to-extend-the-game)

## Game Overview

Mad Batters is a cooking game where players must prepare and serve various recipes within a time limit. The game is inspired by titles like Overcooked, focusing on fast-paced kitchen management. Players interact with different kitchen stations to chop, cook, and assemble ingredients before delivering completed orders.

## Project Structure

The project follows a component-based architecture, which is standard for Unity games. Here's a breakdown of the main folders:

- **Assets**: Contains all game files
  - **Animation**: Contains all animation clips and controllers
  - **Prefabs**: Reusable game objects that can be instantiated at runtime
  - **Scenes**: Different game levels and menus
  - **Scriptable Objects**: Data containers for game items, recipes, etc.
  - **Scripts**: All C# code organized by functionality
  - **Sprites**: 2D images used in the game
  - **TextMesh Pro**: Text rendering system
  - **UI Toolkit**: UI elements and layouts

## Core Game Systems

### Game Manager

The Game Manager is the central control system of the game. It manages:

- **Game States**: Controls transitions between different states (menu, playing, paused, game over)
- **Score Tracking**: Keeps track of the player's score based on successful deliveries
- **Timer Management**: Handles the countdown timer for each level

```csharp
// Example of how the GameManager controls game state
public class GameManager : MonoBehaviour {
    // Singleton pattern allows easy access from other scripts
    public static GameManager Instance { get; private set; }
    
    // Game states using an enum (a special type that defines a set of named constants)
    public enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }
    
    private State state;
    
    // Methods to change game state, trigger events, etc.
}
```

**Why it's designed this way**: The Game Manager uses a "Singleton" pattern (there's only one instance that can be accessed globally) because many different systems need to check the game state or trigger state changes. This centralized approach prevents conflicts and makes the code more maintainable.

### Player System

The Player system handles everything related to the player character:

- **Movement**: Controls how the player moves around the kitchen
- **Interaction**: Manages how the player interacts with counters and objects
- **Inventory**: Tracks what the player is currently holding

The Player script uses Unity's input system to capture player actions and translates them into in-game movements and interactions.

**Key Concept - Events**: The Player script uses events to notify other systems when something important happens. For example, when a player picks up an item:

```csharp
// This is an event - it's like a broadcast system that other scripts can listen to
public event EventHandler OnPlayerPickUp;

// When the player picks up an item, this code runs
private void PickUpItem() {
    // Do the pickup logic...
    
    // Then notify any listeners that a pickup happened
    OnPlayerPickUp?.Invoke(this, EventArgs.Empty);
}
```

**Why it's designed this way**: Using events creates a "loose coupling" between systems. The Player doesn't need to know which systems care about pickups (like the sound system that plays pickup sounds) - it just announces that a pickup happened, and interested systems can respond.

### Kitchen Objects System

This system manages all the interactive elements in the kitchen:

- **BaseCounter**: The parent class for all counters, defining common behaviors
- **CuttingCounter**: For chopping ingredients
- **StoveCounter**: For cooking food
- **DeliveryCounter**: For submitting completed orders
- **KitchenObject**: Represents food items that can be picked up, processed, and combined

The kitchen system uses inheritance (where specialized counters inherit from BaseCounter) to share common code while allowing for unique behaviors.

**Key Concept - Inheritance**: 

```csharp
// Base class with shared functionality
public class BaseCounter : MonoBehaviour {
    // Common methods and properties for all counters
    public virtual void Interact(Player player) {
        // Default interaction behavior
    }
}

// Specialized counter that inherits from BaseCounter
public class CuttingCounter : BaseCounter {
    // Override to change the behavior for this specific counter type
    public override void Interact(Player player) {
        // Cutting-specific interaction
    }
}
```

**Why it's designed this way**: Inheritance reduces code duplication and makes it easier to add new counter types. All counters share basic functionality (like being able to interact with the player), but each type has its own specific behavior.

### Sound System

The Sound System manages all audio in the game:

- **SoundManager**: Plays sound effects based on game events
- **AudioMixerManager**: Controls volume levels for different audio channels

The SoundManager subscribes to events from various game systems and plays appropriate sounds when those events occur.

```csharp
// In SoundManager.cs
private void SubscribeToEvents() {
    // Listen for successful recipe delivery
    DeleveryManager.Instance.OnRecipeSuccess += OnRecipeSuccess;
    // Listen for failed recipe delivery
    DeleveryManager.Instance.OnRecipeFailed += OnRecipeFailed;
    // Listen for cutting actions
    CuttingCounter.OnAnyCut += OnAnyCut;
    // And so on...
}

// When a recipe is successfully delivered, this method is called
private void OnRecipeSuccess(object sender, System.EventArgs e) {
    PlaySound(audioClipsSO.deleverySuccess, success_mixer);
}
```

**Why it's designed this way**: The Sound System is completely decoupled from other systems - it just listens for events and plays sounds accordingly. This makes it easy to add or change sounds without modifying other code.

### Order System

The Order System manages recipe creation, validation, and delivery:

- **DeliveryManager**: Generates random orders and validates completed dishes
- **Recipe**: Defines the ingredients needed for a valid dish
- **RecipeListSO**: A collection of all possible recipes in the game

The Order System uses Scriptable Objects (explained below) to store recipe data, making it easy to add or modify recipes without changing code.

## Unity Concepts for Beginners

### GameObjects and Components

In Unity, everything in your game is a **GameObject**. Think of GameObjects as containers that hold different **Components** which define behavior.

For example, a player character GameObject might have these components:
- **Transform**: Defines position, rotation, and scale
- **SpriteRenderer**: Displays the visual appearance
- **Collider2D**: Handles physical collisions
- **Player Script**: Controls custom behavior

This component-based approach allows for flexible and reusable game elements.

### Events and Event Systems

**Events** are a programming pattern that allows different parts of your code to communicate without being directly connected. Think of events like a radio broadcast:

1. The **broadcaster** (e.g., Player) announces something happened (e.g., "I picked up an item!")
2. **Listeners** (e.g., SoundManager) who care about that event respond accordingly (e.g., "I'll play a pickup sound!")

Benefits of events:
- **Decoupling**: Systems don't need to reference each other directly
- **Extensibility**: New listeners can be added without changing the broadcaster
- **Cleaner code**: Avoids complex dependencies between systems

### Scriptable Objects

**Scriptable Objects** are special Unity assets that store data outside of scene objects. They're used extensively in this project for:

- **Recipe data**: Ingredients required for each dish
- **Audio clips**: Sound effects for different actions
- **Kitchen object data**: Properties of different food items

```csharp
// Example of a Scriptable Object for recipe data
[CreateAssetMenu(fileName = "NewRecipe", menuName = "Scriptable Objects/Recipe")]
public class RecipeSO : ScriptableObject {
    public string recipeName;
    public List<KitchenObjectSO> ingredients;
    public Sprite icon;
}
```

**Why use Scriptable Objects?**: They separate data from behavior, making it easier to modify game content without changing code. They also allow designers to create and modify game content without programming knowledge.

## How to Extend the Game

### Adding New Recipes

1. Create a new RecipeSO in the Unity Editor
2. Define the required ingredients and icon
3. Add it to the RecipeListSO

### Adding New Kitchen Objects

1. Create a new KitchenObjectSO
2. Define its properties (sprite, name, etc.)
3. Create a prefab for the object if needed
4. Update relevant recipes to use the new object

### Adding New Counter Types

1. Create a new script that inherits from BaseCounter
2. Override the Interact method with custom behavior
3. Create a prefab with the new counter script attached
4. Place instances of the counter in your scenes

## Conclusion

This documentation provides an overview of the Mad Batters game structure and systems. As you work with the codebase, you'll discover more details about how each component functions. The component-based architecture and event system make the code modular and extensible, so don't be afraid to explore and experiment!

If you have questions or need further clarification on any aspect of the game, please reach out to the original developer.

Happy coding!
