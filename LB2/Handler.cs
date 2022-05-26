using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Enum;
using LB2.Model;

namespace Lab2
{
    public class Handler<T>
    {
        private readonly Dictionary<Menu, Func<int, Dictionary<string, object>, Task>> _handlers;
        private readonly string _inputFile;
        private readonly string _statusFile;
        private List<Dictionary<string, object>> dictionary;

        public Handler(string inputFile, string statusFile, bool isValidate = true)
        {
            _inputFile = inputFile;
            _statusFile = statusFile;
            dictionary = Advertisement.GetListDictionaryFromFile(_inputFile, false, isValidate);
            _handlers = new Dictionary<Menu, Func<int, Dictionary<string, object>, Task>>
            {
                { Menu.Sort, HandleSortFile },
                { Menu.Search, HandleFilter },
                { Menu.Remove, HandleRemoveRow },
                { Menu.Update, HandleUpdate },
                { Menu.Add, HandleAddRow },
                { Menu.Approve, HandleApprove },
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
            List<Dictionary<string, object>> statusModel = new List<Dictionary<string, object>>(rowid);

            var role = arg.FirstOrDefault(w => w.Key.Contains("Role")).Value;

            if (role == null)
            {
                statusModel = Advertisement.ReadStatusModel(_statusFile, _inputFile, arg.FirstOrDefault(w => w.Key.Contains("Email")).Value.ToString());
                Advertisement.Filter(statusModel, arg.FirstOrDefault(w => !w.Key.Contains("Email")).Key, arg.FirstOrDefault(w => !w.Key.Contains("Email")).Value.ToString());
            }
            else
            {
                statusModel = Advertisement.ReadStatusModel(_statusFile, _inputFile, string.Empty, true);
                Advertisement.Filter(statusModel, arg.FirstOrDefault(w => !w.Key.Contains("Role")).Key, arg.FirstOrDefault(w => !w.Key.Contains("Role")).Value.ToString());
            }
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
            dictionary = Advertisement.GetListDictionaryFromFile(_inputFile, false);
        }
        private async Task HandleApprove(int rowId, Dictionary<string, object> arg)
        {
            var statusFileModel = Advertisement.ParceFileToModel<StatusModel>(_statusFile);

            var result = statusFileModel.FirstOrDefault(w => w.ID == rowId);

            if (result != null && result.Action == Enum.Menu.Remove.ToString())
            {
                Advertisement.DeleteRow(_inputFile, dictionary, "ID", rowId > 0 ? rowId : -1);
                dictionary = Advertisement.GetListDictionaryFromFile(_inputFile);
            }
        }
    }
}