# Betting Game

## Demo

[![Watch the demo](https://img.youtube.com/vi/v0RLttZTbvA/0.jpg)](https://www.youtube.com/embed/v0RLttZTbvA)

## Description

This is a mobile game project created using Unity. The game features betting, a UI for input and displaying balance and winnings, as well as animations and user interactions.

## Target Platform

The target platform for this project is **iOS**. The game is tested to run on iPhone devices.

## Tech Stack

### 1. **Unity**
- The primary game development platform used for this project.

### 2. **Zenject**
- A Dependency Injection library used to manage object dependencies, simplifying the creation and configuration of objects and their interactions.

### 3. **UniTask**
- Used for asynchronous tasks instead of Unity's standard coroutines, providing more efficient handling of asynchronous operations and better performance.

### 4. **DOTween**
- A library for animations, used to create smooth transitions, state changes, and UI element animations (e.g., displaying winnings).

### 5. **VitalRouter (Message Bus)**
- An event/message bus system used to simplify communication between different parts of the game, helping to decouple logic and make the project easier to maintain.

## Applied Design Patterns

### 1. **Entry Point**
- A design pattern used to provide a clear entry point into the application, enabling easier management of initialization and game start states.

### 2. **Object Pool**
- A pattern used for reusing objects (e.g., UI elements like reward labels), improving performance and reducing unnecessary memory allocations.

### 3. **State Machine**
- Used to manage different game states, such as betting, winnings, and UI states. This pattern helps to simplify the application's logic, making it more readable and flexible.

## Flexibility of Modules

The game's modules (such as level designs, betting systems, and UI components) are **decoupled** and **extendable**. For example:

- **Level Structures**: The game’s levels are designed to be dynamic and can easily be extended or modified to create new level types or change the game's logic.
- **Product and Reward Details**: The system handling product information, rewards, and multipliers is flexible, allowing for simple updates without impacting other parts of the game.

This ensures that the game is easily scalable and maintainable, enabling future updates and modifications without needing to overhaul the core systems.

## Unity Version

This project was developed using **Unity 2023.2.20f1**.

## Setup

1. Clone the repository or download the project:

   ```bash
   git clone https://github.com/alex-spiian/Betting.git
