<h1 align="center">Achievement System</h1>
<p align="center">
<a href="https://openupm.com/packages/com.studio23.ss2.achievementsystem/"><img src="https://img.shields.io/npm/v/com.studio23.ss2.achievementsystem?label=openupm&amp;registry_uri=https://package.openupm.com" /></a>
</p>

Description about the package goes here. 

## Table of Contents

1. [Installation](#installation)
2. [Usage](#usage)
   - [Using Achievement Wizard](#Using-Achievement-Wizard)
   - [Using the Achievement Manager](#Using-the-Achievement-Manager)
3. [Extensions](#Extensions)

## Installation

### Install via Git URL

You can also use the "Install from Git URL" option from Unity Package Manager to install the package.
```
https://github.com/Studio-23-xyz/com.studio23.ss2.achievementsystem.git#upm
```

## Usage

### Using Achievement Wizard

You can quickly create achievements from the Editor without having to touch a single line of code. 

1. Click on the 'Studio-23' available on the top navigation bar and navigate to "Achievement System > Achievement Data Wizard"

2. A new window will appear in-front of you. Fill it up with the details of your achievements. 

3. At this stage, you have to select the locked icon and unlocked icon for the achievement. 

4. Once you're done, hit create to setup an achievement-data object.

### Using the Achievement System

1. Create an empty gameobject. 
2. Attach The Achievement System Script to it. 
3. Create your own provider or install one and add it to the same gameobject.
4. Simply call this following method to unlock an Achievement

```Csharp
   AchievementSystem.instance.UnlockAchievement(AchievementData achievementData)
```


## Extensions

There are two extensions available for In-Game Achievement System that adds support for Xbox and Steam platforms separately. Please ensure that this base package is installed if you want to use either of them. And that only one of the extension packages can be used at a time. 