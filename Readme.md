This is test task for Playphoria.

[Playable build](https://kiirusha95.itch.io/playphoriatest)

Requirements:
- Create an empty unity project.
- Place on the scene any collider with mesh renderer, which would act as a ground plane.
- Add any asset with an animated character to the scene.
- Provide the character with the ability to collide with the ground and physical obstacles.
- Add a ready joystick asset to the project. Implement control of the player with this joystick.
- Make the camera follow the player from above.
- Place on the stage primitive physical obstacles of various sizes and weights - cubes, spheres, etc. Check that the player when moving collides with obstacles and cannot pass through them, the obstacles should react to a collision with the player - move / roll etc.
- Find any exposed hand animation and add it to the character animator. Implement the activation of this animation when the player hits the front of any physical obstacle. Implement disabling this animation when the player does not rest against an obstacle.
- Add hit points to the character and implement the HealthBar, which will hang over the character in WorldSpace.
- Place a turret on the stage, which will rotate around its axis and periodically shoot projectiles. When hitting the player, the projectile should cause damage.
- At a fatal hit to a character, it is necessary to push him away from the point of impact and activate Ragdoll mode.
- Implement saving the position of objects on the stage between restarts of the game.