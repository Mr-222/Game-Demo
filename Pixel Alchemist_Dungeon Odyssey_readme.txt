I. Start scene file

Scenes/Main for start scene, the main play scene is Scenes/Alpha Test

II. How to play and what parts of the level to observe technology requirements

The player can be controlled by WASD and rotates with the cursor. The left shift key will make the player dash, and the space key will make the player jump.  The player can make a regular attack by pressing the left key. There are four abilities the player can use with 1,2,3,4 number keys. There are health points and mana settings in the game. When you pick an ability from the card deck, you need to click the left key to use it, or use the right key to cancel it. When the player does not have enough mana, there will be no response from the card decks(no drawing card reaction). We haven’t explicitly set the player’s goal on the HUD. The goal for this alpha test is for the player to kill 10 enemies.  There is music in the start scene, and sound effects for the abilities of  the player in the alpha test scene.



III. Known problem areas:
	AI: At present, enemies continue to move towards the player while the damage animation plays because the navmesh agent remains active, resulting in a sliding effect. To address this, a future update could include one of two potential solutions: (1) deactivating the navmesh agent at the start of the animation and reactivating it upon completion, or (2) employing animation blending and inverse kinematics (IK) to override the animation.
	Enemy Attack: The attack numbers may need to be adjusted for better balance.
	Win Condition: Currently, the game concludes when a specific number of enemies are defeated. Moving forward, we plan to introduce a scenario where, upon reaching a predetermined kill count, a powerful boss enemy will spawn, requiring the player to defeat this boss to progress.
	Level: Only 1 level has been implemented so far, more level will be added in the future. 
	Sound effects: the sound effects do not synchronize well with the player’s abilities
	Ability: Each ability should have effect such as reducing enemy speed and make them stunned etc, these would be implemented in the final version

IV. Manifest of which files authored by each teammate:

1. Detail who on the team did what.
Carl Xiong: Developed a foundational system that incorporates an AI state machine and controls for enemy animations in a modular structure, making it easier to introduce new enemies. Designed an enemy spawner to distribute enemies throughout the game environment and crafted animations along with melee and projectile attack capabilities for the enemies. Established a game end condition manager to identify the completion of levels and implemented a user interface for the enemies.
Yiwei Gao: Responsible for designing and creating the entire scene, utilizing and setting up assets that were downloaded from asset stores. Focusing on careful planning, assets collision size customization to deliver a cohesive and engaging game environment.
Yuxing Sun: Supplemented HUD with the possibility to start, pause, and load the game, implemented slider  mechanics on health, mana, and sprint recovery, and switching the sound volume within the settings menu
Yutong Sun: Implemented camera movement, camera can chase main character’s movement, also can be affected by mouse input to move up/down/left/right. Implemented player controller script to control the main character’s movement and rotation based on keyboard and mouse input. Modeled a card model by using blender, implemented a skill system by treating a card as a skill. Implemented logic about trigger and cancel skills. Skills will be randomly drawn from a card deck, and will be put into the bottom of the card deck after being used. Implemented an audio manager to play sound effects. Wired multiple subsystems together.
Jiaqi He: Implement the player jump and dash functionality. Apply the animation for the player with the animator. Also found the particle systems and special effects of the player’s abilities. Created the abilities prefabs and adjusted the player’s damages with these abilities. Using the ray casts and physics.overlap to make sure the abilities are set on the ground and can damage the enemies. Write scripts for the player to take damage and cost mana.
2. For each team member, list each asset implemented. 
	Carl Xiong: Assets/Animations/Enemy/*, Assets/Resources/Prefabs/Enemy/*
	Yiwei Gao:  Assets/Resources/Layers
	Yuxing Sun: Assets/GUI PRO Kit - Fantasy RPG/*
	Jiaqi He: Assets/Animation/Character/* Assets/Prefabs/Character/*
            Yutong Sun: Assets/Resources/Cards/*, Assets/Prefabs/Card/Card Manager, MainCamera, OverlayCamera
3. Make sure to list C# script files individually so we can confirm each team member contributed to code:
	Carl Xiong: Assets/Scripts/EnemyControl/*, Assets/Scripts/Manager/*, /Scripts/Events/Spawner.
	Yiwei Gao: Assets/Scripts/RandomPosition.cs
	Yuxing Sun: Assets/Scripts/UI Settings/*
	Jiaqi He: Assets/Scripts/CharacterControl/*
	Yutong Sun: Assets/Scripts/Audio/*, Assets/Scripts/Cards/*,      Assets/Scripts/Camera/*, Assets/Scripts/CharacterControl/PlayerController, Assets/Scripts/Collectable/*
		


