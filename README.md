# About

Hot-reload-enabled modification of the [3D-Platformer-Lite](https://github.com/supremestranger/3D-Platformer-Lite).

The purpose of this fork is to proof that [leohot](https://github.com/kkolyan/leohot) (Unity hot-reload extension for LeoECS Lite) works.

# Modifications

Slight refactoring that at most allow to use hot-reload extension properly: some init-systems split into two, 
because with hot-reload there are two "init" phases - one that initializes game state and run only once, 
and one that initializes miscellaneous things and should be run after each hot-reload.
