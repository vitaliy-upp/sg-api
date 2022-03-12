namespace MiroWhiteBoard.Utils
{
    public class MiroConstants
    {
        public const string BoardsUrl = "https://api.miro.com/v1/boards";

        /// <summary>{0} - board id</summary>
        public const string GetWidgetsUrl = "https://api.miro.com/v1/boards/{0}/widgets/";

        /// <summary>
        /// {0} - board id
        /// {1} - widget id
        /// </summary>
        public const string DeleteWidgetUrl = "https://api.miro.com/v1/boards/{0}/widgets/{1}";
    }
}
