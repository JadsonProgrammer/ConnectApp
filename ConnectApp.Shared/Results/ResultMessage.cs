using ConnectApp.Shared.Types.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectApp.Shared.Results
{
    public class ResultMessage
    {
        public ResultMessageTypes? Type { get; set; }
        public string? Code { get; set; }
        public string? Text { get; set; }
        public DateTime? Date { get; set; }
        public IList<KeyValue>? Values { get; set; }

        public ResultMessage()
        {
            this.Date = DateTime.Now;
        }

        public ResultMessage(ResultMessageTypes type, string text)
        {
            this.Type = type;
            this.Text = text;
            this.Date = DateTime.Now;
        }

        public ResultMessage(ResultMessageTypes type, string text, string code)
        {
            this.Type = type;
            this.Code = code;
            this.Text = text;
            this.Date = DateTime.Now;
        }

        public ResultMessage(ResultMessageTypes type, string text, string code, IList<KeyValue> values)
        {
            this.Type = type;
            this.Code = code;
            this.Text = text;
            this.Date = DateTime.Now;
            this.Values = values;
        }


        public static implicit operator string(ResultMessage message) => message.Text!;
    }
}