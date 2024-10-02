

> Why is this a useful project?  What does it seek to achieve?  What is the motivation for it (not your motivation for doing the project)?  What are the **primary objectives**, and what **secondary objectives** may be achieved if there is time?  Make sure these are SMART objectives.

> I think I will do primary & secondary in PDD & then prioritized in the report?

Objectives need to be **SMART** - [[SMART Objectives]]

### Aims

TODO: simplify 

To create an (command line) alignment tool capable of performing Multiple Sequence Alignment (MSA) on authentic test cases/data. (say the goal is speed and quality!)

(To explore, understand and communicate the strengths and weaknesses of a single-state metaheuristic strategy for sequence alignment.

To consider and discuss whether population-based methods should be favoured over single-state methods in regards to the problem of MSA.)
#### Performing MSA

Produce an alignment tool that performs MSA on a set of supplied sequences, conserving the sequence identifiers and biological information of the sequences themselves. **(Sprint 3)**

Deliver a command-line tool that (on a university desktop computer) can perform multiple sequence alignment efficiently, aligning:
- a testcase of 3 protein sequences within 60 seconds. **(Sprint 3)**
- a testcase of 6 protein sequences within 10 seconds. **(Sprint 6)**

#### Work With Established File Formats

Support an established file format as an input source for biological data. Sufficient justification should be given for the choice of this file format. **(Sprint 4)** 

Support an established file format as output for the alignment of sequences produced by the software. Sufficient justification should be given for the choice of this format, which may differ from the input format. **(Sprint 4)** 

#### Experiment with metaheuristics

By conducting an experiment, identify a suitable heuristic for deciding on the number of iterations of improvement to be performed for each set of input sequences. **(Sprint 6)**

By conducting an experiment, contrast, assess and discuss quantitative differences between using a population-based or single-state metaheuristic method as the basis for the alignment software. **(Sprint 7**)


**Trying Named Objective Functions**

Assess the use of the Weighted Sum-of-Pairs objective function to guide the process of sequence alignment. 

Assess the use of the Gap Penalty objective function alongside other function(s) to guide the process of sequence alignment.

Assess the use of the Totally Conserved Columns objective function alongside other function(s) to guide the process of sequence alignment.

Assess the use of the following objective functions to guide the process of sequence alignment :
- **Weighted Sum-of-Pairs**
- **Gap Penalty**
- **Totally Conserved Columns**

**Using Multi-Objective Metaheuristics**

By conducting an experiment, compare the effectiveness single-objective and multi-objective approaches for MSA, as evaluated using structural benchmarking.



To contribute to current research surrounding Multiple Sequence Alignment (MSA), the program should demonstrate multi-objective optimisation for the alignment methods supported.
TODO: isnt SMART

#### Case Study

Perform a comparative case study of the final tool against established MSA software such as ClustalOmega, MUSCLE etc. (**Feb/March?**)



-------

Produce an alignment tool that is compatible with Windows 11, Ubuntu and MacOS desktop computing platforms.

Provide sufficient descriptions of testing configurations such that tests of the software can be reproduced by others. i.e. specify the testcases used and the version of software tested.

#### Deliver a series of iterations of the software

> basically say that I will release the software after each sprint?






