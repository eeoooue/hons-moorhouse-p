
testing elitist genetic algorithm with ColBasedCrossoverOperator

```cs
public class ElitistGeneticAlgorithmAligner : Aligner
{
    public List<Alignment> Population = new List<Alignment>();
    public ICrossoverOperator CrossoverOperator = new ColBasedCrossoverOperator();
    public IAlignmentModifier MutationOperator = new GapShifter();

    public ISelectionStrategy TruncationSelection = new TruncationSelectionStrategy();
    public ISelectionStrategy SelectionStrategy = new RouletteSelectionStrategy();

    public AlignmentSelectionHelper SelectionHelper = new AlignmentSelectionHelper();

    public int PopulationSize = 18;
    public int SelectionSize = 6;

    public ElitistGeneticAlgorithmAligner(IObjectiveFunction objective, int iterations) : base(objective, iterations)
    {

    }
}
```

```cs
public MultiOperatorModifier ConstructMutationOperator()
{
    List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
    {
        new GapInserter(),
        new GapShifter(),
    };

    MultiOperatorModifier modifier = new MultiOperatorModifier(modifiers);
    return modifier;
}

private Aligner GetSagaInspired()
{
    IScoringMatrix matrix = new BLOSUM62Matrix();
    IObjectiveFunction objective = new SumOfPairsWithAffineGapPenaltiesObjectiveFunction(matrix, 4, 1);
    const int maxIterations = 100;

    ElitistGeneticAlgorithmAligner aligner = new ElitistGeneticAlgorithmAligner(objective, maxIterations);
    aligner.PopulationSize = 100;
    aligner.SelectionSize = 50;
    aligner.MutationOperator = ConstructMutationOperator();

    return aligner;
}
```