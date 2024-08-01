<div align="center"> <img src="https://github.com/RedLinesNT/VeryEvil-BRST/blob/main/Visual/VeryEvil_Wide.png"> </div>

"Very Evil" is a "Clash Royale"-like game for PC, but it's also my graduation project from my studies at Brassart Lyon.<br/>
I worked like a maniac on it from mid-February to mid-April 2024, all of that from scratch.<br/>

My aim was mainly to produce the foundation of the game like "Core Gameplay Elements" but also In-Engine tools, so that later the Level/Game Designers and Artists could integrate and create content into the engine for the game without having to rely on me.<br/>

This version dates from mid-April 2024 (18-04-2024), so contains little of the final version delivered on June 28 2024.<br/>
Every graphical "assets" that can be seen here are made by me in "Adobe XD" and are only as PLACEHOLDERS to give a better visualization of what's going on to the people who would later work into the engine for various reasons.

Even if none of their work is on this version of the repo, here are the members of this project:<br/>
<!--ts-->
   * [Kélian AUFFRET (Game Designer - God of Powerpoint)](https://www.linkedin.com/in/kelian-auffret-4a087622b/)
   * [Antoine Jullien (Level Designer - Forced to use my tools)](https://www.linkedin.com/in/antoine-jullien-26312a226/)
   * [Lucas CERQUEIRA (Producer - Our Swiss knife)](https://www.linkedin.com/in/lucas-cerqueira-94b3b9265/)
   * [Eliot ROZENBAUM (2nd Programmer - Forced to use my code)](https://www.linkedin.com/in/eliot-rozenbaum/)
   * [Léo GRIFFOULIERE (Programmer - Typing 0s and 1s like I was Neo in The Matrix)](https://www.linkedin.com/in/l%C3%A9o-griffouli%C3%A8re/)
<!--te-->
Same thing, but here's our beloved Game Artists:<br/>
<!--ts-->
   * [Coline SEGURAN](https://www.linkedin.com/in/coline-seguran-3d/)
   * [Melvin BORNE](https://www.linkedin.com/in/melvin-borne-9b6313259/)
   * [Mathilde CANNARD](https://www.linkedin.com/in/mathilde-cannard/)
   * [Maële DI NATALI](https://www.linkedin.com/in/ma%C3%ABle-di-natali/)
   * [Alan BENZADA](https://www.linkedin.com/in/alan-benzada-4100a4166/)
   * [Eva MOREAU](https://www.linkedin.com/in/eva-moreau-641799296)
<!--te-->

<hr>

## Summary

<!--ts-->
   * [Required programs](#required-programs)
   * [Project packages](#project-packages)
   * [Programming standards](#programming-standards)
   * [Documentation](#documentation)
<!--te-->

<hr>

## Required programs
  - Unity Hub
  - Unity Engine (2022.3.9f1 - LTS)

Programmers will need an IDE of their choice, these twos are recommended :
  - Visual Studio Community 2019/2022
  - JetBrains Rider 2023

### Missing the Unity version mentionned above ?
  To install the engine version required with Unity Hub, go to [this page from Unity](https://unity.com/releases/editor/whats-new/2022.3.9).<br>
  And at the top of this page, click on "<i>Install this version with Unity Hub</i>", then Unity Hub will deal with the rest.

<hr>

## Engine modules
  To be able to build the project, certain modules are required.<br>
  <i>Note that these modules are optional for members who do not wish to make deployable versions of the project.</i><br>
  To add modules to an already installed version of Unity, go to Unity Hub, then to "<i><strong>Install</strong></i>" > "<i><strong>2022.3.9f1</strong></i>" > "<i><strong>Add Modules</strong></i>". And select the following modules : 
  - Linux Build Support (IL2CPP) - (217 MB)
  - Mac Build Support (Mono) - (1.87 GB)
  - Windows Build Support (IL2CPP) - (418 MB)

<hr>

## Project packages

Here's the list of packages currently installed :
 - AI Navigation (1.1.5)
 - Burst (1.8.8)
 - Cinemachine (2.9.7)
 - Core RP Library (14.0.8)
 - Custom NUnit (1.0.6)
 - Input System (1.7.0)
 - JetBrains Rider Editor (3.0.28)
 - Mathematics (1.2.6)
 - ProBuilder (5.1.1)
 - Searcher (4.9.2)
 - Settings Manager (2.0.1)
 - Shader Graph (14.0.8)
 - Sysroot Base (2.0.7)
 - Sysroot Linux x64 (2.0.6)
 - Test Framework (1.1.33)
 - TextMeshPro (3.0.8)
 - Timeline (1.7.6)
 - Toolchain Win Linux x64 (2.0.6)
 - Unity UI (1.0.0)
 - Universal Render Pipeline (14.0.8)
 - Visual Studio Editor (2.0.22)

<hr>

## Programming standards

  Class :
    ```
    CamelCase
    ```<br>
  Attributes :
    ```
    camelCase
    ```<br>
  Variables :
    ```
    _camelCase
    ```<br>
  Methods :
    ```
    CamelCase()
    ```<br>
  Enums :
    ```
    ENameOfEnum
    ```<br>
  Enum's Values :
    ```
    VALUE
    ```<br><br>
All attributes must be private, use Properties or Getters/Setters instead.<br/>
Every names/comments MUST be in English, no matter how broken yours is.

<hr>

## Documentation
  Useful docs :<br>
<!--ts-->
   * <a href="Docs/Precompiled/Art/ExSubstance_imUnity-URP.pdf">Export your textures from Substance to Unity.</a>
   * <a href="Docs/Precompiled/Art/Skybox-Engine-Usage.pdf">Creation and usage of Procedural Skyboxes</a>
   * <a href="Docs/Precompiled/Level-Arch.pdf">Scene's architecture inside the engine.</a>
<!--te-->
  
<hr>
