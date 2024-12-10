
from batch_scorer import BatchScorer
from wrapped_subset import WrappedSubset
from wrapped_aligner import WrappedAligner
from wrapped_scorer import WrappedScorer

subset = WrappedSubset("PREFAB-A")
aligner = WrappedAligner("MAli v0.1", "MAli", "MAli-v0.1")

scorer = WrappedScorer("qscore")
batch_scorer = BatchScorer(aligner, subset, scorer)

# 1011, 1012, 1013, 1014, 1015

SEED_VALUE = 1015
aligner.set_seed(SEED_VALUE)

batch_scorer.record_scores(f"{aligner.title}_seed_{SEED_VALUE}.csv")
