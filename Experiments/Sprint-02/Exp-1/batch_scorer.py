
from wrapped_subset import WrappedSubset
from wrapped_aligner import WrappedAligner
from wrapped_scorer import WrappedScorer

# tests an aligner on a test subset using a scoring tool

class BatchScorer:
    def __init__(self, aligner: WrappedAligner, subset: WrappedSubset, scorer: WrappedScorer) -> None:
        
        self.aligner = aligner
        self.subset = subset
        self.scorer = scorer


