using SADgui;
using SADgui.Models;
using Second.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Second.ViewModels
{
    public class GameServerViewModel : ViewModelBase
    {

        private IGameServer m_server;

        public GameServerViewModel(IGameServer server)
        {
            m_server = server;
        }

        public List<string> GetList()
        {
            var data = m_server.RetrieveGameList();
            List<string> temp = data.ToList();
            return temp;
        }
    }
}
