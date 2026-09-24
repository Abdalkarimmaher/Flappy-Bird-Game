# Flappy Bird Game

A 2D Flappy Bird-style game built with Unity and C#. Keep the bird in the air, fly through gaps between pipes, and try to improve your score.

## Features

- Flap with the space bar, left mouse button, or a touch on the screen.
- Random pipe heights and scoring when the bird passes through a pipe gap.
- Pipe and ground collision detection, a game-over screen, and a restart button.
- Sound effects for flapping, scoring, collisions, and restarting.

## Open and play

1. Install **Unity 6000.2.10f1** through Unity Hub.
2. Clone or download this repository.
3. In Unity Hub, choose **Add project from disk** and select the repository folder.
4. Open the project and allow Unity to restore packages and import assets.
5. Open `Assets/Scenes/SampleScene.unity`, then press **Play**.
6. Press **Space**, click, or tap to flap. Use the restart button after a collision.

## Project layout

| Folder | Contents |
| --- | --- |
| `Assets/Scripts` | Bird controls, scoring, pipe movement, and spawning |
| `Assets/Scenes` | Main game scene |
| `Assets/Game Objects` | Sprites and artwork |
| `Assets/Sounds` | Audio assets |
| `Assets/Settings` | Rendering configuration |
| `Packages` | Unity package dependencies |
| `ProjectSettings` | Editor version, build scenes, and project settings |

Unity caches, editor preferences, recovery scenes, and generated builds are excluded from version control. Unity recreates its cache when the project is opened.

## Build

Open **File > Build Profiles**, select your target platform, and install its Unity build-support module if needed. The game scene is already enabled in the build scene list. Save build output to a `Builds` folder.

## Assets

This project includes third-party artwork and audio used while learning Unity. Their original owners retain their rights; this repository does not grant a separate license for those assets.
