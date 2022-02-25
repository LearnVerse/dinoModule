 # Quick notes on Mirror:

**Callbacks** - Hook into events that happen throughout a game

**Commands** - Client call method on server

**RPCs** -  Server call method on Client

**Messages** - There's a way to use more lower level primitives within Mirror

<hr>

## Network Identity
- Keeps track of objects in the game. Assigns a unique integer to the object that is spawned in. All objects that will spawned in game MUST have a network identity.

<hr>

## Network Authority
- Who is the ultimate source of truth. Server ultimately has all authority, but you can hand over connection to the client.

<hr>

*These are examples*

`NetworkServer.AddPlayerForConnection(conn, player);`

- Spawn a player object

`NetworkServer.Spawn(obj, connectionToClient);`

- Whenever an object is spawned

`NetworkIdentity.AssignClientAuthority(conn);`

- Called on object's Network Identity to hand over authority

`NetworkIdentity.RemoveClientAuthority();`

- Can't remove from player objects

`NetworkServer.ReplacePlayerForConnection(conn, player);`

- Replace player object

<hr>

## NetworkManager Callbacks

- To run custom code for these, must inherit from this and make a new script.
- All within API reference

```
Add in a way to launch 3 builds for easy testing. 
- 2 Local Clients
- 1 Local Server
```

<hr>

## NetworkBehavoir

- Can create functions that exclusively run on the server or client. Mark them like so:

```cs
[Client]
public void ClientFuntion()

[Server]
public void ServerFunction()

[Command]
// Run a function from the server but called by the client.
// Can only pass certain types of parameters !!!
```
- Commands can only be run by objects with client authorities or using `[Command(ignoreAuthority = true)]`

<hr>

## Client RPCs

- Call client functions from the server `[ClientRpc]`
- Would call on all clients, but can be alliviated using visibility parameters

<hr>

## Target RPCs

- Only invoked on one individual target clients

```cs
[TargetRpc]
void targetRpc(NetworkConnection conn)
{
    // NetworkConnection is the client specific id 
}

[TargetRpc]
void targetRpc()
{
    // Implicit version on object containing the NetworkConnection
}
```

<hr>

## SyncVar

- Only on classes inheriting NetworkBehavior
- Sync servers to clients on objects
- Shows latest state of the variable

```cs
// As simple as this
[SyncVar]
int x;
```

> Note: This only works Server to client only. When changed on client, it wont change for other clients.

- Can use a hook to run when a SyncVar is changed to run a function

```cs
[SyncVar hook=nameOf(OnChange)]
int x;

void OnChange(int oldX, int newX)
{
    // do stuff
}
```


