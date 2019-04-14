using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTest
{
    public class MainPresenter
    {
        private readonly IMainParser _parser;
        private readonly IMessageService _message;

        public MainPresenter(IMainParser parser, IMessageService message)
        {
            _parser = parser;
            _message = message;
        }
    }
}
