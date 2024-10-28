
# PacStudent Game - Assessment 3

This project is a PacStudent-style game developed for **Assessment 3**. The game features PacStudent moving along a defined path with interactive sounds and animations. Below is a detailed description of the key features and how to run the game.

## Features

### 1. PacStudent Movement
- **Frame-rate Independent Motion**: PacStudent moves smoothly along a predefined path at a constant speed. The movement is implemented using Vector3.MoveTowards() and is frame-rate independent.
- **Linear Movement**: PacStudent's speed is consistent throughout the entire movement. It moves clockwise around a series of waypoints.

### 2. Audio Effects
- **Purpur Sound Effect**: While PacStudent is walking, you can hear a subtle "purpur" sound effect, resembling a cat's purring sound. This adds immersion to the player's experience.
- **Background Music**: The game includes background music that plays continuously, but its volume has been adjusted so that the player's movement sound effects are not overpowered.

### 3. Ghost Animation
- **Ghost States**: There are several states for Ghost, including walking, scared (when they are frightened), and dead.
- **Dead State**: In the "dead" state, the Ghost turns into a small cat with wings. This animation is controlled by pressing the **spacebar**.

### 4. Controls
- **Movement Animation**: PacStudent's movement animations automatically change depending on its direction.
- **Dead Animation Trigger**: The "dead" state for Ghost is triggered by pressing the **spacebar**. When in this state, the Ghost will turn into a winged cat animation.

## How to Play
1. Run the game in Unity by clicking the **Play** button.
2. Watch as PacStudent moves along the path, playing the purpur sound effect while walking.
3. To activate the Ghost's dead animation (winged cat), press the **spacebar**.
