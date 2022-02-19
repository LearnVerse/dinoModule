# IMPORTANT

**This folder is for any prefabs that will be spawned in through the photon network**

Documentation can be found here: https://doc.photonengine.com/en-us/realtime/current/getting-started/realtime-intro 

Please be careful about where you place packaged assets, non-spawning static gameobject like level assets should go in the prefabs. While things like players must go in the Resources folder.

> Most if not all object in the resources folder must also have a `PhotonView.cs` component on them to be used by the photon network.

> Note: Only player positions are synced. The Player animations need to be sync separately.