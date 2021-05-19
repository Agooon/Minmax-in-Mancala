# Minmax-in-Mancala

## Purpose
Project for using **Minmax** and **Alpha-beta** algorithm in game called Mancala.


## What Minmax-in-Mancala provides?
- Two algorithms for minimizing the maximum possible loss (**Minmax** and **Alpha-beta**)
- Heuristcs that calculate current state of the board
- The solution for a game called Mancala
- We can connect it with genetic algorithm for better heuristic performance
- Mode Human vs Ai (difficulty raises with increasing depth)


## Research with comparing Minmax/Alpha-beta algotiyhms with heuristics.
Due to lack of time, **ExpandedGetPoints** heuristic isn't optimized, however quick implementation of genetic algorithms can do the trick.


## Notes about implementing genetic algorithm
Genetic algorithm just takes 3 values: **wellValue**, **holeValue**, **comboValue**. The most time consuming part, would be the play time of the functions.
I would just add value of importance to each Depth to improve the greater depth (instead of focusing on depth 1-3, we would put more values on depth 10-16)

Single game with depth equal to 16 (average time of single move is around 1,7 seconds), each game have around 30 moves, so simple math, each game last ~ 50 seconds. 
To get a good number of games, each depth should played at minimum of 50-100 games. (more is better ofcourse for better accuracy to calculate points of single solution)

Maybe (big maybe) I'll do it someday (most likely not >.<)

##
### Reasearch file Can be found in **Sierakowski-Adam-Sprawozdanie GRY.pdf**
