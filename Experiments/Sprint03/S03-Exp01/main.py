
from batch_scorer import BatchScorer
from wrapped_subset import WrappedSubset
from wrapped_aligner import WrappedAligner
from wrapped_scorer import WrappedScorer

subset = WrappedSubset("PREFAB-J")

aligners = []
aligners.append(WrappedAligner("MAli v0.2", "MAli", "MAli-v0.2"))
aligners.append(WrappedAligner("MAli Candidate A", "MAli", "MAli-candidate-A"))
aligners.append(WrappedAligner("MAli Candidate B", "MAli", "MAli-candidate-B"))
aligners.append(WrappedAligner("MAli Candidate C", "MAli", "MAli-candidate-C"))

ITERATIONS = 10
SEED_VALUE = 39

for aligner in aligners:

    aligner: WrappedAligner
    scorer = WrappedScorer("qscore")
    batch_scorer = BatchScorer(aligner, subset, scorer)
    aligner.specify_iterations(ITERATIONS)
    aligner.set_seed(SEED_VALUE)
    batch_scorer.record_scores(f"{aligner.title}_seed_{SEED_VALUE}.csv")
