Evan Sinasac - 1081418
INFO6017 Artificial Intelligence
Project 1

This program was built in Unity 2020.3.25f1 Personal.  The purpose of this project is to implement the different steering behaviours taught in class and create a simple FPS arena to showcase the different enemies and behaviours as per the requirements.

CONTROLS
WASD			- Move player around the scene
Space			- Simple jump
Mouse			- Aim the camera
Mouse Left Click 	- Shoot a paintball

ENEMIES
Type A: Seek/Flee behaviour.  When the player is looking directly at this enemy, it switches to the flee behaviour.  Otherwise, it seeks the player's current position.  Blue when seeking, Orange when fleeing.

Type B: Pursue/Evade behaviour.  Attempts to pursue and intercept the player.  However, when the player fires a paintball, and that paintball is within the evade distance, it evades the closest paintball to it.  Green when pursuing, purple when evading.

Type C: Approach behaviour and firing at player.  Seeks the player and uses approach behaviour to slow and stop a specified distance from the player.  While within the target distance, it maintains this distance and faces the player, firing a paintball at the player every 2 seconds or so.  Silver when seeking, gold when within firing range.

Type D: Wander/Idle behaviour.  This enemy doesn't care about the player.  Instead it wanders around the arena for 6 seconds and then stops and idles for 3 seconds.  Dark purple when wandering, red when idling.


Video Demo: https://youtu.be/-zY0SN0D1oI