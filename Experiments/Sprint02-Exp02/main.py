
import os
from batch_scorer import BatchScorer
from wrapped_subset import WrappedSubset
from wrapped_aligner import WrappedAligner
from wrapped_scorer import WrappedScorer

subset = WrappedSubset("PREFAB-I")
aligner = WrappedAligner("MAli Candidate", "MAli", "MAli-candidate")
scorer = WrappedScorer("qscore")
batch_scorer = BatchScorer(aligner, subset, scorer)

SEED_VALUE = 4
aligner.set_seed(SEED_VALUE)

batch_scorer.record_scores(f"{aligner.title}_seed_{SEED_VALUE}.csv")

# needs timers