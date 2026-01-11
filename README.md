# Arcane Pull

A 3D platformer game where players control a mage with gravity-defying magic abilities to escape enemies and find the portal to freedom.

## Game Description

**Arcane Pull** is a 3D platformer where the player plays as a mage with special magic that manifests as platforming abilities like reversing gravity and conjuring blocks to jump on. Navigate through challenging levels, escape from enemies, and find the portal to complete each stage.

## Play Now

**WebGL Demo**: [Play Arcane Pull on Unity Play](https://play.unity.com/en/games/01a2241b-c340-450a-a4f5-faa69d3a63b3/fp2)

## Game Vision

Set in a nature-filled medieval fantasy world, **Arcane Pull** combines vibrant, stylized visuals with engaging platforming mechanics. Players must use their magical abilities wisely to create escape routes, avoid enemies, and reach the end of each level.

## Core Gameplay

### Main Goal
Use your abilities strategically to create escape routes, avoid enemies, and reach the portal at the end of each level.

### Core Mechanics
- **Reverse Gravity** (Press Q): Flip gravity to walk on ceilings and access new areas
- **Conjure Platforms** (Press E): Generate platforms beneath you when airborne
- **Movement**: Standard WASD controls for running
- **Jump**: Space bar for jumping and double-jumping

### Genre
3D Platformer with magical abilities

## Characters

### Player Character: Ostra
A mage with extraordinary abilities to control gravity and conjure magical blocks. Ostra can run, double-jump, and manipulate the environment to overcome obstacles.

### Enemies
- **Normal Enemies (Melee)**: Ground-based enemies that follow the player but cannot jump
- **Special Enemies (Ranged)**: Advanced enemies with shooting abilities and greater mobility

## Game World & Setting

**Arcane Pull** takes place in a vibrant medieval fantasy world filled with magic and magical creatures. The game features:
- Stylized, slightly cartoony art style
- Bright green grass and colorful skies
- Glowing magical effects that stand out from the environment
- Nature-filled environments with fantasy architecture

## Features

- Unique gravity-reversing mechanic
- Platform conjuring ability for creative problem-solving
- Multiple enemy types with distinct behaviors
- Timer-based level completion system
- Pause menu and settings customization
- Player progress tracking and data management
- Immersive medieval fantasy atmosphere

## Technical Details

### Game Components

| Component | Description |
|-----------|-------------|
| **CameraController.cs** | Mouse-controlled camera system for optimal viewing angles |
| **PlayerController.cs** | WASD movement with space bar jumping and double-jumping |
| **PlayerGravitySkill.cs** | Gravity reversal ability (Q key) that flips player, camera, and controls |
| **PlayerPlatformSkill.cs** | Platform conjuring ability (E key) with auto-despawn timer |
| **EnemyAI.cs** | Ranged enemy behavior with detection and shooting mechanics |
| **NormalEnemyBehavior.cs** | Melee enemy AI with detection range and NavMesh pathfinding |
| **LevelManager.cs** | Level win/loss determination, timing system, and level transitions |
| **PlayerHealth.cs** | Player health system and damage calculation |
| **MainMenuBehavior.cs** | Main menu interface and navigation |
| **PauseMenuBehavior.cs** | In-game pause functionality |
| **SettingManager.cs** | Settings persistence and configuration |
| **PlayerDataManager.cs** | Player progress saving using PlayerPrefs |

## Controls

| Key | Action |
|-----|--------|
| **W/A/S/D** | Move character |
| **Space** | Jump / Double Jump |
| **Q** | Reverse Gravity |
| **E** | Conjure Platform (when airborne) |
| **Mouse** | Control camera |
| **ESC** | Pause menu |

## Development Team

**Group 15**

### Contributors

| Team Member | Contributions |
|-------------|---------------|
| **Hanwen Zeng** | Camera controller, player movement/jumping, platform conjuring skill, level management system |
| **Tavishi Bhatia** | Gravity reversal skill, menu systems (main/pause/settings), player data management, UI icon behaviors, object destruction system |
| **Bingqiao Qian** | Enemy AI (melee and ranged), player health system, projectile mechanics, magma hazard system |

## Assets & Acknowledgments

### 3D Models & Environments
- [Castle Pack by ProGru](https://assetstore.unity.com/packages/3d/environments/castle-pack-by-progru-185976)
- [Lowpoly Environment - Nature Free Medieval Fantasy Series](https://assetstore.unity.com/packages/3d/environments/lowpoly-environment-nature-free-medieval-fantas y-series-187052)
- [Stylized Fantasy Props Sample](https://assetstore.unity.com/packages/3d/props/stylized-fantasy-props-sample-234139)
- [Battle Wizard Poly Art](https://assetstore.unity.com/packages/3d/characters/humanoids/fantasy/battle-wizard-poly-art-128097)
- [Stylized Free Skeleton](https://assetstore.unity.com/packages/3d/characters/creatures/stylized-free-skeleton-298650)
- [RPG Monster Duo PBR Polyart](https://assetstore.unity.com/packages/3d/characters/creatures/rpg-monster-duo-pbr-polyart-157762)

### Visual Effects
- [Free Quick Effects Vol. 1](https://assetstore.unity.com/packages/vfx/particles/free-quick-effects-vol-1-304424)
- [Magic Effects Free](https://assetstore.unity.com/packages/vfx/particles/spells/magic-effects-free-247933)

### Audio
- [Free 10 Medieval Ambient Fantasy Tracks Music Pack](https://assetstore.unity.com/packages/audio/music/free-10-medieval-ambient-fantasy-tracks-music-pack-3 10781)
- [Regular Impact Sounds - Sound Effects](https://assetstore.unity.com/packages/audio/sound-fx/regular-impact-sounds-sound-effects-278024)
- [Free Casual Game SFX Pack](https://assetstore.unity.com/packages/audio/sound-fx/free-casual-game-sfx-pack-54116)

### UI
- [Fantasy Wooden GUI Free](https://assetstore.unity.com/packages/2d/gui/fantasy-wooden-gui-free-103811)
- [IM Fell English SC Font](https://fonts.google.com/specimen/IM+Fell+English+SC?query=IM+Fell+English+SC)

## Known Issues & Future Improvements

### Current Issues
- Magma visual effect uses basic plane with textures (planned improvement: implement water simulation)
- Jump behavior and stair navigation may have occasional glitches (planned: animation improvements)
- Game instructions can be easily skipped (planned: make how-to-play screen unskippable)

## License

This project uses various Unity Asset Store assets. Please refer to individual asset licenses for usage terms.

## Support

For questions, bug reports, or feedback, please open an issue in the repository.

---

**Game Type**: 3D Platformer  
**Engine**: Unity  
**Status**: Gold Master Release
