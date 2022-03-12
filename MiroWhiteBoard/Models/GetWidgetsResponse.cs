using System.Collections.Generic;

namespace MiroWhiteBoard.Models
{
    public class GetWidgetsResponse
    {
        public string Type { get; set; }
        public List<BoardWidget> Data { get; set; }
        public int Size { get; set; }
    }
}
