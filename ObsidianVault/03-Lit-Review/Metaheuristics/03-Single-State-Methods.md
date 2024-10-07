



originally I felt stronger about simulated annealing but Dr Parker has prev experience with Tabu Search going well?


----


A number of metaheuristic approaches navigate between possible solutions relative to a single solution state, to which changes are made over time (generally improvements). naive hill climbing can be considered a single-state strategy, but is susceptible to getting caught in local optima. 

Some examples of single state metaheuristic strategies include:

Iterated Local Search - An approach familiar to hill climbing, which Luke (TODO: cite lukeS) describes as attempting to "stochastically hill-climb in the space of local optima". Upon reaching some local optima using traditional hill climbing, a new location is chosen from which to continue the search. The choice of new location is informed by the position of a 'home base' solution which lies at some local optima.

Tabu Search - A method in which a list of recent solutions is maintained in order to promote exploration. By design, the algorithm is not allowed to return to these so-called 'taboo' states and instead is forced to select new neighbours to evaluate. While this mechanism can be an effective strategy to avoid getting caught in **local optima**, when navigating a real-valued decision space it can be difficult to define what constitutes 'revisiting' a solution (TODO: cite LukeS). 

Simulated Annealing - Drawing on concepts from the Metropolis algorithm, this approach models annealing - a technique used in metallurgy to improve the malleability of materials while working with them. At runtime, gradual 'cooling' of the system manifests as a smooth transition from explorative to exploitative search over time. Under the hood, the probability of accepting a candidate solution is informed by the 'temperature' of the system and the proposed change in solution quality (TODO: cite Kirkpatrick). 

TODO: add picture of blacksmithing / heated metal being worked



