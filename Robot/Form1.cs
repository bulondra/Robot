using Automation.BDaq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robot
{
    public partial class Form1 : Form
    {
        private static InstantDoCtrl port;
        private static InstantDiCtrl vstup;
        private static DeviceInformation di;
        RobotManager robotManager;
        BytesManager bytesManager;

        public Form1()
        {
            InitializeComponent();
            initBoard();

            robotManager = new RobotManager(this);
            bytesManager = new BytesManager(this);

            timer1.Start();
        }

        public InstantDoCtrl getPort()
        {
            return port;
        }

        public void initBoard()
        {
            port = new InstantDoCtrl();
            vstup = new InstantDiCtrl();
            di = new DeviceInformation();

            di.Description = "PCI-1756,BID#0";
            di.DeviceMode = AccessMode.ModeWrite;

            port.SelectedDevice = di;
            port.LoadProfile("...\\profil.xml");
            

            vstup.SelectedDevice = di;
            vstup.LoadProfile("...\\profil.xml");

            port.Write(0, 0xFF);
        }

        public RobotManager getRobotManager()
        {
            return robotManager;
        }

        public BytesManager getBytesManager()
        {
            return bytesManager;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            byte ovladac;
            vstup.Read(1, out ovladac);

            byte cidla;
            vstup.Read(0, out cidla);

            byte vystup;
            port.Read(0, out vystup);


            // ATTACK BUTTON
            if (robotManager.attackButtonPressed(ovladac))
            {
                button9.BackColor = Color.Orange;
            } else
            {
                button9.BackColor = Color.FromKnownColor(KnownColor.Control);
            }

            // LEFT BUTTON
            if (robotManager.leftButtonPressed(ovladac))
            {
                button3.BackColor = Color.Red;

                port.Read(0, out vystup);
                robotManager.smerOtaceni(vystup, false);

                port.Read(0, out vystup);
                robotManager.otaceniZakladny(vystup, true);
            } else
            {
                button3.BackColor = Color.FromKnownColor(KnownColor.Control);
                port.Read(0, out vystup);
                robotManager.otaceniZakladny(vystup, false);
            }

            // RIGHT BUTTON
            if (robotManager.rightButtonPressed(ovladac))
            {
                button4.BackColor = Color.Red;

                port.Read(0, out vystup);
                robotManager.smerOtaceni(vystup, true);

                port.Read(0, out vystup);
                robotManager.otaceniZakladny(vystup, true);
            } else
            {
                button4.BackColor = Color.FromKnownColor(KnownColor.Control);

                port.Read(0, out vystup);
                robotManager.otaceniZakladny(vystup, false);
            }

            // UP BUTTON
            if (robotManager.upButtonPressed(ovladac))
            {
                button1.BackColor = Color.Red;

                port.Read(0, out vystup);
                robotManager.smerOtaceni(vystup, false);

                port.Read(0, out vystup);
                robotManager.otaceniRamena(vystup, true);
            } else
            {
                button1.BackColor = Color.FromKnownColor(KnownColor.Control);

                port.Read(0, out vystup);
                robotManager.otaceniRamena(vystup, false);
            }

            // DOWN BUTTON
            if (robotManager.downButtonPressed(ovladac))
            {
                button2.BackColor = Color.Red;

                port.Read(0, out vystup);
                robotManager.smerOtaceni(vystup, true);

                port.Read(0, out vystup);
                robotManager.otaceniRamena(vystup, true);
            } else
            {
                button2.BackColor = Color.FromKnownColor(KnownColor.Control);

                port.Read(0, out vystup);
                robotManager.otaceniRamena(vystup, false);
            }

            port.Read(0, out vystup);
            robotManager.taktovaciPulz(vystup);
        }
    }
}
