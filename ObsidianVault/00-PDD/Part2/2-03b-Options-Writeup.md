

> What options are available to you for the tools, techniques and design parameters of your project?  How will you evaluate them and make the best selection?

**Programming Languages**

The choice of programming language will likely be informed by the high-level design of the software, after having captured the requirements. A simpler design may be feasible in C++, which may offer performance benefits over other languages. A design with greater complexity, or clear opportunities for unit testing may be a reason to use C# due to its rich ecosystem of supporting tools.

**Algorithm Design**

In designing the algorithm there are a wide range of metaheuristic strategies to choose from, each with different properties, design choices and parameters. In addition to the choice of a strategy, the configuration of its parameters also presents options to be considered. Further, the literature surrounding MSA offers a selection of objective functions for the problem.

These many different areas of decision-making present a significant challenge in developing a proficient alignment tool. It is for this reason that an iterative development methodology has been proposed. With each successive sprint of development, experiments can be conducted to identify ways to improve on the algorithm design, with the goal of arriving at a strong alignment tool to conclude development.

**Evaluation Methods**

> I could write about how structural benchmarking is an option? though this is quite  abig rewrite.
> Use of SP and TC score is not really optional, as this is very established in the literature & I don't have the domain knowledge to pick another strategy.