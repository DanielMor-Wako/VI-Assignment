**Overview**

The Game State Management System in Unity provides a scalable solution for managing game state data. It comprises the GameStateManager, PrefabBank, and integrates with external services using interfaces like Firebase for data storage and JSON serialization. This system allows for seamless orchestration of game state data and assets within Unity engine.

**Components**

GameStateManager: Manages game state, including saving and loading game data using scalable storage and serialization methods.
Services: Integrates with Firebase for scalable data storage and employs the JsonSerializer for flexible data serialization.
PrefabBank: A central repository for game prefabs, utilizing Unity's basic features of Scriptable Objects for easy management of prefabs and their classes, despite not being generic. This enhances modularity and reusability of assets.

**Usage**

1. Enter a unique userId in the scene during runtime, for instance "user007".
2. Set the display name and level, then press Save.
Note: A userid must be saved before it is loaded.
3. Restart the app, enter the userId you have chosen and press Load

**Future Development**

To fully showcase the capacity of the system, consider expanding the integration with additional external services for data storage and exploring alternative serialization methods to accommodate diverse game state management requirements.

Personal note:
- No external assemblies were used, as namespaces provided sufficient organization for the project.
- Reflection was used on serialization for a more generic approach, yet the traid off being less performant.
The reason for choosing reflection is due to the fact that Loading and Saving does not occur frequencly duruing the app runtime.
