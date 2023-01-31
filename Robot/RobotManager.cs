using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
    public class RobotManager
    {
        private Form1 instance;
        public RobotManager(Form1 instance)
        {
            this.instance = instance;
        }

        public bool attackButtonPressed(int state)
        {
            return !instance.getBytesManager().isSet(state, (int)RemoteButtons.ATTACK + 1);
        }

        public bool leftButtonPressed(int state)
        {
            return !instance.getBytesManager().isSet(state, (int)RemoteButtons.LEFT + 1);
        }

        public bool rightButtonPressed(int state)
        {
            return !instance.getBytesManager().isSet(state, (int)RemoteButtons.RIGHT + 1);
        }

        public bool upButtonPressed(int state)
        {
            return !instance.getBytesManager().isSet(state, (int)RemoteButtons.UP + 1);
        }

        public bool downButtonPressed(int state)
        {
            return !instance.getBytesManager().isSet(state, (int)RemoteButtons.DOWN + 1);
        }

        public void otaceniZakladny(int state, bool run)
        {
            if (!run)
            {
                instance.getPort().Write(0, (byte)instance.getBytesManager().setBit(state, (int)MotorType.OTACENI_ZAKLADNY));
                return;
            }

            instance.getPort().Write(0, (byte)instance.getBytesManager().clearBit(state, (int)MotorType.OTACENI_ZAKLADNY));
        }

        public void otaceniRamena(int state, bool run)
        {
            if (!run)
            {
                instance.getPort().Write(0, (byte)instance.getBytesManager().setBit(state, (int)MotorType.HLAVNI_RAMENO));
                return;
            }

            instance.getPort().Write(0, (byte)instance.getBytesManager().clearBit(state, (int)MotorType.HLAVNI_RAMENO));
        }

        public void taktovaciPulz(int state)
        {
            if (instance.getBytesManager().isSet(state, (int)MotorType.TATKOVACI_PULZ+1)) instance.getPort().Write(0, (byte)instance.getBytesManager().clearBit(state, (int)MotorType.TATKOVACI_PULZ));
            else instance.getPort().Write(0, (byte)instance.getBytesManager().setBit(state, (int)MotorType.TATKOVACI_PULZ));
        }

        public void smerOtaceni(int state, bool direction)
        {
            if (direction) instance.getPort().Write(0, (byte)instance.getBytesManager().setBit(state, (int)MotorType.SMER_OTACENI));
            else instance.getPort().Write(0, (byte)instance.getBytesManager().clearBit(state, (int)MotorType.SMER_OTACENI));
        }
    }
}
