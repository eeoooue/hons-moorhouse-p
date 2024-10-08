
> What options are available to you for the tools, techniques and design parameters of your project?  How will you evaluate them and make the best selection?


**For Development Tools**

C++ is one option, offers performance & OO development

C alignment tools can be found in the literature

C# tools aren't typical of alignment software in the literature
I like C# for the support for me as a developer, especially in the automated testing of my software's behaviour

Why have I chosen this tooling for the project:
- C# has lots of support for tooling and automated testing
- When working across disciplines, testing for established behaviours is even more valuable. 

Choice informed by the produced design of the software. A smaller design could be changed more rapidly in python, while C++ might only be suitable for simple designs. A complex design or an especially testable might be a reason to choose C# for the development tooling.

---

**For Development Theory**

Could use design patterns: strategy pattern, observer pattern, singleton pattern (for randomness?)?

choice of algorithms?
single state methods in; simulated annealing, tabu search, iterated local search
population based; cuckoo search, genetic algorithm
number of iterations
how to model the problem

**For Comparison**

I have access to other alignment tools for comparison against my own tool:
- ClustalOmega is the latest of a renowned lineage of alignment software 
- MUSCLE is a strong iterative aligner associated with a structural benchmarking dataset
	- fast & gives good results

Can I find a metaheuristic aligner exe to compare against?
- If not, why not? is this an opportunity/benefit for my project?

**Choice of structural benchmarking dataset**


**For Experimentation**

I have options of metaheuristic methods, their parameters
objective functions to choose from in the literature

I will use experimentation process to pick best performers and aim to improve the efficiency and scoring of my tool wherever possible?

There is a wealth of research on the problem, while many combinations of objective functions remain unexplored.


**For Assessing the Software**

Structural based, simulated, consensus

within these options, further choices; though it is clear that the dataset must be externally sourced as im not a bioinformatician

