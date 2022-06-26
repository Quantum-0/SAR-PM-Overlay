# SAR-PM-Overlay

This project is a game overlay for [Super Animal Royale](https://animalroyale.com/) provides UI for moders/private match hosters.

It displays bar in the bottom of the screen with buttons. When you click on them, tool set focus on game window and input commands via chat simulating user input.

[![wakatime](https://wakatime.com/badge/user/7c9029ee-89d1-45a3-8197-cbf6c3bcaf78/project/f55f187d-9c69-43cb-b40c-baa66fc0e648.svg)](https://wakatime.com/badge/user/7c9029ee-89d1-45a3-8197-cbf6c3bcaf78/project/f55f187d-9c69-43cb-b40c-baa66fc0e648)
[![Lint Code Base](https://github.com/Quantum-0/SAR-PM-Overlay/actions/workflows/lint.yml/badge.svg)](https://github.com/Quantum-0/SAR-PM-Overlay/actions/workflows/lint.yml)
[![Build .NET Desktop](https://github.com/Quantum-0/SAR-PM-Overlay/actions/workflows/build.yml/badge.svg)](https://github.com/Quantum-0/SAR-PM-Overlay/actions/workflows/build.yml)
[![pages-build-deployment](https://github.com/Quantum-0/SAR-PM-Overlay/actions/workflows/pages/pages-build-deployment/badge.svg?branch=gh-pages)](https://github.com/Quantum-0/SAR-PM-Overlay/actions/workflows/pages/pages-build-deployment)

# Screenshot

![Screenshot](Resources/Screenshot1.jpg)

# Instalation and running

- Install .NET 5.0 for Desktop
- Download latest release from this repository
- Open the game
- Run that application
- Start private match
- Enjoy!

## Usage

I hope interface is friendly for user and don't need an explanation. 

The only non-obvious thing is that right-clicking on the [Kill] [God] [Ghost] buttons applies the command to itself, and for the [Spawn] button - executes the same command that was called the last time when selecting item.

## Features

The list of features available in that overlay:
- Match ID - *prints current match's ID*
- Night - *switchs the night*
- Gas - *turn on/off the gas*
- Soccer - *creates a soccer ball*
- Teleport - *teleports you or another player to selected location, which can be choosen from list or on the map*
- Start match - *starts match with or without bots*
- Scenarious - *executes described queue of actions*
- Kill - *kills the player or self if click with right mouse button*
- Kick - *kicks the player*
- God - *Enables/disables the god mode for the player or self if click with right mouse button*
- Ghost - *Enables/disables the ghost mode for the player or self if click with right mouse button*
- Flight - *Change flight direction*
- One hits - *one-hits mode*

## Structure of the project

```mermaid
flowchart LR
  User_Interface --> Internal_App_Logic
  subgraph User_Interface
    direction TB
      SFI(SAR Facade Instance)
      MW(Main Overlay Window)
      SPW(Select Player Window)
      STW(Teleport Window)
      SSW(Select Scenario Window)
   end
  subgraph Internal_App_Logic
    direction TB
      NM(Native Methods)
      SAR(SAR Facade EntryPoint)
      SL(Location Class)
      SP(Player Class)
      SE(SAR Enums)
      SS(Scenario)
      SC(Commands)
      SI(Interaction with game)
      SPr(Properties)
   end
   SFI --> MW
   MW --> SPW
   MW --> STW
   MW --> SSW

   SAR --> SL
   SAR --> SP
   SAR --> SS
   SAR --> SC
   SC --> SI
   SS --> NM
   SS --> SC
   SC --> SE
   SAR --> SPr
   SPr --> SI
   SAR --> NM
   
   click MW href "https://github.com/Quantum-0/SAR-PM-Overlay/blob/master/MainWindow.xaml.cs"
   click SS href "https://github.com/Quantum-0/SAR-PM-Overlay/blob/master/SARFacade/SARScenario.cs"
   click SC href "https://github.com/Quantum-0/SAR-PM-Overlay/blob/master/SARFacade/SARFacadeCommands.cs"
   click SI href "https://github.com/Quantum-0/SAR-PM-Overlay/blob/master/SARFacade/SARFacadeInteractionWithSAR.cs"
   click SE href "https://github.com/Quantum-0/SAR-PM-Overlay/blob/master/SARFacade/InGameEntities/SAREnums.cs"
   click SP href "https://github.com/Quantum-0/SAR-PM-Overlay/blob/master/SARFacade/InGameEntities/SARPlayer.cs"
   click SL href "https://github.com/Quantum-0/SAR-PM-Overlay/blob/master/SARFacade/InGameEntities/SARLocation.cs"
   click SRp href "https://github.com/Quantum-0/SAR-PM-Overlay/blob/master/SARFacade/SARFacadeProperties.cs"
   click NM href "https://github.com/Quantum-0/SAR-PM-Overlay/blob/master/NativeMethods.cs"
   click SAR href "https://github.com/Quantum-0/SAR-PM-Overlay/tree/master/SARFacade"
```

## About Scenarios

Scenario is a queue of actions save in file .sarpms (SAR Private Match Scenario)
Syntax:
- Char # in the begining of the line - comment, line will be ignored
- Empty lines are also ignores
- Other lines interprets as **actions**
- Action contains **type** and **parameters** divided with tabulation symbol
- Types and parameters:
  - D - Delay
    - Parameter is a number of seconds or * for wait for pressing [Continue] button
  - C - Chat or command
    - Parameter is just a string which will be entered in game chat
  - P - Press a key
    - Parameter is a key
  - T - Title of scenario
  - S - Start the match
    - Parameter + turn on bots
- You can use templates in actions
- Available templates:
  - <ALL> - action will be applied for all players
  - <ME> - for current user
  - <AEM> - All Exclude Me - for all players exclude current
  - <T0> - No Team, <T1> and <T2> - Team 1 and Team 2

## People

### Author of the project
Quantum0 (aka Eat Me OwO in the game)

### Thankies to
- Mango - Access to Private Matches
- Ket_domashniy - Discussing idea, icon
- Woo - Template of scenario for duel, testing
- Naxifir - Discussing idea
- My friends in SAR - Testing
- Pixile - Thankies for that game!
- SAR wiki - Information about commands and their description, map image

*Materials from [SAR Wiki](https://animalroyale.fandom.com/) is under [CC BY-NC-SA 3.0 license](https://creativecommons.org/licenses/by-nc-sa/3.0/)*
