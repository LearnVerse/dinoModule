
This design spec is largely derived from Comp-3's Unity tutorial series on Youtube.com
# User Interface
The Energy module will feature a fillable bar HUD element 

Examples:

https://images.presentationgo.com/2020/01/Progress-Bar-PowerPoint.png
https://imgs.developpaper.com/imgs/596704542-5d006e7389c90_articlex.gif

# Inputs and Outputs
Input: User action of eating an energy source (meat or plants) or an action that consumes energy.

Output: Updated energy values, accurately reflected by the HUD element


# Functional Decomposition into Modules

We anticipate the following modules or functions:

**Drain_Energy()**: _function to gradually drain user's energy to 
simulate onset of hunger; dynamic response to reflect energy cost of 
certain actions - stationary, walking, running, eating_

**Replenish_Energy()**: _function to replenish energy upon user 
consuming food_

**Death()**: _death function to be triggered when user's Energy reaches zero._


# Testing plan
Test basic functionality of radial bar; test for robustness against over-underflow values (Math.clamp)