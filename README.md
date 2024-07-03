# Very Evil

Branche spécifique pour @RedLinesNT, destinée au developpement du nouveau (et final) prototype de Very Evil.
Veuillez ne pas push ou utiliser cette branche sauf si @RedLinesNT vous l'a demandé, merci.

<hr>

## Table des matières

<!--ts-->
   * [Programmes requis](#programmes-requis)
   * [Packages du projet](#packages-du-projet)
   * [Norme de programmation](#norme-de-programmation)
   * [Signaler un problème](#signler-un-problème)
   * [Documentation](#documentation)
<!--te-->

<hr>

## Programmes requis
  - GitHub Desktop
  - Unity Hub
  - Unity Engine (2022.3.9f1 - LTS)

Les programmeurs auront besoin d'avoir un "IDE" de leur choix. L'un de ces deux IDE sont recommandés :
  - Visual Studio Community 2019/2022
  - JetBrains Rider 2023

<i>Il est fortement recommandé d'avoir Visual Studio Community d'installé peu importe l'IDE pour faire une version déployable du projet.</i>

### Vous ne possedez pas la version d'Unity mentionné au dessus ?
  Pour installer la version du moteur demandée avec Unity Hub, rendez-vous sur [cette page d'Unity](https://unity.com/releases/editor/whats-new/2022.3.9).<br>
  En haut de cette page, cliquez sur "<i>Install this version with Unity Hub</i>".

<hr>

## Modules du moteur
  Pour être en mesure de faire des builds du projet, certains modules du moteur sont nécessaire.<br>
  <i>Notez que ces modules sont optionnels pour les membres ne cherchant pas à faire des versions deployable du projet.</i><br>
  Pour ajouter des modules à une version d'Unity déjà installée, rendez-vous dans Unity Hub, puis dans "<i><strong>Install</strong></i>" > "<i><strong>2022.3.9f1</strong></i>" > "<i><strong>Add Modules</strong></i>". Et sélectionnez les modules suivant : 
  - Linux Build Support (IL2CPP) - (217 MB)
  - Mac Build Support (Mono) - (1.87 GB)
  - Windows Build Support (IL2CPP) - (418 MB)

<hr>

## Packages du projet

Voici la liste des packages installés sur ce projet :
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

## Norme de programmation

  Class :
    ```
    CamelCase
    ```<br>
  Attributs :
    ```
    camelCase
    ```<br>
  Variables :
    ```
    _camelCase
    ```<br>
  Methodes :
    ```
    CamelCase()
    ```<br>
  Enums :
    ```
    ENameOfEnum
    ```<br>
  Valeur d'un Enum :
    ```
    VALUE
    ```<br><br>
L'entièreté des attributs doivent être en private. Utilisez à la place des <i>Properties</i> ou des <i>Getters/Setters</i>.<br>
<strong>Tous les noms doivent être de préférence en anglais!</strong>

### Commentaires
  Afin que votre code puisse être comprit par le reste de l'équipe, il est important de commenter votre code.<br>
  Commentez votre code avec des "<i>//</i>" pour les attributs et les variables et des passages de code si nécessaire!<br>
  Commentez vos "<i>methods</i>" avec des ```<summary>```.

<hr>

## Signaler un problème
  C'est super simple!<br>
  Rendez-vous dans la categorie "<i>Issues</i>" de Github (sur la page web), puis crèez une nouvelle issue.<br>
  Regardez l'issue "Exemple" pour en créer une nouvelle.
  
<hr>

## Documentation
  Vous trouverez ici des liens vers de la documentation pour par exemple:
	- Intégrer du contenu comme des textures/modèles dans le projet
	- Utilisation de certains outils d’Unity ainsi que ceux conçus pour ce projet.
	- Documentation du code

  Documents utiles :<br>
<!--ts-->
   * <a href="Docs/Precompiled/Art/ExSubstance_imUnity-URP.pdf">Exporter ses textures de Substance / En importer sur Unity (URP)</a>
   * <a href="Docs/Precompiled/Art/Skybox-Engine-Usage.pdf">Création et utilisation des Skybox procédurales</a>
   * <a href="Docs/Precompiled/Level-Arch.pdf">Architecture des Scenes au sein du moteur</a>
<!--te-->
  
<hr>
