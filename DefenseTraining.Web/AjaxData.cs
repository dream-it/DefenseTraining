namespace DefenseTraining.Web
{
    public class AjaxData
    {
        public AjaxData()
        {
        }

        public AjaxData(bool isSuccessful, object data, string errorMessage)
            : this(isSuccessful, data, errorMessage, 0)
        {
        }

        public AjaxData(bool isSuccessful, object data, string errorMessage, int totalCount)
        {
            Data = data;
            IsSuccessful = isSuccessful;
            ErrorMessage = errorMessage;
            TotalCount = totalCount;
        }

        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
        public int TotalCount { get; set; }
    }
}