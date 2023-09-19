namespace Core.SpecificationParams
{
    public class SpecParamsEvaluator
    {
        public int Skip { get; private set; }
        public int Take { get; private set; }
        public bool HasDateFilter { get; private set; }

        public SpecParamsEvaluator(SpecParams specParams)
        {
            ApplyPagination(specParams.ItemsPerPage * (specParams.CurrentPage - 1), specParams.ItemsPerPage);
            CheckDateFilter(specParams.TransactionDate);
        }

        private void ApplyPagination(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }

        private void CheckDateFilter(DateTime transactionDate)
        {
            HasDateFilter = transactionDate != new DateTime();
        }
    }
}
