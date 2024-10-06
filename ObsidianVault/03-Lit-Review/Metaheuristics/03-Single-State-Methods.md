



originally I felt stronger about simulated annealing but Dr Parker has prev experience with Tabu Search going well?


----


A number of metaheuristic approaches navigate between possible solutions relative to a single solution state, to which changes are made over time (generally improvements). naive hill climbing can be considered a single-state strategy, but is susceptible to getting caught in local optima. 

Some examples of single state metaheuristic strategies include:

iterated local search - aims to improve on hill climbing with random restarts by informing the restart positions by already known high-quality solutions

tabu search - maintains a blacklist/'taboo' list of solutions not to be revisited to promote exploration of the search space, as a mechanism for avoiding getting stuck at local optima.

simulated annealing - uses 'annealing' metaphor, modelling a high quality solution as a low-energy (stable) state of the system, with poor quality solutions having lots of energy / being unstable. The probability of accepting a new solution relative to the current is informed by the proposed change in energy.




