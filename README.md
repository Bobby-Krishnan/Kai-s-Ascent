# Kai-s-Ascent
CS 390 Project


Game Title: Kai’s Ascent

List of All Team Members, Abhishek Krishnan (AK700)

GitLab or GitHub Link (include this in the PDF for Gradescope submission): https://github.com/Bobby-Krishnan/Kai-s-Ascent.git 

Game Pitch: Kai’s Ascent is a retro-inspired 2D side-scrolling platformer featuring four progressively challenging levels. Players control Kai as they jump, dodge, and blast their way through waves of enemies and hazards. It’s built for a simple arcade-style experience with polished gameplay. 

How to Play: 
Arrow Keys – Move left and right
Up Arrow – Jump (Double jump unlocks starting in Level 3),\
X Key – Shoot a fireball in the direction you're facing. 
Your goal is to defeat all enemies in each level. Once all enemies are eliminated, the game will automatically progress to the next stage. 
Avoid hazards like water zones. Taking damage reduces your health, and death will return you to the title screen. Complete all four levels to reach the ending!

Amount & Type of Content Available: 
4 Playable Levels: Each level features handcrafted terrain using Unity's Tilemap system, Difficulty increases progressively with tighter platforming, more enemies, and additional hazards, Levels share a consistent visual style (retro pixel-art) but grow more challenging

1 Title Screen Scene: Contains the game’s background, title, and a working “Start Game” button

Scene Transitions: Each level transitions automatically to the next once all enemies are defeated. The final level displays a victory message and auto-returns to the title screen after 10 seconds

Movement: 2D directional movement using arrow keys, Jumping using the Up Arrow key, Double Jump unlocked in Level 3 (automatically enabled when that scene loads)

Attacks: Fireball projectile launched with X key, Fires in the direction the player is facing, Fireball uses a separate prefab with animation and directional velocity

Health System: Player has max health with real-time health tracking. Health is persistent across levels (does not reset between scenes). Taking damage reduces health; dying returns player to the title screen

Hazards: Water zones cause player death or damage. Edges of the map bounded by kill zones

Enemy Type: SlimeEnemy: Patrols horizontally, flipping when reaching edges or walls. Damages player on collision. Dies with an animation when health reaches 0

Enemy Health System: Each enemy has a health value and takes multiple hits from fireballs

Enemy Counter System: Tracks the number of enemies in the level. Decrements the counter as each enemy dies. Displays as UI in the top corner. Automatically progresses to next level when enemy count reaches 0

Persistent HUDCanvas: Displays player health, enemy counter, and in-level tutorial messages. Uses DontDestroyOnLoad to remain visible across all gameplay scenes

Tutorial Popups: Initial tutorial appears at the start of Level 1 (explaining movement and shooting). Double jump hint appears at the start of Level 3

Victory Message: On completing Level 4, a prefab “You Win” message is displayed. Waits 10 seconds, then returns player to title screen

Start Menu: Interactive start button loads MainGameScene.
Canvas Scaler Setup: All UI uses CanvasScaler with Scale With Screen Size for resolution independence

Background Music: One looping .mp3 track. Managed via AudioManager.cs with DontDestroyOnLoad

Sound Effects: Fireball shooting. Jumping (used for both regular and double jump)

Audio Optimization: SFX clips set to Decompress on Load for instant playback. Background music set to Streaming for memory efficiency

Scene Management: Levels load via SceneManager.LoadScene().Scene transitions handled via EnemyManager upon final enemy defeat

EnemyManager.cs: Tracks enemy count, Updates UI, Handles scene transitions and final level victory

PlayerHealth.cs: Manages persistent health, Triggers death and game reset

PlayerMovement.cs: Handles movement, jump, double jump, and fireball logic

SlimeEnemy.cs: Patrol AI, collision with player, and death logic

AudioManager.cs: Singleton for persistent background music

TutorialHint.cs & Level3HintSpawner.cs: Display level-specific hints at runtime

Physics: Player and tilemaps use custom physics material with Friction = 0 for smoother movement

Prefab Architecture: All enemies, UI popups, and fireballs are prefab-based

Lessons Learned: This project was great in refining my skills in Unity. I began thinking about problems that I had not considered during the Unity Game Jam, such as creating a new physics material to put on walls to prevent collision, or how to properly manage the Canvas UI, and also the importance of using prefabs. I became familiar with the camera game object and its different utilities like zooming out to show more of the game screen, making the camera follow the player and keeping it fixed as a side scroller effect. I became more comfortable with scene management and handling of different scene triggers (this helped to make the game continue as long as someone wanted to play, such as Death -> Title and also Win - > Title. Lastly, I realized that I don’t need to make a separate script and reference it for every single thing but instead it is smarter to modularize sometimes and include certain components all in the same c# file. 

Utilized these assets:
Tile Map: https://v3x3d.itch.io/block-land/download/eyJpZCI6MTcyMTA1OSwiZXhwaXJlcyI6MTc0NDU4Njk2Mn0%3d.KknJumk2VQOiDVW1eHr%2blSpYf1E%3d

Slime asset:
https://tienlev.itch.io/slime-pixel-set/download/eyJpZCI6MTIwMDkxMywiZXhwaXJlcyI6MTc0NDY2Mzg2NH0%3d.gb%2fXgyTkC%2bZUrHvKlgTeDUz%2bDao%3d

Background music: https://www.fesliyanstudios.com/royalty-free-music/downloads-c/8-bit-music/6

Sound effects: https://kronbits.itch.io/freesfx
