
These are kinda antagonists for this project


probably cover
- artificial bee colony
- ant colony optimization
- bacterial foraging
- cuckoo search
- particle swarm optimizer
- genetic algorithm
	- evolutionary algorithms in general

----

Many metaheuristic approaches maintain a population of solution states throughout the process of searching for a high quality solution. Often, these designs incorporate analogies from the real world, especially nature - known as 'bio-inspired' algorithms (TODO: cite). 

Access to a population allows for comparison, interaction and even cooperation (conceptually) between solutions that may lie far apart from each other in the search space. These operations can be used to promote exploration in population-based methods (TODO: cite LukeS).

In the case of evolutionary algorithms, elements of different solutions from a population can be combined together to create new child solutions with qualities of both parents. The general structure an evolutionary algorithm is presented below in **Figure**, and elements of this approach are often incorporated into other population-based algorithms.

TODO: add a figure here showing typical evolutionary alg structure



Population-based metaheuristics algorithms include:

Genetic Algorithm - A strategy that differs from the classical evolutionary algorithm primarily in how population members are selected for breeding. In genetic algorithms, an empty population of children is initialized and gradually populated by selecting pairs of parents at a time, which are 'bred' together using mutation operations to produce child solutions. This approach is popular in the literature and has proven to be widely applicable - having been applied to scheduling, architecture and design to name only a few (TODO: add citations LukeS etc. and concrete examples?). (I could refer to my applications section here? rather than repeating myself)

Particle Swarm Optimization - An algorithm that models swarming and flocking behaviours rather than Darwinian evolution. In place of selection and breeding mechanisms, this design introduces social and coordinated behaviours to revise solutions as the search space is being explored. In terms of implementation, this approach is generally restricted to real-valued search spaces, as individual 'particles' move towards the best known solution's position with each iteration. (todo: cite lukeS)

Cuckoo Search - A nature-inspired method introduced in 2009 that aims to model brood parasitism as observed in some species of cuckoo. This algorithm treats solution states as eggs, grouped in a number of nests. New candidate solutions - 'cuckoos' are found by taking a random walk from known solutions. To explore the search space, candidate cuckoos are generated and potentially afflict an assigned nest if they represent a superior solution. With each iteration, high quality nests are conserved and new nests are randomly generated. (TODO: add citation)

-----


