# MeatScraper
Very cool game that is developed at the Making Games Remastered 2022 seminar at HfG Karlsruhe

## Usage

The game is developed with Unity 2021.3.3f1.

While playing in the editor, left click to hide your cursor and press escape to show it again.

## Configurations

### Player:

- **Break Acceleration:** Deacceleration rate when counter steering after pushed back by an explosion
- **Jump Velocity:** Upwards velocity when pressing jump on the ground
- **Movement Speed:** Player velocity when pressing left or right
- **Ragdoll Duration:** Time after explosion for which you are pushed back while being on the ground

### Gun:

- **Cooldown:** Time between each shot

### Projectile (Prefab):

- **Explosion Force:** How much maximum force will be applied to entities
- **Explosion Full Force Radius:** Radius around the explosion in which the full force is applied
- **Explosion Radius:** Maximum range influenced by an explosion
- **Is Force More Distributed:** Whether a more gentle explosion force decline is used instead of the realistic one
- **Speed:** Velocity of the projectile