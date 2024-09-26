
What, Why, How, When

Think of it as a business pitch almost


Remember I have a research gap!
- single state metaheuristics for MSA is underrepresented
- there is room for further exploration of multi-objective approaches for MSA
	- in terms of the specific objective functions used

Objectives need to be **SMART**

Ethical concerns are hard for me, it's okay to have none but:
- Transparency is key in my work - the aligner must be explainable
- Tests should be reproducible - what testcases did I use, exact settings
- Make my program available for reproducing my testing & make this easy
- I shouldn't misrepresent the effectiveness of my tool.

What resources do I need?
- The structural references are my main external dependency
	- I could use simulated instead if they become unavailable
- I need to use someone else's scoring tool - QScore


I intend to use an iterative methodology:
- make something that makes a limping attempt at the job
- iteratively improve it & make it better
- release it regularly

What benefit is there to my project:
- are any iterative aligners even available as CLI tools?
- potentially bring single-state methods into the spotlight as candidates for further MSA research
- potentially make a meaningful contribution to the exploration of multiple-objective approaches for MSA

Constraints:
- I need to work with established Bioinformatics file formats, such as FASTA. (read and write)

Why does my project take the form that it does?
- I want to make a cross platform CLI tool:
	- Good for automated pipelines, accessible cross-platform

Why have I chosen this tooling for the project:
- C# has lots of support for tooling and automated testing
- When working across disciplines, testing for established behaviours is even more valuable. 

Risks:
- Some factor about the problem could stump me:
	- I could define a simplified form of the problem and solve that instead
- Solving for the benchmark testcases with my algorithm + C# could be too time consuming to be practical
	- I could use make custom groups of the smaller testcases, or use simulation approaches to create smaller testcases than are available in public datasets
- The software may entirely break at some stage
	- should be mitigated by using an agile development methodology where the capability and effectiveness of the program is well documented and available as a fallback

