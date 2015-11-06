using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEditor.BL.Interfaces;
using TextEditor.Interfaces;

namespace TextEditor
{
    public class MainPresenter
    {
        private readonly IMainForm _view;
        private readonly IFileManager _manager;
        private readonly IMessageService _messageService;
        private string _currentFilePath { get; set; }

        public MainPresenter(IMainForm view, IFileManager manager, IMessageService service)
        {
            _view = view;
            _manager = manager;
            _messageService = service;

            _view.ContentChanged += OnViewContentChanged;
            _view.FileOpenClick += OnViewFileOpenClick;
            _view.FileSaveClick += OnViewFileSaveClick;
        }

        private void OnViewFileSaveClick(object sender, EventArgs e)
        {
            try
            {
                string content = _view.Content;
                _manager.SaveContent(content, _currentFilePath);
                _messageService.ShowMessage("File saved succesfuly");
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage(ex.Message);
            }
        }

        private void OnViewFileOpenClick(object sender, EventArgs e)
        {
            try
            {
                string filePath = _view.FilePath;
                bool isExist = _manager.IsExist(filePath);
                if (!isExist)
                {
                    _messageService.ShowExclamation("Selected file doen't exist");
                    return;
                }
                _currentFilePath = filePath;
                string content = _manager.GetContent(filePath);
                int count = _manager.GetSymbolCount(content);
                _view.Content = content;
                _view.SetSymbolCount(count);
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage(ex.Message);
            }
        }

        private void OnViewContentChanged(object sender, EventArgs e)
        {
            string content = _view.Content;
            int count = _manager.GetSymbolCount(content);
            _view.SetSymbolCount(count);
        }
    }
}
