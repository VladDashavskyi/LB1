using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Enum;

namespace Lab2
{
    public class Handler<T>
    {
        private readonly Dictionary<Menu, Func<int, Dictionary<string, object>,Task>> _handlers;
        private readonly string _inputFile;
        private List<Dictionary<string, object>> dictionary;

        public Handler(string inputFile, bool isValidate = true)
        {
            _inputFile = inputFile;
            dictionary = Advertisement.GetListDictionaryFromFile(_inputFile,false, isValidate);
            _handlers = new Dictionary<Menu, Func<int, Dictionary<string, object>, Task>>
            {
                { Menu.Sort, HandleSortFile },
                { Menu.Search, HandleFilter },
                { Menu.Remove, HandleRemoveRow },
                { Menu.Update, HandleUpdate },
                { Menu.Add, HandleAddRow },
            };
        }

        public void HandlerRun(Menu menu, int rowId, Dictionary<string, object> arg)
        {
            if (!System.Enum.IsDefined(typeof(Menu), menu))
            {
                Console.WriteLine($"Unsupported action type - {(int)menu}");
                return;
            }

            if (_handlers.ContainsKey(menu))
                _handlers[menu].Invoke(rowId, arg);
        }
        private async Task HandleSortFile(int rowid, Dictionary<string, object> arg)
        {
            int.TryParse(arg.FirstOrDefault().Value.ToString(), out int order);
            Advertisement.Sort(_inputFile, dictionary, arg.FirstOrDefault().Key, order);
            dictionary = Advertisement.GetListDictionaryFromFile(_inputFile);
        }

        private async Task HandleFilter(int rowid, Dictionary<string, object> arg)
        {
            Advertisement.Filter(dictionary, arg.FirstOrDefault().Key, arg.FirstOrDefault().Value.ToString());
        }

        private async Task HandleRemoveRow(int rowId, Dictionary<string, object> arg)
        {
            Advertisement.DeleteRow(_inputFile, dictionary, "ID", rowId > 0 ? rowId : -1);
            dictionary = Advertisement.GetListDictionaryFromFile(_inputFile);
        }

        private async Task HandleUpdate(int rowid, Dictionary<string, object> arg)
        {
            Advertisement.UpdateRow(_inputFile, dictionary, "ID", rowid, arg);
            dictionary = Advertisement.GetListDictionaryFromFile(_inputFile);
        }

        private async Task HandleAddRow(int rowid, Dictionary<string, object> arg)
        {
            dictionary.Add(arg);
            Advertisement.AddRow(_inputFile, dictionary);
            dictionary = Advertisement.GetListDictionaryFromFile(_inputFile);
        }

        private Dictionary<string, object> AddRow(string key, object value)
        {
            Dictionary<string, object> newRow = new Dictionary<string, object>();
            newRow.Add(key, value);

            return newRow;
        }
    }
}
