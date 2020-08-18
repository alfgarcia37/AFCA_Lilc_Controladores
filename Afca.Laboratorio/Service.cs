using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Afca.Common;
using Afca.Common.DataSets;
using Afca.Common.ServicesInterfaces;

namespace Afca.Laboratorio
{
    public partial class Service : ServiceBase
    {
        IBuzonLaboratorioService _service;
        Timer timer = new Timer();
        public Service()
        {
            InitializeComponent();
            _service = AfcaServiceProvider.GetBuzonLaboratorioService();
        }

        protected override void OnStart(string[] args)
        {
            timer.Interval = TimeSpan.FromHours(1).TotalMilliseconds;
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //ir a la ruta
            //cheuqear, leer y guardar

        }

        protected override void OnStop()
        {
            timer.Enabled = false;
        }
    }
}
