namespace quotes_web.Domain
{
    public class ValidationError
    {
        public string Title { get; set; }
        public string Text { get; set; }

        public ValidationError(string text, string title)
        {
            this.Title = title;
            this.Text = text;
        }
    }
}