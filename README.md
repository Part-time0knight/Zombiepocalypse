# Zombiepocalypse
See Releases for build.
# Architecture: 
* Project. Data exchange occurs via the DiConteiner (Zenject). The main game logic is controlled by the gameplay finite state machine.
* Player character. The game character is based on a facade. The work of handlers is regulated by the player finite state machine.
* Enemy and enemy spawner. The enemy is formed by the facade and is taken from the pool by the spawner.
* Bullets and items. Also, items and projectiles are taken from pools by their spawners.
* UI. The user interface is implemented using the MVVM pattern.
