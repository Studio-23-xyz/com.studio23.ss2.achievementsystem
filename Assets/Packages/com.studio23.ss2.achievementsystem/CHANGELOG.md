# Changelog

## [v1.2.2] - 2024-1-2

### Update
- Unlocking achievements now implements a process queue

## [v1.2.1] - 2023-12-20

### Added
- Initialization Event is now based on unity event.

## [v1.1.9] - 2023-12-18

### Added
- Added Initialiation Event
- Now Achievement system initialization is being check by a bool


## [v1.1.8] - 2023-12-15

### Added
- Added Explicit is unlocked property


## [v1.1.7] - 2023-12-14

### Added
- AchievementData Class added
- Now you can get AchievementData from the system


## [v1.1.5] - 2023-12-12

### Added
- UpdateAchievementProgress method added

## [v1.1.4] - 2023-12-11

### Added
- Get Achievement Method

### Fixed
- An editor script bug that didn't show stats features

## [v1.1.1] - 2023-12-08

### Added
- Stats Table Generator and Mapper
- Added New API to call Stats From Achievement System


## [v0.1.0] - 2023-10-31


### Updated
- Updated Provider Classs : Made IDMapper Protected


## [v0.0.9] - 2023-10-30

### Added
- Added an Achievement Wizard for easy creation of achievements, including name, description, icons, type, and progress goal.
- Implemented an Achievement Manager for efficient management of achievements.
- Provided extensions for Xbox and Steam platforms.
- Added a new script for GUI elements and styles in the Achievement Wizard.
- Added ID Mapping Table Generator

### Updated
- Updated README with instructions on how to use the Achievement System package.
- Added a new MIT License, granting permissions for use, modification, and distribution of the software.

### Removed
- Removed all data classes as it's unnecessary for the System
- Removed Editor script for creating the assets