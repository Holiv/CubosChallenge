namespace Core.SpecificationParams
{
    public class SpecParams
    {
        private int _itemsPerPage = 5;

        public int ItemsPerPage
        {
            get => _itemsPerPage; 
            set => _itemsPerPage = value <= 0 ? _itemsPerPage : value;
        }
        public int CurrentPage { get; set; } = 1;
        public DateTime TransactionDate { get; set; }

    }
}
