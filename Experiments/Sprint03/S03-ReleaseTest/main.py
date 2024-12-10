
from batch_scorer import BatchScorer
from wrapped_subset import WrappedSubset
from wrapped_aligner import WrappedAligner
from wrapped_scorer import WrappedScorer

subset = WrappedSubset("PREFAB-C")

aligners = []
aligners.append(WrappedAligner("MAli v1.0", "MAli", "MAli-v1.0"))
aligners.append(WrappedAligner("MAli v0.2", "MAli", "MAli-v0.2"))

ITERATIONS = 100
SEED_VALUE = 39

for aligner in aligners:

    print(f"Now testing: {aligner.title}")

    aligner: WrappedAligner
    scorer = WrappedScorer("qscore")
    batch_scorer = BatchScorer(aligner, subset, scorer)
    aligner.specify_iterations(ITERATIONS)
    aligner.set_seed(SEED_VALUE)
    batch_scorer.record_scores(f"{aligner.title}_seed_{SEED_VALUE}_iters_{ITERATIONS}.csv")
