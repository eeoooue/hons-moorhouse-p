
from batch_scorer import BatchScorer
from wrapped_subset import WrappedSubset
from wrapped_aligner import WrappedAligner
from wrapped_scorer import WrappedScorer

subset = WrappedSubset("PREFAB-J")
# aligner = WrappedAligner("MAli v0.1", "MAli", "MAli-v0.1")
aligner = WrappedAligner("MAli Candidate", "MAli", "MAli-candidate")

scorer = WrappedScorer("qscore")
batch_scorer = BatchScorer(aligner, subset, scorer)

aligner.specify_iterations(100)

SEED_VALUE = 525
aligner.set_seed(SEED_VALUE)

batch_scorer.record_scores(f"{aligner.title}_seed_{SEED_VALUE}.csv")

# needs timers